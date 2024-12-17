using CommentPost.Domain.Entities;

namespace CommentPost.Application.Services;

public interface IUserService : IGenericService<User, int>
{
	// Get
	public Task<User?> GetByExternalId(string externalId);
	public Task<User?> GetByUsername(string username);
}
