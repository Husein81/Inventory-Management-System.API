﻿using Application.Repository;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Infrastructure.Repository
{
    public class UserAccessor : IUserAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string GetUsername()
            => _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
    }
}
