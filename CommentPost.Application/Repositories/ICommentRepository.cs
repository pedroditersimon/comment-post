using CommentPost.Application.Filters;
using CommentPost.Domain.Entities;

namespace CommentPost.Application.Repositories;

public interface ICommentRepository : IGenericRepository<Comment, int>
{
	// Get
	public Task<PaginationResult<Comment>> GetAllByPageId(string pageId, Pagination pagination);
	public Task<PaginationResult<Comment>> GetAllByReplyId(int replyId, Pagination pagination);
	public Task<PaginationResult<Comment>> SearchByText(string text, Pagination pagination);


	// Create


	// Update


	// Delete

}
