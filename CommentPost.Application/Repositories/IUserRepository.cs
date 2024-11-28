using CommentPost.Domain.Entities;

namespace CommentPost.Application.Repositories;

public interface IUserRepository
{
	// Get
	public IQueryable<User> GetAll();
	public User? GetById(int id);

	// Create
	public User? Create(User user);

	// Update
	public User? Update(User user);

	// Delete
	public bool SoftDelete(int id);
}
