using CommentPost.Application.Filters;
using CommentPost.Application.Repositories;
using CommentPost.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CommentPost.Infrastructure.EntityFramework.Repositories;

public class GenericRepository<T, Tid> : IGenericRepository<T, Tid>
	where T : BaseEntity<Tid>
{
	protected readonly ApplicationDBContext _dbContext;
	protected DbSet<T> Entities => _dbContext.Set<T>();

	public GenericRepository(ApplicationDBContext dbContext)
	{
		_dbContext = dbContext;
	}


	// Get
	public async Task<PaginationResult<T>> GetAll(Pagination pagination)
	{
		throw new NotImplementedException();
	}

	public async Task<T?> GetById(Tid id)
	{
		return await Entities.SingleOrDefaultAsync(t => t.ID.Equals(id));
	}



	// Create
	public async Task<T?> Create(T comment)
	{
		comment.CreationDate = DateTime.UtcNow;
		return Entities.Add(comment).Entity;
	}


	// Update
	public async Task<T?> Update(T comment)
	{
		T? existingEntity = await GetById(comment.ID);
		if (existingEntity == null)
			return null;

		comment.LastUpdatedDate = DateTime.UtcNow;
		_dbContext.Entry(existingEntity).CurrentValues.SetValues(comment);
		return existingEntity;
	}


	// Delete
	public async Task<bool> SoftDelete(Tid id)
	{
		T? entity = await GetById(id);
		if (entity == null)
			return false;

		entity.IsDeleted = true;
		//entity.LastDeletedTime = DateTime.UtcNow;
		await Update(entity);

		return true;
	}

}
