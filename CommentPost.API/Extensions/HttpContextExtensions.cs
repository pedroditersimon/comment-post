using CommentPost.Infrastructure.Models.Auth;

namespace CommentPost.API.Extensions;

public static class HttpContextExtensions
{
	public static bool TryGetDecodedAuthToken(this HttpContext httpContext, out DecodedAuthToken decodedToken)
	{
		decodedToken = httpContext.Items["DecodedToken"] as DecodedAuthToken;
		return decodedToken != null;
	}
}
