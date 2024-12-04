using CommentPost.Application.DTOs.Comment;
using CommentPost.Application.Mappers;
using CommentPost.Application.Repositories;
using CommentPost.Application.Services;
using CommentPost.Application.UseCases.Comments;
using CommentPost.Application.UseCases.Comments.Moderator;
using CommentPost.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CommentPost.API.Controllers;

[ApiController]
[Route("comments")]
public class CommentController : ControllerBase
{
	readonly IUnitOfWork _unityOfWork;
	readonly ICommentRepository _commentRepository;

	public CommentController(IUnitOfWork unitOfWork)
	{
		_commentRepository = unitOfWork.CommentRepository;
		_unityOfWork = unitOfWork;
	}


	[HttpGet("{id}")]
	public async Task<ActionResult<CommentResponse?>> Get(int id)
	{
		Comment? comment = await _commentRepository.GetById(id);
		if (comment == null)
			return NotFound();

		return Ok(comment.ToResponse());
	}

	[HttpPost]
	public async Task<ActionResult<CommentResponse?>> Post(PostNewCommentCommand command)
	{
		PostNewCommentHandler handler = new(_unityOfWork);

		// execute
		Comment? comment = await handler.ExecuteAsync(command);
		if (comment == null)
			return Conflict();

		return Ok(comment.ToResponse());
	}

	[HttpPost("reply")]
	public async Task<ActionResult<CommentResponse?>> PostReply(PostNewReplyCommentCommand command)
	{
		PostNewReplyCommentHandler handler = new(_unityOfWork);

		// execute
		Comment? comment = await handler.ExecuteAsync(command);
		if (comment == null)
			return Conflict();

		return Ok(comment.ToResponse());
	}

	[HttpPatch]
	public async Task<ActionResult<CommentResponse?>> Patch(UpdateCommentByModCommand command)
	{
		UpdateCommentByModHandler handler = new(_unityOfWork);

		// execute
		Comment? comment = await handler.ExecuteAsync(command);
		if (comment == null)
			return Conflict();

		return Ok(comment.ToResponse());
	}
}
