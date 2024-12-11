using CommentPost.API.DTOs.Auth;
using CommentPost.Application.Services;
using CommentPost.Infrastructure.Models.Auth;
using CommentPost.Infrastructure.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace CommentPost.API.Controllers;

[ApiController]
[Route("[controller]")]
public class LocalAuthController : ControllerBase
{
	readonly IUserService _userService;
	readonly LocalAuthService _localAuthService;

	public LocalAuthController(IUserService userService, LocalAuthService localAuthService)
	{
		_userService = userService;
		_localAuthService = localAuthService;
	}



	[HttpPost(nameof(Login))]
	public async Task<ActionResult<AuthToken>> Login([FromBody] LocalLoginRequest loginRequest)
	{
		try
		{
			AuthToken token = await _localAuthService.Login(loginRequest.Username, loginRequest.Password);
			return Ok(token);
		}
		catch (Exception)
		{
			return Unauthorized();
		}
	}


	[HttpPost(nameof(Register))]
	public async Task<ActionResult<AuthToken>> Register([FromBody] LocalLoginRequest loginRequest)
	{
		try
		{
			AuthToken token = await _localAuthService.Register(loginRequest.Username, loginRequest.Password);
			return Ok(token);
		}
		catch (Exception ex)
		{
			return Unauthorized(new AuthToken()
			{
				Token = ex.Message
			});
		}
	}
}
