using CommentPost.API.DTOs.Auth;
using CommentPost.Infrastructure.Models.Auth;
using CommentPost.Infrastructure.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace CommentPost.API.Controllers;

[ApiController]
[Route("auth/external/auth0")]
public class Auth0Controller : ControllerBase
{
	readonly Auth0Service _auth0Service;

	public Auth0Controller(Auth0Service auth0Service)
	{
		_auth0Service = auth0Service;
	}

	[HttpGet(nameof(AccessToken))]
	public async Task<ActionResult<Auth0TokenResponse>> AccessToken(string authenticationCode)
	{
		Auth0TokenResponse? tokenResponse = await _auth0Service.GetAccessToken(authenticationCode);

		if (tokenResponse == null)
			return BadRequest();

		return tokenResponse;
	}


	[HttpGet(nameof(GetUserId))]
	public async Task<ActionResult<string>> GetUserId(string accessToken)
	{
		string? userId = _auth0Service.GetUserId(accessToken);

		if (string.IsNullOrEmpty(userId))
			return BadRequest();

		return userId;
	}

	[HttpGet(nameof(GetUserInfo))]
	public async Task<ActionResult<Auth0UserInfo?>> GetUserInfo(string accessToken)
	{
		Auth0UserInfo? userInfo = await _auth0Service.GetUserInfo(accessToken);

		if (userInfo == null)
			return BadRequest();

		return userInfo;
	}






	[HttpPost(nameof(Login))]
	public async Task<ActionResult<AuthToken>> Login([FromBody] Auth0LoginRequest request)
	{
		try
		{
			Auth0TokenResponse? tokenResponse = await _auth0Service.GetAccessToken(request.AuthenticationCode);
			if (tokenResponse == null)
				throw new Exception("cannot GetAccessToken");

			string? userId = _auth0Service.GetUserId(tokenResponse.AccessToken);
			if (userId == null)
				throw new Exception("cannot GetUserId");

			AuthToken token = await _auth0Service.Login(userId);
			return Ok(token);
		}
		catch (Exception)
		{
			return Unauthorized();
		}
	}


	[HttpPost(nameof(Register))]
	public async Task<ActionResult<AuthToken>> Register([FromBody] Auth0LoginRequest request)
	{
		try
		{
			Auth0TokenResponse? tokenResponse = await _auth0Service.GetAccessToken(request.AuthenticationCode);
			if (tokenResponse == null)
				throw new Exception("cannot GetAccessToken");

			Auth0UserInfo? userInfo = await _auth0Service.GetUserInfo(tokenResponse.AccessToken);
			if (userInfo == null)
				throw new Exception("cannot GetUserInfo");

			AuthToken token = await _auth0Service.Register(userInfo);
			return Ok(token);
		}
		catch (Exception ex)
		{
			return Unauthorized();
		}
	}
}
