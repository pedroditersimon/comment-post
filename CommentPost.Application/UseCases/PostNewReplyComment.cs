using CommentPost.Application.Repositories;
using CommentPost.Domain.Entities;

namespace CommentPost.Application.UseCases;

// Responder comentarios: Un usuario registrado responde a un comentario existente.

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


	public async Task<Comment?> ExecuteAsync(PostNewReplyCommentRequest request)
	{
		Comment? originalComment = await _commentRepository.GetById(request.ReplyId);

		// not found
		if (originalComment == null)
			return null;

		// map
		Comment comment = new()
		{
			UserId = request.UserId,
			Text = request.Text,

			// if original comment is already a reply, point to the same reply
			ReplyId = originalComment.ReplyId ?? originalComment.ID
		};

		return await _commentRepository.Create(comment);
	}
}
