using CommentPost.API.Attributes;
using CommentPost.API.DTOs.Auth;
using CommentPost.Application.Exceptions;
using CommentPost.Domain.Enums;
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
	[NeedAuthorization(Role.Moderator)]
	public async Task<ActionResult<Auth0TokenResponse>> AccessToken(string authenticationCode)
	{
		Auth0TokenResponse? tokenResponse = await _auth0Service.GetAccessToken(authenticationCode);

		if (tokenResponse == null)
			return BadRequest();

		return tokenResponse;
	}


	[HttpGet(nameof(GetUserId))]
	[NeedAuthorization(Role.Moderator)]
	public async Task<ActionResult<string>> GetUserId(string accessToken)
	{
		string? userId = _auth0Service.GetUserId(accessToken);

		if (string.IsNullOrEmpty(userId))
			return BadRequest();

		return userId;
	}

	[HttpGet(nameof(GetUserInfo))]
	[NeedAuthorization(Role.Moderator)]
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
		if (string.IsNullOrWhiteSpace(request?.AuthenticationCode))
			return BadRequest("Authentication code is required.");

		try
		{
			Auth0TokenResponse? tokenResponse = await _auth0Service.GetAccessToken(request.AuthenticationCode);
			if (tokenResponse == null)
				return Unauthorized("Invalid authentication code.");

			string? userId = _auth0Service.GetUserId(tokenResponse.AccessToken);
			if (userId == null)
				return StatusCode(StatusCodes.Status500InternalServerError, "Failed to extract user ID.");

			AuthToken token = await _auth0Service.Login(userId);
			return Ok(token);
		}
		catch (Exception ex) when (ex is InvalidArgumentException
								|| ex is ResourceConflictException)
		{
			return BadRequest(ex.Message);
		}
		catch (NotFoundException ex)
		{
			return NotFound(ex.Message);
		}
		catch (InvalidCredentialsException ex)
		{
			return Unauthorized(ex.Message);
		}
		catch (Exception ex)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, $"An unexpected error occurred: {ex.Message}");
		}
	}


	[HttpPost(nameof(Register))]
	public async Task<ActionResult<AuthToken>> Register([FromBody] Auth0LoginRequest request)
	{
		if (string.IsNullOrWhiteSpace(request?.AuthenticationCode))
			return BadRequest("Authentication code is required.");

		try
		{
			Auth0TokenResponse? tokenResponse = await _auth0Service.GetAccessToken(request.AuthenticationCode);
			if (tokenResponse == null)
				return Unauthorized("Invalid authentication code.");

			Auth0UserInfo? userInfo = await _auth0Service.GetUserInfo(tokenResponse.AccessToken);
			if (userInfo == null)
				return StatusCode(StatusCodes.Status500InternalServerError, "Failed to extract user ID.");

			AuthToken token = await _auth0Service.Register(userInfo);
			return Ok(token);
		}
		catch (Exception ex) when (ex is InvalidArgumentException
								|| ex is ResourceConflictException)
		{
			return BadRequest(ex.Message);
		}
		catch (Exception ex)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, $"An unexpected error occurred: {ex.Message}");
		}
	}
}
