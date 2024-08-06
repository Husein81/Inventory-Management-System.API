using API.Extensions;
using API.UserRequests;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly TokenServices _tokenServices;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _configuration;
        public AccountController(UserManager<AppUser> userManager, 
            SignInManager<AppUser> signInManager, 
            TokenServices tokenServices, 
            IConfiguration configuration)
        {
            _signInManager = signInManager;
            _configuration = configuration;
            _userManager = userManager;
            _tokenServices = tokenServices;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserRequest>> Login([FromBody] LoginRequest request)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == request.Username);

            if(user is null)
            {
                return Unauthorized("Account does not exist");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (result.Succeeded) 
            {
                var u = new UserRequest
                {
                    DisplayName = user.DisplayName,
                    Token = _tokenServices.CreateToken(user),
                    Username = user.UserName,
                    ImageUrl = null,
                };
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Expires = DateTime.UtcNow.AddDays(7)
                };

                Response.Cookies.Append("jwt", u.Token, cookieOptions);

                return u;
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
        [AllowAnonymous]
        [HttpPost("verifyEmail")]
        public async Task<IActionResult> VerifyEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
                return Unauthorized();
            var decodedTokenBytes = WebEncoders.Base64UrlDecode(token);
            var decodedToken = Encoding.UTF8.GetString(decodedTokenBytes);
            var result = await _userManager.ConfirmEmailAsync(user, decodedToken);
            if (!result.Succeeded)
                return BadRequest("Could not Verify Email Address");
            return Ok("Email confirmed - you can now login");
        }
    }
}
