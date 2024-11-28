using CommentPost.Application.Filters;
using CommentPost.Application.Repositories;
using CommentPost.Domain.Entities;

namespace CommentPost.Application.UseCases;

// Leer comentarios: Visualizar los comentarios publicados en una página.

public class GetCommentsByPageIdRequest : Pagination
{
	public string PageId { get; set; }
}

public class GetCommentsByPageId
{
	readonly ICommentRepository _commentRepository;

	public GetCommentsByPageId(ICommentRepository commentRepository)
	{
		_commentRepository = commentRepository;
	}


	public async Task<PaginationResult<Comment>> ExecuteAsync(GetCommentsByPageIdRequest request)
	{
		Pagination pagination = request;

		return await _commentRepository.GetAllByPageId(request.PageId, pagination);
	}
}
