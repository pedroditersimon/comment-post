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

	public async Task<IQueryable<Comment>> GetAllByPageId(string pageId)
	{
		return Entities
			.Where(comment => comment.PageId == pageId);
	}

	public async Task<IQueryable<Comment>> GetAllByReplyId(int replyId)
	{
		return Entities
			.Where(comment => comment.ReplyId == replyId);
	}


	public Task<IQueryable<Comment>> SearchByText(string text)
	{
		throw new NotImplementedException();
	}

}
