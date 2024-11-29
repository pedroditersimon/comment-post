using CommentPost.Application.Filters;
using CommentPost.Application.Repositories;
using CommentPost.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CommentPost.Infrastructure.EntityFramework.Repositories;

/// <summary>
/// EntityFramework based repository </summary>
public class CommentRepository : ICommentRepository
{

	readonly ApplicationDBContext _dbContext;
	DbSet<Comment> Comments => _dbContext.Set<Comment>();

	public CommentRepository(ApplicationDBContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task<Comment?> Create(Comment comment)
	{
		comment.CreationDate = DateTime.UtcNow;
		return Comments.Add(comment).Entity;
	}

	public async Task<PaginationResult<Comment>> GetAll(Pagination pagination)
	{
		throw new NotImplementedException();
	}

	public Task<PaginationResult<Comment>> GetAllByPageId(string pageId, Pagination pagination)
	{
		throw new NotImplementedException();
	}

	public Task<PaginationResult<Comment>> GetAllByReplyId(int replyId, Pagination pagination)
	{
		throw new NotImplementedException();
	}

	public async Task<Comment?> GetById(int id)
	{
		return await Comments.SingleOrDefaultAsync(t => t.ID.Equals(id));
	}

	public Task<PaginationResult<Comment>> SearchByText(string text, Pagination pagination)
	{
		throw new NotImplementedException();
	}

	public async Task<bool> SoftDelete(int id)
	{
		Comment? entity = await GetById(id);
		if (entity == null)
			return false;

		entity.IsDeleted = true;
		//entity.LastDeletedTime = DateTime.UtcNow;
		await Update(entity);

		return true;
	}

	public async Task<Comment?> Update(Comment comment)
	{
		Comment? existingEntity = await GetById(comment.ID);
		if (existingEntity == null)
			return null;

		comment.LastUpdatedDate = DateTime.UtcNow;
		_dbContext.Entry(existingEntity).CurrentValues.SetValues(comment);
		return existingEntity;
	}
}
