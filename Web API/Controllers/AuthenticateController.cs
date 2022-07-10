using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Linq;
using System.Text;
using Web_API.Models;
using BLL.Models;
using Microsoft.AspNetCore.Http;

namespace Web_API.Controllers
{
    /// <summary>
    /// The controller responsible for logging into the system.
    /// </summary>
    /// <remarks>
    /// In this controller, the method of user login to the system,
    /// as well as the generation of the JWT token for the authenticated user, takes place.
    /// </remarks>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IUserService _userService;

        private IConfiguration _config;

        public AuthenticateController(IConfiguration config, IUserService userService)
        {
            _userService = userService;
            _config = config;
        }

        /// <summary>
        /// Responsible for logging into the system
        /// </summary>
        /// <returns> Status code </returns>
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status404NotFound)] // Not found
        [ProducesResponseType(StatusCodes.Status200OK)] // Ok
        public IActionResult Login([FromBody] UserLogin userLogin)
        {
            var user = Authenticate(userLogin);

            if (user != null)
            {
                var token = Generate(user);

                return Ok(new Response(token, user));
            }

            return NotFound("User not found");
        }

        /// <summary>
        /// The method that generates the JWT token
        /// </summary>
        /// <returns> JWT token in string </returns>
        /// <param name="user">A user who has passed authorization</param>
        private string Generate(UserModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Name),
                new Claim(ClaimTypes.Surname, user.Surname),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Locality, user.Location),
                new Claim(ClaimTypes.HomePhone, user.PhoneNumber),
                new Claim(ClaimTypes.Role, user.AccessLevel.ToString()),
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Audience"],
              claims,
              expires: DateTime.Now.AddMinutes(15),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// The method that performs user authenticate in the database
        /// </summary>
        /// <returns> User found </returns>
        /// <param name="userLogin">The user that will be searched in the database</param>
        private UserModel Authenticate(UserLogin userLogin)
        {
            var currentUser = _userService.GetAllAsync().Result.FirstOrDefault
                            (o => o.Email.ToLower() == userLogin.EmailAddress.ToLower() && o.Password == userLogin.Password);

            if (currentUser != null)
            {
                return currentUser;
            }

            return null;
        }
    }
}
