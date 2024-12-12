namespace CommentPost.API.DTOs.Comment;

public class PostNewReplyCommentRequest
{
	public string Text { get; set; }
	public int ReplyId { get; set; }
}
