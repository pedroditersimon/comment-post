using CommentPost.Application.Repositories;
using CommentPost.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CommentPost.Infrastructure.EntityFramework.Repositories;

public class UserRepository : GenericRepository<User, int>, IUserRepository
{
	public UserRepository(ApplicationDBContext dbContext)
		: base(dbContext)
	{
	}

	public async Task<User?> GetByExternalId(string externalId)
	{
		return await Entities
			.SingleOrDefaultAsync(t => t.ExternalId != null && t.ExternalId.Equals(externalId));
	}

	public async Task<User?> GetByUsername(string username)
	{
		return await Entities
			.SingleOrDefaultAsync(t => t.Username != null && t.Username.Equals(username));
	}
}
