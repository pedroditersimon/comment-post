using CommentPost.Application.Filters;
using CommentPost.Application.Repositories;
using CommentPost.Domain.Entities;

namespace CommentPost.Infrastructure.EntityFramework.Repositories;

/// <summary>
/// EntityFramework based repository </summary>
public class CommentRepository : GenericRepository<Comment, int>, ICommentRepository
{

	public CommentRepository(ApplicationDBContext dbContext)
		: base(dbContext)
	{
	}

	public Task<PaginationResult<Comment>> GetAllByPageId(string pageId, Pagination pagination)
	{
		throw new NotImplementedException();
	}

	public Task<PaginationResult<Comment>> GetAllByReplyId(int replyId, Pagination pagination)
	{
		throw new NotImplementedException();
	}


	public Task<PaginationResult<Comment>> SearchByText(string text, Pagination pagination)
	{
		throw new NotImplementedException();
	}

}
