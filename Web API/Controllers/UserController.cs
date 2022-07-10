using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Web_API.Models;
using System.Security.Claims;
using DAL.Entities;

namespace Web_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        private readonly IConfiguration _configuration;
        public UserController(IConfiguration configuration, IUserService userService)
        {
            _userService = userService;
            _configuration = configuration;
        }
        private UserDTO CurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userClaims = identity.Claims;

                return new UserDTO
                {
                    Name = userClaims.FirstOrDefault(n => n.Type == ClaimTypes.NameIdentifier)?.Value,
                    Surname = userClaims.FirstOrDefault(n => n.Type == ClaimTypes.Surname)?.Value,
                    Email = userClaims.FirstOrDefault(n => n.Type == ClaimTypes.Email)?.Value,
                    AccessLevel = userClaims.FirstOrDefault(n => n.Type == ClaimTypes.Role)?.Value,
                };
            }
            return null;
        }


    }
}
