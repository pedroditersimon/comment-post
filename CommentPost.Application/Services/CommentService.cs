using CommentPost.Application.Filters;
using CommentPost.Application.Repositories;
using CommentPost.Domain.Entities;

namespace CommentPost.Application.Services;

public class CommentService
{
	readonly ICommentRepository _commentRepository;

	public CommentService(ICommentRepository commentRepository)
	{
		_commentRepository = commentRepository;
	}

	// Postear comentarios: Un usuario registrado crea un nuevo comentario en la plataforma.
	public Comment? PostNewComment(Comment comment)
	{
		return _commentRepository.Create(comment);
	}

	// Responder comentarios: Un usuario registrado responde a un comentario existente.
	public Comment? PostNewReplyComment(Comment comment, int replyId)
	{
		comment.ReplyId = replyId;
		return _commentRepository.Create(comment);
	}

	// Leer comentarios: Visualizar los comentarios publicados en una página.
	public PaginationResult<Comment> GetCommentsByPageId(string pageId, Pagination pagination)
	{
		return _commentRepository.GetAllByPageId(pageId, pagination);
	}

	// Leer respuestas de comentarios: Un usuario lee las respuestas de un comentario.
	public PaginationResult<Comment> GetRepliesByCommentId(int commentId, Pagination pagination)
	{
		return _commentRepository.GetAllByReplyId(commentId, pagination);
	}
}
