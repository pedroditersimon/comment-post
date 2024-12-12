using CommentPost.API.Attributes;
using CommentPost.API.Extensions;
using CommentPost.Application.DTOs.Comment;
using CommentPost.Application.Exceptions;
using CommentPost.Application.Filters;
using CommentPost.Application.Mappers;
using CommentPost.Application.Services;
using CommentPost.Application.UseCases.Comments;
using CommentPost.Application.UseCases.Comments.Moderator;
using CommentPost.Domain.Entities;
using CommentPost.Domain.Enums;
using CommentPost.Infrastructure.Models.Auth;
using CommentPost.Infrastructure.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace CommentPost.API.Controllers;

[ApiController]
[Route("comments")]
public class CommentController : ControllerBase
{
	readonly IUnitOfWork _unitOfWork;
	readonly ICommentService _commentService;
	readonly AuthService _authService;

	public CommentController(ICommentService commentService, IUnitOfWork unitOfWork, AuthService authService)
	{
		_unitOfWork = unitOfWork;
		_commentService = commentService;
		_authService = authService;
	}


	[HttpPost]
	[NeedAuthorization]
	public async Task<ActionResult<CommentResponse?>> Post([FromBody] PostNewCommentCommand command)
	{
		if (!HttpContext.TryGetDecodedAuthToken(out DecodedAuthToken decodedToken))
			return Unauthorized();

		command.UserId = decodedToken.UserId;

		PostNewCommentHandler handler = new(_commentService, _unitOfWork);

		Comment? comment;

		// execute
		try
		{
			comment = await handler.ExecuteAsync(command);
		}
		catch (Exception ex) when (ex is CreateResourceException
								|| ex is SaveChangesException)
		{
			return StatusCode(500); // Internal Server Error
		}

		if (comment == null)
			return StatusCode(500); // Internal Server Error

		return Ok(comment.ToResponse());
	}

	[HttpPost("reply")]
	[NeedAuthorization]
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
		catch (Exception ex) when (ex is CreateResourceException
								|| ex is SaveChangesException)
		{
			return StatusCode(500); // Internal Server Error
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
	public async Task<ActionResult<PaginationResult<CommentResponse>>> GetByPageId(string pageId, [FromQuery] int offset, [FromQuery] int limit)
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
	public async Task<ActionResult<PaginationResult<CommentResponse>>> GetReplies(int commentId, [FromQuery] int offset, [FromQuery] int limit)
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
	[NeedAuthorization(Role.Moderator)]
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
		catch (Exception ex) when (ex is UpdateResourceException
								|| ex is SaveChangesException)
		{
			return StatusCode(500); // Internal Server Error
		}

		if (comment == null)
			return StatusCode(500); // Internal Server Error

		return Ok(comment.ToResponse());
	}
}
