using CommentPost.Application.Filters;
using CommentPost.Application.Services;
using CommentPost.Application.UseCases.Comments;
using CommentPost.Domain.Entities;
using NSubstitute;

namespace Application.UnitTests.UseCases.Comments;

public class GetRepliesByCommentId
{
	ICommentService _commentService;

	[SetUp]
	public void SetUp()
	{
		_commentService = Substitute.For<ICommentService>();
	}


	// Devolver respuestas existentes asociados a un comentario
	[Test]
	public async Task ShouldGetRepliesOnExistingComment()
	{
		// Arrenge
		Comment[] replies = [
			new()
			{
				ID = 2,
				Text = "test reply 2",
				ReplyId = 1
			},
			new()
			{
				ID = 3,
				Text = "test reply 3",
				ReplyId = 1
			}
		];
		PaginationResult<Comment> paginationResult = new(replies.AsQueryable(), new Pagination());

		_commentService.GetAllByReplyId(1, Arg.Any<Pagination>()).Returns(paginationResult);

		GetCommentRepliesCommand command = new() { CommentId = 1 };
		GetCommentRepliesHandler handler = new(_commentService);

		// Act
		PaginationResult<Comment> commentsResult = await handler.ExecuteAsync(command);

		// Assert
		Assert.That(commentsResult, Is.Not.Null);
		Assert.Multiple(() =>
		{
			Assert.That(commentsResult.Elements.Count(), Is.EqualTo(2));
			Assert.That(commentsResult.Elements, Is.All.Matches<Comment>(e => e.ReplyId == 1));
		});
	}


	// No devolver nada ante un comentario inexistente
	[Test]
	public async Task ShouldNotGetRepliesOnNonExistingComment()
	{
		// Arrenge
		PaginationResult<Comment> paginationResult = new(Array.Empty<Comment>().AsQueryable(), new Pagination());

		_commentService.GetAllByReplyId(1, Arg.Any<Pagination>()).Returns(paginationResult);

		GetCommentRepliesCommand command = new() { CommentId = 1 };
		GetCommentRepliesHandler handler = new(_commentService);

		// Act
		PaginationResult<Comment> commentsResult = await handler.ExecuteAsync(command);

		// Assert
		Assert.That(commentsResult, Is.Not.Null);
		Assert.That(commentsResult.Count, Is.EqualTo(0));
		Assert.That(commentsResult.Elements.Count(), Is.EqualTo(0));
	}
}


