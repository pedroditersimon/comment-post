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
	public async Task<IQueryable<T>> GetAll()
	{
		return Entities;
	}

	public async Task<T?> GetById(Tid id)
	{
		return await Entities.SingleOrDefaultAsync(t => t.ID.Equals(id));
	}



	// Create
	public async Task<T?> Create(T entity)
	{
		entity.CreationDate = DateTime.UtcNow;
		return Entities.Add(entity).Entity;
	}


	// Update
	public async Task<T?> Update(T entity)
	{
		T? existingEntity = await GetById(entity.ID);
		if (existingEntity == null)
			return null;

		entity.LastUpdatedDate = DateTime.UtcNow;
		_dbContext.Entry(existingEntity).CurrentValues.SetValues(entity);
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
