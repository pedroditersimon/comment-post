using CommentPost.Application.Filters;
using CommentPost.Application.Repositories;
using CommentPost.Domain.Entities;

namespace CommentPost.Application.UseCases;

// Leer comentarios: Visualizar los comentarios publicados en una página.

public class GetCommentsByPageIdCommand : Pagination
{
	public string PageId { get; set; }
}

public class GetCommentsByPageIdHandler
{
	readonly ICommentRepository _commentRepository;

	public GetCommentsByPageIdHandler(ICommentRepository commentRepository)
	{
		_commentRepository = commentRepository;
	}


	public async Task<PaginationResult<Comment>> ExecuteAsync(GetCommentsByPageIdCommand command)
	{
		Pagination pagination = command;

		return await _commentRepository.GetAllByPageId(command.PageId, pagination);
	}
}
