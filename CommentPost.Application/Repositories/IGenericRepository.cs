using CommentPost.Domain.Entities;

namespace CommentPost.Application.Repositories;

public interface IGenericRepository<T, Tid>
		where T : BaseEntity<Tid>
{
	// Get
	public Task<T?> GetById(Tid id);
	public Task<IQueryable<T>> GetAll();

	// Create
	public Task<T?> Create(T entity);

	// Update
	public Task<T?> Update(T entity);

	// Delete
	public Task<bool> SoftDelete(Tid id);
}
