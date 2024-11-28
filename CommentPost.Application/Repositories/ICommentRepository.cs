using CommentPost.Application.Filters;
using CommentPost.Domain.Entities;

namespace CommentPost.Application.Repositories;

public interface ICommentRepository
{
	// Get
	public Task<Comment?> GetById(int id);
	public Task<PaginationResult<Comment>> GetAll(Pagination pagination);
	public Task<PaginationResult<Comment>> GetAllByPageId(string pageId, Pagination pagination);
	public Task<PaginationResult<Comment>> GetAllByReplyId(int replyId, Pagination pagination);
	public Task<PaginationResult<Comment>> SearchByText(string text, Pagination pagination);

	// Create
	public Task<Comment?> Create(Comment comment);

	// Update
	public Task<Comment?> Update(Comment comment);

	// Delete
	public Task<bool> SoftDelete(int id);
}
