using CommentPost.Application.Filters;
using CommentPost.Domain.Entities;

namespace CommentPost.Application.Repositories;

public interface IUserRepository
{
	// Get
	public Task<PaginationResult<User>> GetAll(Pagination pagination);
	public Task<User?> GetById(int id);

	// Create
	public Task<User?> Create(User user);

	// Update
	public Task<User?> Update(User user);

	// Delete
	public Task<bool> SoftDelete(int id);
}
