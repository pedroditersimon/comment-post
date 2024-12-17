using CommentPost.Application.Exceptions;
using CommentPost.Application.Services;
using CommentPost.Domain.Constants;
using CommentPost.Domain.Entities;
using CommentPost.Domain.Enums;
using CommentPost.Infrastructure.Models.Auth;
using System.Security.Cryptography;
using System.Text;

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


	/// <exception cref="InvalidArgumentException" />
	/// <exception cref="InvalidCredentialsException" />
	public async Task<AuthToken> Login(string username, string password)
	{
		if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
			throw new InvalidArgumentException("Username and password cannot be empty or whitespace.");

		User? user = await _userService.GetByUsername(username);
		if (user == null || user.Username == null)
			throw new InvalidCredentialsException("Invalid username or password.");

		// validate
		if (!VerifyPassword(password, user.PasswordHash))
			throw new InvalidCredentialsException("Invalid username or password.");

		// login user
		AuthToken token = await _authService.LoginUser(user);
		return token;
	}


	/// <exception cref="InvalidArgumentException" />
	public async Task<AuthToken> Register(string username, string password)
	{
		if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
			throw new InvalidArgumentException();

		// register user
		AuthToken token = await _authService.RegisterUser(new User()
		{
			AuthProvider = AuthProviders.Local,
			Role = Role.User,
			PasswordHash = _authService.HashString(password),
			Username = username
		});

		return token;
	}


	/// <summary>
	/// Verify the password against the stored hash </summary>
	/// <exception cref="ArgumentException" />
	public bool VerifyPassword(string password, string storedHash)
	{
		if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(storedHash))
			throw new ArgumentException("Password and stored hash cannot be null or empty.");

		// Generate hash for the input password
		string passwordHash = _authService.HashString(password);

		// Securely compare the hashes to prevent timing attacks
		return CryptographicOperations.FixedTimeEquals(
			Encoding.UTF8.GetBytes(passwordHash),
			Encoding.UTF8.GetBytes(storedHash)
		);
	}

}
