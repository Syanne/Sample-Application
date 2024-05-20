using IdentityServer4.Events;
using IdentityServer4;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sample.Auth.Models.Requests;
using Sample.Auth.Models.Responses;
using Microsoft.EntityFrameworkCore;

namespace Sample.Auth.Api.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly IEventService _events;
		private readonly SignInManager<IdentityUser> _signInManager; 

		public AccountController(IEventService events, SignInManager<IdentityUser> signInManager)
		{
			_events = events;
			_signInManager = signInManager;
		}

		[HttpPost]
		public async Task<IActionResult> Login([FromBody] AunthenticateUserModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _signInManager.UserManager.Users
					.FirstOrDefaultAsync(x => x.Email == model.Email);

				if (user != null)
				{
					var userLogin = await _signInManager.CheckPasswordSignInAsync(user, model.Password, true);

					if (userLogin == Microsoft.AspNetCore.Identity.SignInResult.Success)
					{
						AuthenticationProperties props = null;
						if (AccountOptions.AllowRememberLogin)
						{
							props = new AuthenticationProperties
							{
								IsPersistent = true,
								ExpiresUtc = DateTimeOffset.UtcNow.Add(AccountOptions.RememberMeLoginDuration)
							};
						};

						var isuser = new IdentityServerUser(user.Id)
						{
							DisplayName = user.UserName
						};

						await HttpContext.SignInAsync(isuser, props);

						await _events
							.RaiseAsync(new UserLoginSuccessEvent(user.UserName, user.Id, user.UserName));

						var userModel = new UserAuthenticatedResponse(user.UserName, user.Email, user.PhoneNumber);

						return Ok(userModel);
					}
				}
			}

			await _events.RaiseAsync(new UserLoginFailureEvent(model.Email, "invalid credentials"));
			throw new BadHttpRequestException("Bad request");
		}
	}
}