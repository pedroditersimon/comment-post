namespace CommentPost.Application.UseCases;

using CommentPost.Application.Filters;
using CommentPost.Application.Repositories;
using CommentPost.Domain.Entities;


public class GetRepliesByCommentIdRequest : Pagination
{
	public int CommentId { get; set; }
}

public class GetRepliesByCommentId
{
	readonly ICommentRepository _commentRepository;

	public GetRepliesByCommentId(ICommentRepository commentRepository)
	{
		_commentRepository = commentRepository;
	}

	// Leer respuestas de comentarios: Un usuario lee las respuestas de un comentario.
	public async Task<PaginationResult<Comment>> ExecuteAsync(GetRepliesByCommentIdRequest request)
	{
		Pagination pagination = request;

		return await _commentRepository.GetAllByReplyId(request.CommentId, pagination);
	}
}
