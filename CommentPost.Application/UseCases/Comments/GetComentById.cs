using CommentPost.Application.Repositories;
using CommentPost.Domain.Entities;

namespace CommentPost.Application.UseCases.Comments;

// Obtener informacion de un comentario.

public class GetComentByIdCommand
{
	public int CommentId { get; set; }
}

public class GetComentByIdHandler
{
	readonly ICommentRepository _commentRepository;

	public GetComentByIdHandler(ICommentRepository commentRepository)
	{
		_commentRepository = commentRepository;
	}


	public async Task<Comment?> ExecuteAsync(GetComentByIdCommand command)
	{
		return await _commentRepository.GetById(command.CommentId);
	}
}
