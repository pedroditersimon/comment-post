using CommentPost.Application.DTOs.Comment;
using CommentPost.Application.Mappers;
using CommentPost.Application.Repositories;
using CommentPost.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CommentPost.API.Controllers;

[ApiController]
[Route("comments")]
public class CommentController : ControllerBase
{
	readonly ICommentRepository _commentRepository;

	public CommentController(ICommentRepository commentRepository)
	{
		_commentRepository = commentRepository;
	}


	[HttpGet("{id}")]
	public async Task<ActionResult<CommentResponse?>> Get(int id)
	{
		Comment? comment = await _commentRepository.GetById(id);
		if (comment == null)
			return NotFound();

		return Ok(comment.ToResponse());
	}
}
