using CommentPost.Application.Services;
using CommentPost.Domain.Entities;

namespace CommentPost.Application.UseCases.Comments;

// Responder comentarios: Un usuario registrado responde a un comentario existente.

public class PostNewReplyCommentCommand
{
	public int UserId { get; set; }
	public string Text { get; set; }
	public int ReplyId { get; set; }
}

public class PostNewReplyCommentHandler
{
	readonly ICommentService _commentService;
	readonly IUnitOfWork _unitOfWork;

	public PostNewReplyCommentHandler(ICommentService commentService, IUnitOfWork unitOfWork)
	{
		_commentService = commentService;
		_unitOfWork = unitOfWork;
	}

	public async Task<Comment?> ExecuteAsync(PostNewReplyCommentCommand command)
	{
		Comment? originalComment = await _commentService.GetById(command.ReplyId);

		// not found
		if (originalComment == null)
			return null;

		// map
		Comment comment = new()
		{
			UserId = command.UserId,

			// if original comment is already a reply, point to the same reply
			ReplyId = originalComment.ReplyId ?? originalComment.ID,

			// inherit page id from original comment
			PageId = originalComment.PageId,

			Text = command.Text,
		};


		// create
		Comment? createdComment = await _commentService.Create(comment);

		// save
		bool saved = await _unitOfWork.ApplyChangesAsync();
		if (!saved) return null;

		return createdComment;
	}
}
