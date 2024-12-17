namespace CommentPost.Application.UseCases.Comments;

using CommentPost.Application.Filters;
using CommentPost.Application.Services;
using CommentPost.Domain.Entities;

// Leer respuestas de comentarios: Un usuario lee las respuestas de un comentario.
public class GetCommentRepliesCommand : Pagination
{
	public int CommentId { get; set; }
}

public class GetCommentRepliesHandler
{
	readonly ICommentService _commentService;

	public GetCommentRepliesHandler(ICommentService commentService)
	{
		_commentService = commentService;
	}


	public async Task<PaginationResult<Comment>> ExecuteAsync(GetCommentRepliesCommand command)
	{
		Pagination pagination = command;

		return await _commentService.GetAllByReplyId(command.CommentId, pagination);
	}
}
