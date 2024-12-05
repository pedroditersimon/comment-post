using CommentPost.Application.Filters;
using CommentPost.Application.Services;
using CommentPost.Domain.Entities;

namespace CommentPost.Application.UseCases.Comments;

// Leer comentarios: Visualizar los comentarios publicados en una página.

public class GetCommentsByPageIdCommand : Pagination
{
	public string PageId { get; set; }
}

public class GetCommentsByPageIdHandler
{
	readonly ICommentService _commentService;

	public GetCommentsByPageIdHandler(ICommentService commentService)
	{
		_commentService = commentService;
	}


	public async Task<PaginationResult<Comment>> ExecuteAsync(GetCommentsByPageIdCommand command)
	{
		Pagination pagination = command;

		return await _commentService.GetAllByPageId(command.PageId, pagination);
	}
}
