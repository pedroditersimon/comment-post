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

	public static PaginationResult<T2> Map<T1, T2>(this PaginationResult<T1> paginationResult, Func<T1, T2> map)
	{
		// map
		IQueryable<T2> elements = paginationResult.Elements
			.Select(elem => map(elem));

		Pagination pagination = paginationResult;
		return new PaginationResult<T2>(elements, pagination);
	}
}
