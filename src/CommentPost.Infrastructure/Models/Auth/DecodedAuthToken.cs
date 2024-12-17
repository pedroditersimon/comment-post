using CommentPost.Domain.Enums;

namespace CommentPost.Infrastructure.Models.Auth;

public class DecodedAuthToken
{
	public int UserId { get; set; }
	public Role Role { get; set; }
}
