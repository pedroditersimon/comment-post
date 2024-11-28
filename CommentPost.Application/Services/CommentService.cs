using CommentPost.Domain.Entities;

namespace CommentPost.Application.Services;



public class CommentService
{
	// Postear comentarios: Un usuario registrado crea un nuevo comentario en la plataforma.
	public Comment? PostNewComment(Comment comment) { return null; }

	// Responder comentarios: Un usuario registrado responde a un comentario existente.
	public Comment? PostNewReplyComment(Comment comment, int replyId) { return null; }

	// Leer comentarios: Visualizar los comentarios publicados en una página.
	public List<Comment> GetCommentsByPageId(string pageId) { return null; }

	// Leer respuestas de comentarios: Un usuario lee las respuestas de un comentario.
	public List<Comment> GetRepliesByCommentId(int commentId) { return null; }
}
