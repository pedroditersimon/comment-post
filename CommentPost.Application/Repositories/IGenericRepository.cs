using CommentPost.Domain.Entities;

namespace CommentPost.Application.Repositories;

public interface IGenericRepository<T, Tid>
		where T : BaseEntity<Tid>
{
	// Get
	public Task<T?> GetById(Tid id);
	// Even if an <Iqueryable> is returned, <Task> is maintained by abstraction
	public Task<IQueryable<T>> GetAll();

	// Create
	public Task<T?> Create(T entity);

	// Update
	public Task<T?> Update(T entity);

	// Delete
	public Task<bool> SoftDelete(Tid id);
}
