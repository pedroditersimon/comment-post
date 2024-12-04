using CommentPost.Application.DTOs.Comment;
using CommentPost.Domain.Entities;

namespace CommentPost.Application.Mappers;

public static partial class MappingExtensions
{

	public static CommentResponse ToResponse(this Comment comment)
	{
		return new CommentResponse().ReplaceWith(comment);
	}

}
