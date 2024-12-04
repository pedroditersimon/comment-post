using CommentPost.Application.DTOs.Comment;
using CommentPost.Application.Filters;
using CommentPost.Application.Mappers;
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

	public CommentController(IUnitOfWork unitOfWork)
	{
		_unityOfWork = unitOfWork;
	}


	[HttpPost]
	public async Task<ActionResult<CommentResponse?>> Post([FromBody] PostNewCommentCommand command)
	{
		PostNewCommentHandler handler = new(_unityOfWork);

		// execute
		Comment? comment = await handler.ExecuteAsync(command);
		if (comment == null)
			return Conflict();

		return Ok(comment.ToResponse());
	}

	[HttpPost("reply")]
	public async Task<ActionResult<CommentResponse?>> PostReply([FromBody] PostNewReplyCommentCommand command)
	{
		PostNewReplyCommentHandler handler = new(_unityOfWork);

		// execute
		Comment? comment = await handler.ExecuteAsync(command);
		if (comment == null)
			return Conflict();

		return Ok(comment.ToResponse());
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<CommentResponse?>> Get(int id)
	{
		GetComentByIdCommand command = new() { CommentId = id };
		GetComentByIdHandler handler = new(_unityOfWork.CommentRepository);

		// execute
		Comment? comment = await handler.ExecuteAsync(command);
		if (comment == null)
			return NotFound();

		return Ok(comment.ToResponse());
	}

	[HttpGet("page/{pageId}")]
	public async Task<ActionResult<PaginationResult<CommentResponse>>> Get(string pageId, [FromQuery] int offset, [FromQuery] int limit)
	{
		GetCommentsByPageIdCommand command = new()
		{
			PageId = pageId,
			Offset = offset,
			Limit = limit
		};
		GetCommentsByPageIdHandler handler = new(_unityOfWork.CommentRepository);

		// execute
		PaginationResult<Comment> comments = await handler.ExecuteAsync(command);

		// map
		PaginationResult<CommentResponse> responseComments = comments.Map(comment => comment.ToResponse());

		return Ok(responseComments);
	}


	[HttpGet("replies/{commentId}")]
	public async Task<ActionResult<PaginationResult<CommentResponse>>> Get(int commentId, [FromQuery] int offset, [FromQuery] int limit)
	{
		GetCommentRepliesCommand command = new()
		{
			CommentId = commentId,
			Offset = offset,
			Limit = limit
		};
		GetCommentRepliesHandler handler = new(_unityOfWork.CommentRepository);

		// execute
		PaginationResult<Comment> comments = await handler.ExecuteAsync(command);

		// map
		PaginationResult<CommentResponse> responseComments = comments.Map(comment => comment.ToResponse());

		return Ok(responseComments);
	}

	[HttpPatch]
	public async Task<ActionResult<CommentResponse?>> Patch([FromBody] UpdateCommentByModCommand command)
	{
		UpdateCommentByModHandler handler = new(_unityOfWork);

		// execute
		Comment? comment = await handler.ExecuteAsync(command);
		if (comment == null)
			return Conflict();

		return Ok(comment.ToResponse());
	}
}
