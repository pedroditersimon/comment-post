namespace CommentPost.Application.DTOs.Comment;

public class CommentResponse : ICloneable
{
	public int ID { get; set; }

	public DateTime CreationDate { get; set; }
	public DateTime LastUpdatedDate { get; set; }


	public int UserId { get; set; }

	public string PageId { get; set; }

	public string Text { get; set; }
	public bool Visibility { get; set; } = true;

	public int? ReplyId { get; set; }

	public object Clone()
	{
		return this.MemberwiseClone();
	}
}
