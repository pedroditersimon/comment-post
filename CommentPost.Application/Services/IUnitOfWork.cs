namespace CommentPost.Application.Services;

public interface IUnitOfWork
{
	/// <summary>
	/// Applies all database changes. </summary>
	public Task<bool> SaveAsync();
}
