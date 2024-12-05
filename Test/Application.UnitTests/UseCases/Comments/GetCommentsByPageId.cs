using CommentPost.Application.Filters;
using CommentPost.Application.Services;
using CommentPost.Application.UseCases.Comments;
using CommentPost.Domain.Entities;
using NSubstitute;

namespace Application.UnitTests.UseCases.Comments;

public class GetCommentsByPageId
{
	ICommentService _commentService;

	[SetUp]
	public void SetUp()
	{
		_commentService = Substitute.For<ICommentService>();
	}


	// Devolver comentarios existentes asociados a una page
	[Test]
	public async Task ShouldGetCommentsOnExistingPage()
	{
		// Arrenge
		Comment[] comments = [
			new()
			{
				ID = 1,
				Text = "test comment 1",
				PageId= "mypageid"
			},
			new()
			{
				ID = 2,
				Text = "test comment 2",
				PageId= "mypageid"
			}
		];
		PaginationResult<Comment> paginationResult = new(comments.AsQueryable(), new Pagination());

		_commentService.GetAllByPageId("mypageid", Arg.Any<Pagination>()).Returns(paginationResult);

		GetCommentsByPageIdCommand command = new() { PageId = "mypageid" };
		GetCommentsByPageIdHandler handler = new(_commentService);

		// Act
		PaginationResult<Comment> commentsResult = await handler.ExecuteAsync(command);

		// Assert
		Assert.That(commentsResult, Is.Not.Null);
		Assert.Multiple(() =>
		{
			Assert.That(commentsResult.Elements.Count(), Is.EqualTo(2));
			Assert.That(commentsResult.Elements, Is.All.Matches<Comment>(e => e.PageId == "mypageid"));
		});
	}


	// No devolver nada ante una page inexistente
	[Test]
	public async Task ShouldNotGetCommentsOnNonExistentPage()
	{
		// Arrenge
		PaginationResult<Comment> paginationResult = new(Array.Empty<Comment>().AsQueryable(), new Pagination());

		_commentService.GetAllByPageId("nonexistentpage", Arg.Any<Pagination>()).Returns(paginationResult);

		GetCommentsByPageIdCommand command = new() { PageId = "nonexistentpage", };
		GetCommentsByPageIdHandler handler = new(_commentService);

		// Act
		PaginationResult<Comment> commentsResult = await handler.ExecuteAsync(command);

		// Assert
		Assert.That(commentsResult, Is.Not.Null);
		Assert.That(commentsResult.Count, Is.EqualTo(0));
		Assert.That(commentsResult.Elements.Count(), Is.EqualTo(0));
	}
}


