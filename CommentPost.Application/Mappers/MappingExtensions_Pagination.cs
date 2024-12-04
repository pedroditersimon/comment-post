using CommentPost.Application.Filters;

namespace CommentPost.Application.Mappers;

public static class MappingExtensions_Pagination
{
	public static PaginationResult<T> GetPaged<T>(this Pagination pagination, IQueryable<T> elements)
	{
		IQueryable<T> query = elements
			.Skip(pagination.Offset)
			.Take(pagination.Limit);

		return new PaginationResult<T>(query, pagination);
	}
}
