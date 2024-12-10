using CommentPost.Domain.Constants;
using CommentPost.Domain.Enums;

namespace CommentPost.Domain.Entities;

public class User : BaseEntity<int>
{
	// auth
	public string AuthProvider { get; set; } = AuthProviders.Local;

	// external auth
	public string? ExternalId { get; set; }

	// local auth
	public string? Username { get; set; }
	public string? PasswordHash { get; set; }

	// role
	public Role Role { get; set; }

	// User details
	public string DisplayName { get; set; } = "User";
	public string LastLoginAt { get; set; }
	public string? ProfilePhotoUrl { get; set; }

}

