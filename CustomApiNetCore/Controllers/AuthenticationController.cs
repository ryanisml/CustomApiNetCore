using CustomApiNetCore.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CustomApiNetCore.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public class AuthenticationController : ControllerBase
    {
        IConfiguration _configuration;
        public AuthenticationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("api")]
        public IActionResult Index()
        {
            ResponseModel response = new()
            {
                message = "Its Works!",
                code = 200
            };

            return Ok(response);
        }

        [HttpPost]
        [Route("api/login")]
        public IActionResult Login([FromBody] AuthModel user)
        {
            if (user == null)
            {
                ResponseModel response = new()
                {
                    message = "Invalid client request",
                    code = 400
                };
                return BadRequest(response);
            }

            if (user.username == "admin" && user.password == "admin")
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
                var signinCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var tokenOptions = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signinCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                var data = new
                {
                    user.username,
                    token = tokenString,
                    expiration = tokenOptions.ValidTo
                };
                ResponseModel response = new()
                {
                    success = true,
                    data = data
                };
                return Ok(response);
            }
            else
            {
                ResponseModel response = new()
                {
                    message = "Username or password inccorect",
                    code = 401
                };

                return Unauthorized(response);
            }
        }

        [HttpPost]
        [Route("api/logout")]
        public IActionResult Logout()
        {
            return Ok();
        }

        [HttpGet]
        [Route("api/validate")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Validate()
        {
            ResponseModel response = new()
            {
                success = true,
                message = "Token is valid"
            };
            return Ok(response);
        }
    }
}
