using AuthenDemo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace AuthenDemo.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LoginController : ControllerBase
	{
		private IConfiguration _config;

		public LoginController(IConfiguration config)
		{
			_config = config;
		}

		[AllowAnonymous]
		[HttpPost]
		public IActionResult Login([FromBody] UserLogin userLogin)
		{
			var user = Authenticate(userLogin);

			if (user != null)
			{
				var token = Generate(user);
				return Ok(token);
			}

			return BadRequest("User not found");
		}

		private string Generate(UserModel user)
		{
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

			Claim[] claims = new[]
			{
				new Claim(ClaimTypes.NameIdentifier, user.UserName),
				new Claim(ClaimTypes.Email, user.Email),
				new Claim(ClaimTypes.Role, user.Role)
			};

			var token = new JwtSecurityToken( _config["Jwt:Issuer"],
											  _config["Jwt:Audience"],
											  claims,
											  expires: DateTime.Now.AddDays(30),
											  signingCredentials: credentials );

			return new JwtSecurityTokenHandler().WriteToken(token);
		}

		private UserModel Authenticate(UserLogin userLogin)
		{
			var currentUser = UserDBFake.Users.FirstOrDefault( x => x.UserName.ToLower() == userLogin.UserName.ToLower() &&
															   x.Password == userLogin.Password );

			if (currentUser != null)
			{
				return currentUser;
			}

			return null;
		}
	}
}
