namespace CommentPost.Domain.Entities;

public class Comment : BaseEntity<int>
{
	public User User { get; set; }
	public int UserId { get; set; }

	public string PageId { get; set; }

	public string Text { get; set; }
	public bool Visibility { get; set; } = true;

	public Comment? Reply { get; set; }
	public int? ReplyId { get; set; }
}
