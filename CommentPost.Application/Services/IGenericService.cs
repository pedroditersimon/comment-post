using CommentPost.Application.Filters;
using CommentPost.Domain.Entities;

namespace CommentPost.Application.Services;

public interface IGenericService<T, Tid>
		where T : BaseEntity<Tid>
{
	// Get
	public Task<T?> GetById(Tid id);
	public Task<PaginationResult<T>> GetAll(Pagination pagination);

	// Create
	public Task<T?> Create(T entity);

	// Update
	public Task<T?> Update(T entity);

	// Delete
	public Task<bool> SoftDelete(Tid id);
}
