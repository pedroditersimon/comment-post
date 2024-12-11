using CommentPost.Application.Exceptions;
using CommentPost.Application.Services;
using CommentPost.Domain.Constants;
using CommentPost.Domain.Entities;
using CommentPost.Domain.Enums;
using CommentPost.Infrastructure.Models.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace CommentPost.Infrastructure.Services.Auth;
public class AuthService
{
	readonly IUserService _userService;
	readonly JwtService _jwtService;
	readonly IUnitOfWork _unitOfWork;

	public AuthService(IUserService userService, JwtService jwtService, IUnitOfWork unitOfWork)
	{
		_userService = userService;
		_jwtService = jwtService;
		_unitOfWork = unitOfWork;
	}

	public async Task<User?> RegisterUser(User user)
	{
		User? existingUser = user.AuthProvider == AuthProviders.Local
			? await _userService.GetByUsername(user.Username)
			: await _userService.GetByExternalId(user.ExternalId);

		if (existingUser != null)
			throw new ResourceConflictException("The user already exists.");

		User? createdUser = await _userService.Create(user);
		if (createdUser == null)
			throw new CreateResourceException();

		bool saved = await _unitOfWork.ApplyChangesAsync();
		if (!saved)
			throw new SaveChangesException();

		return user.AuthProvider == AuthProviders.Local
			? await _userService.GetByUsername(user.Username)
			: await _userService.GetByExternalId(user.ExternalId);
	}

	public AuthToken CreateAuthToken(User user)
	{
		string token = _jwtService.CreateEncodedToken(
			new Dictionary<string, object>()
			{
				["userId"] = user.ID.ToString(),
				["role"] = user.Role.ToString()
			}
		);
		return new AuthToken() { Token = token };
	}

	public DecodedAuthToken? DecodeAuthToken(AuthToken token)
	{
		JwtSecurityToken? jwtSecurityToken = _jwtService.VerifyToken(token.Token);
		if (jwtSecurityToken == null)
			return null;

		JwtPayload payload = jwtSecurityToken.Payload;

		var decodedToken = new DecodedAuthToken
		{
			UserId = payload["userId"]?.ToString() ?? string.Empty,
			Role = Enum.TryParse<Role>(payload["role"]?.ToString(), out var role) ? role : Role.User
		};

		return decodedToken;
	}

	public string HashString(string text)
		=> Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(text)));

}
