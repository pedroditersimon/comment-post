using CommentPost.Application.Repositories;
using CommentPost.Domain.Entities;

namespace CommentPost.Infrastructure.EntityFramework.Repositories;

public class UserRepository : GenericRepository<User, int>, IUserRepository
{
	public UserRepository(ApplicationDBContext dbContext)
		: base(dbContext)
	{
	}
}
