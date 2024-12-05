using CommentPost.Application.Filters;
using CommentPost.Application.Mappers;
using CommentPost.Application.Repositories;
using CommentPost.Domain.Entities;

namespace CommentPost.Application.Services;

public class GenericService<T, Tid> : IGenericService<T, Tid>
		where T : BaseEntity<Tid>
{
	protected readonly IGenericRepository<T, Tid> _repository;

	public GenericService(IGenericRepository<T, Tid> repository)
	{
		_repository = repository;
	}


	// Get
	public async Task<PaginationResult<T>> GetAll(Pagination pagination)
	{
		IQueryable<T> elements = await _repository.GetAll();
		return pagination.GetPaged(elements);
	}

	public async Task<T?> GetById(Tid id)
	{
		return await _repository.GetById(id);
	}



	// Create
	public async Task<T?> Create(T entity)
	{
		return await _repository.Create(entity);
	}


	// Update
	public async Task<T?> Update(T entity)
	{
		return await _repository.Update(entity);
	}


	// Delete
	public async Task<bool> SoftDelete(Tid id)
	{
		return await _repository.SoftDelete(id);
	}

}
