using CommentPost.Application.DTOs.Comment;
using CommentPost.Application.Exceptions;
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
	readonly IUnitOfWork _unitOfWork;
	readonly ICommentService _commentService;

	public CommentController(ICommentService commentService, IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
		_commentService = commentService;
	}


	[HttpPost]
	public async Task<ActionResult<CommentResponse?>> Post([FromBody] PostNewCommentCommand command)
	{
		PostNewCommentHandler handler = new(_commentService, _unitOfWork);

		Comment? comment;

		// execute
		try
		{
			comment = await handler.ExecuteAsync(command);
		}
		catch (CreateResourceException)
		{
			return StatusCode(500); // Internal Server Error
		}
		catch (SaveChangesException)
		{
			return StatusCode(503); // Service Unavailable
		}

		if (comment == null)
			return StatusCode(500); // Internal Server Error

		return Ok(comment.ToResponse());
	}

	[HttpPost("reply")]
	public async Task<ActionResult<CommentResponse?>> PostReply([FromBody] PostNewReplyCommentCommand command)
	{
		PostNewReplyCommentHandler handler = new(_commentService, _unitOfWork);

		Comment? comment;

		// execute
		try
		{
			comment = await handler.ExecuteAsync(command);
		}
		catch (NotFoundException)
		{
			return NotFound();
		}
		catch (CreateResourceException)
		{
			return StatusCode(500); // Internal Server Error
		}
		catch (SaveChangesException)
		{
			return StatusCode(503); // Service Unavailable
		}

		if (comment == null)
			return Conflict();

		return Ok(comment.ToResponse());
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<CommentResponse?>> Get(int id)
	{
		GetComentByIdCommand command = new() { CommentId = id };
		GetComentByIdHandler handler = new(_commentService);

		Comment? comment;

		// execute
		try
		{
			comment = await handler.ExecuteAsync(command);
		}
		catch (NotFoundException)
		{
			return NotFound();
		}

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
		GetCommentsByPageIdHandler handler = new(_commentService);

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
		GetCommentRepliesHandler handler = new(_commentService);

		// execute
		PaginationResult<Comment> comments = await handler.ExecuteAsync(command);

		// map
		PaginationResult<CommentResponse> responseComments = comments.Map(comment => comment.ToResponse());

		return Ok(responseComments);
	}

	[HttpPatch]
	public async Task<ActionResult<CommentResponse?>> Patch([FromBody] UpdateCommentByModCommand command)
	{
		UpdateCommentByModHandler handler = new(_commentService, _unitOfWork);

		Comment? comment;

		// execute
		try
		{
			comment = await handler.ExecuteAsync(command);
		}
		catch (NotFoundException)
		{
			return NotFound();
		}
		catch (UpdateResourceException)
		{
			return StatusCode(500); // Internal Server Error
		}
		catch (SaveChangesException)
		{
			return StatusCode(503); // Service Unavailable
		}

		if (comment == null)
			return StatusCode(500); // Internal Server Error

		return Ok(comment.ToResponse());
	}
}
