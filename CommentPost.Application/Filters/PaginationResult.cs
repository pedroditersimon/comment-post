using System.Collections.Immutable;

namespace CommentPost.Application.Filters;

public class PaginationResult<T> : Pagination
{
	public required ImmutableArray<T> Elements { get; set; }

	public long Count => Elements.Length;
}
