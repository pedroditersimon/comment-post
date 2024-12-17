using CommentPost.Domain.Entities;

namespace CommentPost.Application.Repositories;

public interface ICommentRepository : IGenericRepository<Comment, int>
{
	// Get
	public Task<IQueryable<Comment>> GetAllByPageId(string pageId);
	public Task<IQueryable<Comment>> GetAllByReplyId(int replyId);
	public Task<IQueryable<Comment>> SearchByText(string text);


	// Create


	// Update


	// Delete

}
