﻿using API.Extensions;
using API.UserRequests;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly TokenServices _tokenServices;

        public AccountController(UserManager<AppUser> userManager, TokenServices tokenServices)
        {
            _userManager = userManager;
            _tokenServices = tokenServices;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserRequest>> Login([FromBody] LoginRequest request)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == request.Email);

            if(user is null)
            {
                return Unauthorized("Invalid Email");
            }
            var result = await _userManager.CheckPasswordAsync(user, request.Password);

            if (result)
            {
                return new UserRequest()
                {
                    DisplayName = user.DisplayName,
                    Token = _tokenServices.CreateToken(user),
                    Username = user.UserName,
                    ImageUrl = null,
                };
            }

            return Unauthorized("Invalid Email or Password");
        } 

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<UserRequest>> Register([FromBody] RegisterRequest request)
        {
            if (await _userManager.Users.AnyAsync(x => x.Email == request.Email))
            {
                ModelState.AddModelError("email", "Email taken");
                return ValidationProblem();
            }

            if (await _userManager.Users.AnyAsync(x => x.UserName == request.Username))
            {
                ModelState.AddModelError("username", "Username taken");
                return ValidationProblem();
            }

            var user = new AppUser
            {
                DisplayName = request.DisplayName,
                Email = request.Email,
                UserName = request.Username,
                NormalizedEmail = request.Email.ToUpper()
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                return new UserRequest
                {
                    DisplayName = user.DisplayName,
                    Token = _tokenServices.CreateToken(user),
                    Username = user.UserName,
                    ImageUrl = null,
                };
            }

            return BadRequest(result.Errors);
        }
    }
}
