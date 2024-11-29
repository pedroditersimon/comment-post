namespace CommentPost.Application.Services;

public interface IUnitOfWork : IDisposable
{
	/// <summary>
	/// Applies all database changes. </summary>
	public Task<bool> SaveAsync();
}
