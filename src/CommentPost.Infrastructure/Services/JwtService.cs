using CommentPost.Infrastructure.Configurations;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CommentPost.Infrastructure.Services;

public class JwtService
{
	readonly JwtSettings _settings;

	public JwtService(JwtSettings settings)
	{
		_settings = settings;
	}

	public string CreateEncodedToken(Dictionary<string, object> claims)
	{
		// create key
		byte[] key = Encoding.UTF8.GetBytes(_settings.SecretKey);

		SigningCredentials signingCredentials = new(
			new SymmetricSecurityKey(key),
			SecurityAlgorithms.HmacSha256
		);

		// payload
		SecurityTokenDescriptor tokenDescriptor = new()
		{
			IssuedAt = DateTime.UtcNow,
			Claims = claims,
			Expires = DateTime.UtcNow.AddMinutes(_settings.ExpiresInMinutes),
			Issuer = _settings.Issuer,
			SigningCredentials = signingCredentials,
		};

		// create token
		JwtSecurityTokenHandler jwtHandler = new();
		SecurityToken token = jwtHandler.CreateToken(tokenDescriptor);
		string encodedToken = jwtHandler.WriteToken(token);

		return encodedToken;
	}

	/// <summary>
	/// Verifies and decodes a JWT token. </summary>
	/// <returns>
	/// Decoded token if verification is successful; otherwise, null. </returns>
	public JwtSecurityToken? VerifyToken(string token)
	{
		TokenValidationParameters validation = new()
		{
			// issuer
			ValidateIssuer = true,
			ValidIssuer = _settings.Issuer,

			// audience
			ValidateAudience = false,

			// secretKey
			ValidateIssuerSigningKey = true,
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SecretKey)),

			// expiration
			ValidateLifetime = true,
			ClockSkew = TimeSpan.Zero
		};

		try
		{
			JwtSecurityTokenHandler jwtHandler = new();
			ClaimsPrincipal claims = jwtHandler.ValidateToken(token, validation, out SecurityToken securityToken);

			// try convert
			if (securityToken is JwtSecurityToken jwtToken)
			{
				return jwtToken;
			}

			return null;
		}

		catch (SecurityTokenInvalidSignatureException)
		{
			return null;
		}
		catch (SecurityTokenExpiredException)
		{
			return null;
		}
		catch (Exception)
		{
			return null;
		}
	}



	#region Static utils
	/// <summary>
	/// Decode a token without verifyng the signature </summary>
	public static JwtSecurityToken? DecodeToken(string token)
	{
		try
		{
			JwtSecurityTokenHandler jwtHandler = new();
			JwtSecurityToken decoded = jwtHandler.ReadJwtToken(token);
			return decoded;
		}
		catch (System.ArgumentException)
		{
			return null;
		}
	}
	#endregion
}
