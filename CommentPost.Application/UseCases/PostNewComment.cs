using CommentPost.Application.Repositories;
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

	public PostNewComment(ICommentRepository commentRepository)
	{
		_commentRepository = commentRepository;
	}


	public async Task<Comment?> ExecuteAsync(PostNewCommentRequest request)
	{
		Comment comment = new()
		{
			UserId = request.UserId,
			PageId = request.PageId,
			Text = request.Text
		};

		return await _commentRepository.Create(comment);
	}
}
