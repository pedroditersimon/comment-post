using CommentPost.Application.Filters;
using CommentPost.Domain.Entities;

namespace CommentPost.Application.Repositories;

public interface ICommentRepository
{
	// Get
	public Comment? GetById(int id);
	public PaginationResult<Comment> GetAll(Pagination pagination);
	public PaginationResult<Comment> GetAllByPageId(string pageId, Pagination pagination);
	public PaginationResult<Comment> GetAllByReplyId(int replyId, Pagination pagination);
	public PaginationResult<Comment> SearchByText(string text, Pagination pagination);

	// Create
	public Comment? Create(Comment comment);

	// Update
	public Comment? Update(Comment comment);

	// Delete
	public bool SoftDelete(int id);
}
