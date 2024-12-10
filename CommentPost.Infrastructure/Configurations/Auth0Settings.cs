namespace CommentPost.Infrastructure.Configurations;

public class Auth0Settings
{
	public string BaseUrl { get; set; }
	public string ClientId { get; set; }
	public string ClientSecret { get; set; }
	public string RedirectUri { get; set; }
}
