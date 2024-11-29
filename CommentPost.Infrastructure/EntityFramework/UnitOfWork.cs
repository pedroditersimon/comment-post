using CommentPost.Application.Services;

namespace CommentPost.Infrastructure.EntityFramework;

public class UnitOfWork : IUnitOfWork
{
	readonly ApplicationDBContext _dbContext;

	public UnitOfWork(ApplicationDBContext dbContext)
	{
		_dbContext = dbContext;
	}

	public void Dispose()
	{
		_dbContext.Dispose();
	}

	public async Task<bool> SaveAsync()
	{
		return await _dbContext.SaveChangesAsync() > 0;
	}
}
