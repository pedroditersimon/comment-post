using CommentPost.Application.Exceptions;
using CommentPost.Application.Services;
using CommentPost.Domain.Entities;

namespace CommentPost.Application.UseCases.Comments;

// Postear comentarios: Un usuario registrado crea un nuevo comentario en la plataforma.

public class PostNewCommentCommand
{
	public int UserId { get; set; }
	public string PageId { get; set; }
	public string Text { get; set; }
}

public class PostNewCommentHandler
{
	readonly ICommentService _commentService;
	readonly IUnitOfWork _unitOfWork;

	public PostNewCommentHandler(ICommentService commentService, IUnitOfWork unitOfWork)
	{
		_commentService = commentService;
		_unitOfWork = unitOfWork;
	}

	/// <exception cref="InvalidArgumentException" />
	/// <exception cref="CreateResourceException" />
	/// <exception cref="SaveChangesException" />
	public async Task<Comment?> ExecuteAsync(PostNewCommentCommand command)
	{
		if (string.IsNullOrEmpty(command.Text) || string.IsNullOrEmpty(command.PageId))
			throw new InvalidArgumentException();

		// TODO: verify that userId exists

		Comment comment = new()
		{
			UserId = command.UserId,
			PageId = command.PageId,
			Text = command.Text
		};

		// create
		Comment? createdComment = await _commentService.Create(comment);
		if (createdComment == null)
			throw new CreateResourceException(nameof(Comment), command);

		// save
		bool saved = await _unitOfWork.ApplyChangesAsync();
		if (!saved)
			throw new SaveChangesException();

		return createdComment;
	}
}
