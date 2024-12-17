using CommentPost.Infrastructure.Models.Auth;

namespace CommentPost.API.Extensions;

public static class HttpRequestExtensions
{

	public static AuthToken? GetAuthToken(this HttpRequest request)
	{
		string authorization = request.Headers.Authorization.ToString();

		if (string.IsNullOrWhiteSpace(authorization)
			|| !authorization.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
			return null;

		string token = authorization.Substring("Bearer ".Length).Trim();
		return new AuthToken() { Token = token };
	}
}
