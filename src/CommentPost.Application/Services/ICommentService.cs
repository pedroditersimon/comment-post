using CommentPost.Application.Filters;
using CommentPost.Domain.Entities;

namespace CommentPost.Application.Services;

public interface ICommentService : IGenericService<Comment, int>
{
	// Get
	public Task<PaginationResult<Comment>> GetAllByPageId(string pageId, Pagination pagination);
	public Task<PaginationResult<Comment>> GetAllByReplyId(int replyId, Pagination pagination);
	public Task<PaginationResult<Comment>> SearchByText(string text, Pagination pagination);

}
