using System.Text.Json.Serialization;

namespace CommentPost.Infrastructure.Models;

public class Auth0TokenResponse
{
	[JsonPropertyName("access_token")]
	public string AccessToken { get; set; }

	[JsonPropertyName("id_token")]
	public string IdToken { get; set; }

	public string Scope { get; set; }

	[JsonPropertyName("expires_in")]
	public int ExpiresIn { get; set; }

	[JsonPropertyName("token_type")]
	public string TokenType { get; set; }
}
