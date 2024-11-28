using CommentPost.Application.Mappers;
using CommentPost.Application.Repositories;
using CommentPost.Domain.Entities;

namespace CommentPost.Application.UseCases;

public class PostNewReplyCommentRequest
{
	public int UserId { get; set; }
	public string Text { get; set; }
	public int ReplyId { get; set; }
}

public class PostNewReplyComment
{
	readonly ICommentRepository _commentRepository;

	public PostNewReplyComment(ICommentRepository commentRepository)
	{
		_commentRepository = commentRepository;
	}

	// Responder comentarios: Un usuario registrado responde a un comentario existente.
	public async Task<Comment?> ExecuteAsync(PostNewReplyCommentRequest request)
	{
		Comment? originalComment = await _commentRepository.GetById(request.ReplyId);

		// not found
		if (originalComment == null)
			return null;

		// map
		Comment comment = new Comment().ReplaceWith(request);

		// original comment is already a reply
		comment.ReplyId = originalComment.ReplyId ?? originalComment.ID;

		return await _commentRepository.Create(comment);
	}
}
