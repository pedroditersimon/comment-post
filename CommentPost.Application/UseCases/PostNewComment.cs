using CommentPost.Application.Repositories;
using CommentPost.Application.Services;
using CommentPost.Domain.Entities;

namespace CommentPost.Application.UseCases;

// Postear comentarios: Un usuario registrado crea un nuevo comentario en la plataforma.

public class PostNewCommentRequest
{
	public int UserId { get; set; }
	public string PageId { get; set; }
	public string Text { get; set; }
}

public class PostNewComment
{
	readonly ICommentRepository _commentRepository;
	readonly IUnitOfWork _unitOfWork;

	public PostNewComment(IUnitOfWork unitOfWork)
	{
		_commentRepository = unitOfWork.CommentRepository;
		_unitOfWork = unitOfWork;
	}


	public async Task<Comment?> ExecuteAsync(PostNewCommentRequest request)
	{
		Comment comment = new()
		{
			UserId = request.UserId,
			PageId = request.PageId,
			Text = request.Text
		};

		// create
		Comment? createdComment = await _commentRepository.Create(comment);

		// save
		bool saved = await _unitOfWork.ApplyChangesAsync();
		if (!saved) return null;

		return createdComment;
	}
}
