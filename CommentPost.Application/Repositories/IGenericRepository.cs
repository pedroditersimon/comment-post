using CommentPost.Application.Filters;

namespace CommentPost.Application.Repositories;

public interface IGenericRepository<T, Tid>
{
	// Get
	public Task<T?> GetById(Tid id);
	public Task<PaginationResult<T>> GetAll(Pagination pagination);

	// Create
	public Task<T?> Create(T comment);

	// Update
	public Task<T?> Update(T comment);

	// Delete
	public Task<bool> SoftDelete(Tid id);
}
