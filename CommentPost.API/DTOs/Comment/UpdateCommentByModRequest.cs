namespace CommentPost.API.DTOs.Comment;

public class UpdateCommentByModRequest
{
	public int CommentId { get; set; }

	// replaced values
	public int? UserId { get; set; }
	public string? PageId { get; set; }
	public string? Text { get; set; }
	public bool? Visibility { get; set; }
	public int? ReplyId { get; set; }
}
