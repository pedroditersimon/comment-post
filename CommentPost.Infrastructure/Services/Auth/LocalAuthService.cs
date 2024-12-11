using CommentPost.Application.Exceptions;
using CommentPost.Application.Services;
using CommentPost.Domain.Constants;
using CommentPost.Domain.Entities;
using CommentPost.Domain.Enums;
using CommentPost.Infrastructure.Models.Auth;

namespace CommentPost.Infrastructure.Services.Auth;

public class LocalAuthService
{
	readonly IUserService _userService;
	readonly AuthService _authService;

	public LocalAuthService(IUserService userService, AuthService authService)
	{
		_userService = userService;
		_authService = authService;
	}


	public async Task<AuthToken> Login(string username, string password)
	{
		if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
			throw new InvalidArgumentException();

		User? user = await _userService.GetByUsername(username);
		if (user == null || user.Username == null)
			throw new NotFoundException("Invalid username");

		// create password hash
		string passwordHash = _authService.HashString(password);

		// validate
		if (user.PasswordHash != passwordHash)
			throw new InvalidCredentialsException("Invalid password");

		// return jwt
		return _authService.CreateAuthToken(user);
	}

	public async Task<AuthToken> Register(string username, string password)
	{
		if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
			throw new InvalidArgumentException();

		User? registeredUser = await _authService.RegisterUser(new User()
		{
			AuthProvider = AuthProviders.Local,
			CreationDate = DateTime.UtcNow,
			LastLoginAt = DateTime.UtcNow,
			Role = Role.User,
			PasswordHash = _authService.HashString(password),
			Username = username
		});

		// return jwt
		return _authService.CreateAuthToken(registeredUser);
	}


}
