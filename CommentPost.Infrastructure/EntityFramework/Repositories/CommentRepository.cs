using CommentPost.Application.Filters;
using CommentPost.Application.Mappers;
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

	public async Task<PaginationResult<Comment>> GetAllByPageId(string pageId, Pagination pagination)
	{
		IQueryable<Comment> query = Entities
			.Where(comment => comment.PageId == pageId);

		return pagination.GetPaged(query);
	}

	public async Task<PaginationResult<Comment>> GetAllByReplyId(int replyId, Pagination pagination)
	{
		IQueryable<Comment> query = Entities
			.Where(comment => comment.ReplyId == replyId);

		return pagination.GetPaged(query);
	}


	public Task<PaginationResult<Comment>> SearchByText(string text, Pagination pagination)
	{
		throw new NotImplementedException();
	}

}
