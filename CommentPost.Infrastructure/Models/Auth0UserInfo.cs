using System.Text.Json.Serialization;

namespace CommentPost.Infrastructure.Models;

public class Auth0UserInfo
{
	public string Sub { get; set; }
	public string Nickname { get; set; }
	public string Name { get; set; }
	public string Picture { get; set; }

	[JsonPropertyName("updated_at")]
	public DateTime UpdatedAt { get; set; }
}
