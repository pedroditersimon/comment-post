namespace CommentPost.Application.UseCases.Comments;

using CommentPost.Application.Filters;
using CommentPost.Application.Repositories;
using CommentPost.Domain.Entities;

// Leer respuestas de comentarios: Un usuario lee las respuestas de un comentario.
public class GetRepliesByCommentIdCommand : Pagination
{
    public int CommentId { get; set; }
}

public class GetRepliesByCommentIdHandler
{
    readonly ICommentRepository _commentRepository;

    public GetRepliesByCommentIdHandler(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }


    public async Task<PaginationResult<Comment>> ExecuteAsync(GetRepliesByCommentIdCommand command)
    {
        Pagination pagination = command;

        return await _commentRepository.GetAllByReplyId(command.CommentId, pagination);
    }
}
