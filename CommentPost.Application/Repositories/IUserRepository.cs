using CommentPost.Domain.Entities;

namespace CommentPost.Application.Repositories;

public interface IUserRepository : IGenericRepository<User, int>
{
	// Get
	public Task<User?> GetByExternalId(string externalId);
	public Task<User?> GetByUsername(string username);

	// Create


	// Update


	// Delete

}
