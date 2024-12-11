namespace CommentPost.API.DTOs.Auth;

public class LocalLoginRequest
{
	public required string Username { get; set; }
	public required string Password { get; set; }
}
