namespace CommentPost.Application.Filters;

public class PaginationResult<T> : Pagination
{
	public IQueryable<T> Elements { get; }

	public long Count => Elements.Count();

	public PaginationResult(IQueryable<T> elements, Pagination pagination)
	{
		Offset = pagination.Offset;
		Limit = pagination.Limit;
		Elements = elements;
	}
}
