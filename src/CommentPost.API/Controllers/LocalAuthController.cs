using CommentPost.API.DTOs.Auth;
using CommentPost.Application.Exceptions;
using CommentPost.Application.Services;
using CommentPost.Infrastructure.Models.Auth;
using CommentPost.Infrastructure.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace CommentPost.API.Controllers;

[ApiController]
[Route("auth")]
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
		if (string.IsNullOrWhiteSpace(loginRequest?.Username)
			|| string.IsNullOrWhiteSpace(loginRequest?.Password))
			return BadRequest("Username and password is required.");

		try
		{
			AuthToken token = await _localAuthService.Login(loginRequest.Username, loginRequest.Password);
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
	public async Task<ActionResult<AuthToken>> Register([FromBody] LocalLoginRequest loginRequest)
	{
		if (string.IsNullOrWhiteSpace(loginRequest?.Username)
			|| string.IsNullOrWhiteSpace(loginRequest?.Password))
			return BadRequest("Username and password is required.");

		try
		{
			AuthToken token = await _localAuthService.Register(loginRequest.Username, loginRequest.Password);
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
