using AuthenDemo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace AuthenDemo.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		[HttpGet("Admin")]
		[Authorize(Roles = "Administrator")]
		public IActionResult AdminsEndpoint()
		{
			var currentUser = GetCurrentUser();

			return Ok(currentUser);
		}

		[HttpGet("Partner")]
		[Authorize(Roles = "Partner")]
		public IActionResult PartnersEndpoint()
		{
			var currentUser = GetCurrentUser();

			return Ok(currentUser);
		}

		[HttpGet("AdminAndPartner")]
		[Authorize(Roles = "Administrator,Partner")]
		public IActionResult AdminAndPartnerEndpoint()
		{
			var currentUser = GetCurrentUser();

			return Ok(currentUser);
		}

		private UserModel GetCurrentUser()
		{
			var identity = HttpContext.User.Identity as ClaimsIdentity;

			if (identity != null)
			{
				var userClaims = identity.Claims;

				return new UserModel
				{
					UserName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
					Email = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
					Role = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value
				};
			}

			return null;
		}
	}
}
