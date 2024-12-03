using CommentPost.Application.Repositories;
using CommentPost.Application.Services;

namespace CommentPost.Infrastructure.EntityFramework;

public class UnitOfWork : IUnitOfWork
{
	readonly ApplicationDBContext _dbContext;

	public IUserRepository UserRepository { get; }
	public ICommentRepository CommentRepository { get; }


	public UnitOfWork(ApplicationDBContext dbContext,
		IUserRepository userRepository, ICommentRepository commentRepository)
	{
		_dbContext = dbContext;

		UserRepository = userRepository;
		CommentRepository = commentRepository;
	}


	public void Dispose()
	{
		_dbContext.Dispose();
		GC.SuppressFinalize(this);
	}

	public async Task<bool> ApplyChangesAsync()
	{
		return await _dbContext.SaveChangesAsync() > 0;
	}
}
