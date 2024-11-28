namespace CommentPost.Domain.Entities;

public class Comment : BaseEntity<int>
{
	public required User User { get; set; }
	public required int UserId { get; set; }

	public required string PageId { get; set; }

	public required string Text { get; set; }
	public bool Visibility { get; set; } = true;

	public Comment? Reply { get; set; }
	public int? ReplyId { get; set; }
}
