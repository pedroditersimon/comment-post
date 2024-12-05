using CommentPost.Application.Services;
using CommentPost.Application.UseCases.Comments;
using CommentPost.Domain.Entities;
using NSubstitute;

namespace Application.UnitTests.UseCases.Comments;

public class GetCommentById
{

	ICommentService _commentService;

	[SetUp]
	public void SetUp()
	{
		_commentService = Substitute.For<ICommentService>();
	}


	// Devolver un comentario existente
	[Test]
	public async Task ShouldGetExistingComment()
	{
		// Arrenge
		_commentService.GetById(1).Returns(new Comment()
		{
			ID = 1,
			Text = "test comment"
		});

		GetComentByIdCommand command = new() { CommentId = 1 };
		GetComentByIdHandler handler = new(_commentService);

		// Act
		Comment? comment = await handler.ExecuteAsync(command);

		// Assert
		Assert.That(comment, Is.Not.Null);
		Assert.Multiple(() =>
		{
			Assert.That(comment.ID, Is.EqualTo(1));
			Assert.That(comment.Text, Is.EqualTo("test comment"));
		});
	}


	// No devolver un comentario inexistente
	[Test]
	public async Task ShouldNotGetInexistentComment()
	{
		// Arrenge
		GetComentByIdCommand command = new() { CommentId = 1 };
		GetComentByIdHandler handler = new(_commentService);

		// Act
		Comment? comment = await handler.ExecuteAsync(command);

		// Assert
		Assert.That(comment, Is.Null);
	}

}
