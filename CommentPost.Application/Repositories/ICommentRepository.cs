using CommentPost.Application.Filters;
using CommentPost.Domain.Entities;

namespace CommentPost.Application.Repositories;

public interface ICommentRepository
{
	// Get
	public PaginationResult<User> GetAll(Pagination pagination);
	public Comment? GetById(int id);
	public Comment? GetByPageId(string pageId);
	public Comment? GetByReplyId(int replyId);
	public PaginationResult<Comment> SearchByText(string text, Pagination pagination);

	// Create
	public Comment? Create(Comment comment);

	// Update
	public Comment? Update(Comment comment);

	// Delete
	public bool SoftDelete(int id);
}
