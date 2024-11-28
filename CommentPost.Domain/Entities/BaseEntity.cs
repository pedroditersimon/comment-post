namespace CommentPost.Domain.Entities;

public class BaseEntity<Tid>
{
	public required Tid ID { get; set; }

	public DateTime CreationDate { get; set; }
	public DateTime LastUpdateDate { get; set; }
	public bool IsDeleted { get; set; }
}
