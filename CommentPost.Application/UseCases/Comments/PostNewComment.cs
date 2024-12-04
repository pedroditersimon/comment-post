using CommentPost.Application.Repositories;
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
    readonly ICommentRepository _commentRepository;
    readonly IUnitOfWork _unitOfWork;

    public PostNewCommentHandler(IUnitOfWork unitOfWork)
    {
        _commentRepository = unitOfWork.CommentRepository;
        _unitOfWork = unitOfWork;
    }


    public async Task<Comment?> ExecuteAsync(PostNewCommentCommand command)
    {
        Comment comment = new()
        {
            UserId = command.UserId,
            PageId = command.PageId,
            Text = command.Text
        };

        // create
        Comment? createdComment = await _commentRepository.Create(comment);

        // save
        bool saved = await _unitOfWork.ApplyChangesAsync();
        if (!saved) return null;

        return createdComment;
    }
}
