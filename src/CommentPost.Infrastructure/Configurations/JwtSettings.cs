namespace CommentPost.Infrastructure.Configurations;

public class JwtSettings
{
	public string SecretKey { get; set; }
	public int ExpiresInMinutes { get; set; }
	public string Issuer { get; set; }
}
