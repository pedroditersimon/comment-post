using CommentPost.Infrastructure.Models;
using CommentPost.Infrastructure.Services;
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

	[HttpPost(nameof(Token))]
	public async Task<ActionResult<Auth0TokenResponse>> Token(string authenticationCode)
	{
		Auth0TokenResponse? tokenResponse = await _auth0Service.GetAccessToken(authenticationCode);

		if (tokenResponse == null)
			return BadRequest();

		return tokenResponse;
	}


	[HttpPost(nameof(GetUserId))]
	public async Task<ActionResult<string>> GetUserId(string accessToken)
	{
		string? userId = _auth0Service.GetUserId(accessToken);

		if (string.IsNullOrEmpty(userId))
			return BadRequest();

		return userId;
	}

	[HttpPost(nameof(GetUserInfo))]
	public async Task<ActionResult<Auth0UserInfo?>> GetUserInfo(string accessToken)
	{
		Auth0UserInfo? userInfo = await _auth0Service.GetUserInfo(accessToken);

		if (userInfo == null)
			return BadRequest();

		return userInfo;
	}
}
