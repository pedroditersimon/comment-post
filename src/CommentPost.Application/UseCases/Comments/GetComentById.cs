using CommentPost.Application.Services;
using CommentPost.Domain.Entities;

namespace CommentPost.Application.UseCases.Comments;

// Obtener informacion de un comentario.

public class GetComentByIdCommand
{
	public int CommentId { get; set; }
}

public class GetComentByIdHandler
{
	readonly ICommentService _commentService;

	public GetComentByIdHandler(ICommentService commentService)
	{
		_commentService = commentService;
	}


	public async Task<Comment?> ExecuteAsync(GetComentByIdCommand command)
	{
		return await _commentService.GetById(command.CommentId);
	}
}
