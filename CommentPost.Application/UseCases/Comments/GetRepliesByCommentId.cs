namespace CommentPost.Application.UseCases.Comments;

using CommentPost.Application.Filters;
using CommentPost.Application.Repositories;
using CommentPost.Domain.Entities;

// Leer respuestas de comentarios: Un usuario lee las respuestas de un comentario.
public class GetCommentRepliesCommand : Pagination
{
	public int CommentId { get; set; }
}

public class GetCommentRepliesHandler
{
	readonly ICommentRepository _commentRepository;

	public GetCommentRepliesHandler(ICommentRepository commentRepository)
	{
		_commentRepository = commentRepository;
	}


	public async Task<PaginationResult<Comment>> ExecuteAsync(GetCommentRepliesCommand command)
	{
		Pagination pagination = command;

		return await _commentRepository.GetAllByReplyId(command.CommentId, pagination);
	}
}
