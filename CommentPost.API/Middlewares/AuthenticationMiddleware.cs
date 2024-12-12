
using CommentPost.API.Attributes;
using CommentPost.API.Extensions;
using CommentPost.Infrastructure.Models.Auth;
using CommentPost.Infrastructure.Services.Auth;

namespace CommentPost.API.Middlewares;

public class AuthenticationMiddleware : IMiddleware
{
	readonly AuthService _authService;

	public AuthenticationMiddleware(AuthService authService)
	{
		_authService = authService;
	}


	public async Task InvokeAsync(HttpContext context, RequestDelegate next)
	{
		var authorizationAttribute = context.GetEndpoint()?.Metadata.GetMetadata<NeedAuthorizationAttribute>();

		// no need authorization
		if (authorizationAttribute == null)
		{
			await next(context);
			return;
		}

		bool needRole = authorizationAttribute.Roles.Length > 0;

		// extract token from request header
		AuthToken? authToken = context.Request.GetAuthToken();
		if (authToken == null)
		{
			context.Response.StatusCode = StatusCodes.Status401Unauthorized;
			await context.Response.WriteAsync("Cannot GetAuthToken");
			return;
		}

		// verify and decode
		DecodedAuthToken? decodedToken = _authService.DecodeAuthToken(authToken);
		if (decodedToken == null)
		{
			context.Response.StatusCode = StatusCodes.Status401Unauthorized;
			await context.Response.WriteAsync("Cannot DecodeAuthToken");
			return;
		}

		// verify role
		if (needRole)
		{
			bool hasRole = authorizationAttribute.Roles.Contains(decodedToken.Role);
			if (!hasRole)
			{
				context.Response.StatusCode = StatusCodes.Status401Unauthorized;
				await context.Response.WriteAsync("Not authorized");
				return;
			}
		}

		// add token to the context so controllers can use it
		context.Items["DecodedToken"] = decodedToken;

		await next(context);
	}
}
