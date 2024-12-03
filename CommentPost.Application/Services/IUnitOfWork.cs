using CommentPost.Application.Repositories;

namespace CommentPost.Application.Services;

public interface IUnitOfWork : IDisposable
{
	public IUserRepository UserRepository { get; }
	public ICommentRepository CommentRepository { get; }

	/// <summary>
	/// Applies all changes. </summary>
	public Task<bool> ApplyChangesAsync();
}
