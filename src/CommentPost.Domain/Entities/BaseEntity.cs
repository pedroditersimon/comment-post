using System.ComponentModel.DataAnnotations;

namespace CommentPost.Domain.Entities;

public class BaseEntity<Tid> : ICloneable
{
	public Tid ID { get; set; }

	public DateTime CreationDate { get; set; }
	[ConcurrencyCheck]
	public DateTime LastUpdatedDate { get; set; }
	public bool IsDeleted { get; set; }

	public object Clone() => this.MemberwiseClone();
}
