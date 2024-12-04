using CommentPost.Application.Repositories;
using CommentPost.Application.Services;
using CommentPost.Domain.Entities;

namespace CommentPost.Application.UseCases;

// Responder comentarios: Un usuario registrado responde a un comentario existente.

public class PostNewReplyCommentCommand
{
	public int UserId { get; set; }
	public string Text { get; set; }
	public int ReplyId { get; set; }
}

public class PostNewReplyCommentHandler
{
	readonly ICommentRepository _commentRepository;
	readonly IUnitOfWork _unitOfWork;

	public PostNewReplyCommentHandler(IUnitOfWork unitOfWork)
	{
		_commentRepository = unitOfWork.CommentRepository;
		_unitOfWork = unitOfWork;
	}

	public async Task<Comment?> ExecuteAsync(PostNewReplyCommentCommand command)
	{
		Comment? originalComment = await _commentRepository.GetById(command.ReplyId);

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
		Comment? createdComment = await _commentRepository.Create(comment);

		// save
		bool saved = await _unitOfWork.ApplyChangesAsync();
		if (!saved) return null;

		return createdComment;
	}
}
