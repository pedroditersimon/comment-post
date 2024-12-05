using CommentPost.Application.Filters;
using CommentPost.Application.Mappers;
using CommentPost.Application.Repositories;
using CommentPost.Domain.Entities;

namespace CommentPost.Application.Services;

public class CommentService : GenericService<Comment, int>, ICommentService
{

	readonly IUnitOfWork _unityOfWork;
	readonly ICommentRepository _commentRepository;

	public CommentService(IUnitOfWork unitOfWork)
		: base(unitOfWork.CommentRepository)
	{
		_commentRepository = unitOfWork.CommentRepository;
		_unityOfWork = unitOfWork;
	}


	// Get
	public async Task<PaginationResult<Comment>> GetAllByPageId(string pageId, Pagination pagination)
	{
		IQueryable<Comment> elements = await _commentRepository.GetAllByPageId(pageId);
		IQueryable<Comment> sortedElements = elements.OrderBy(e => e.CreationDate);
		return pagination.GetPaged(sortedElements);
	}

	public async Task<PaginationResult<Comment>> GetAllByReplyId(int replyId, Pagination pagination)
	{
		IQueryable<Comment> elements = await _commentRepository.GetAllByReplyId(replyId);
		IQueryable<Comment> sortedElements = elements.OrderBy(e => e.CreationDate);
		return pagination.GetPaged(sortedElements);
	}

	public async Task<PaginationResult<Comment>> SearchByText(string text, Pagination pagination)
	{
		IQueryable<Comment> elements = await _commentRepository.SearchByText(text);
		IQueryable<Comment> sortedElements = elements.OrderBy(e => e.CreationDate);
		return pagination.GetPaged(sortedElements);
	}
}
