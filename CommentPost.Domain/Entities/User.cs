using CommentPost.Domain.Enums;

namespace CommentPost.Domain.Entities;

public class User : BaseEntity<int>
{
	public string Name { get; set; }

	// public string Token { get; set; }

	public Role Role { get; set; }

}

