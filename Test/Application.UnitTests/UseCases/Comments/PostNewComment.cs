using CommentPost.Application.Services;
using CommentPost.Application.UseCases.Comments;
using CommentPost.Domain.Entities;
using NSubstitute;

namespace Application.UnitTests.UseCases.Comments;

public class PostNewComment
{
	ICommentService _commentService;
	IUnitOfWork _unitOfWork;

	[SetUp]
	public void SetUp()
	{
		_commentService = Substitute.For<ICommentService>();

		_unitOfWork = Substitute.For<IUnitOfWork>();
		_unitOfWork.ApplyChangesAsync().Returns(true);
	}

	[TearDown]
	public void TearDown()
	{
		_unitOfWork.Dispose();
	}


	// Crear un comentario con inputs correctos
	[Test]
	public async Task ShouldCreateCommentOnValidInputs()
	{
		// Arrenge
		_commentService.Create(Arg.Any<Comment>()).Returns(new Comment()
		{
			ID = 1,
			UserId = 1,
			PageId = "mypageid",
			Text = "test comment"
		});

		PostNewCommentCommand command = new()
		{
			UserId = 1,
			PageId = "mypageid",
			Text = "test comment"
		};
		PostNewCommentHandler handler = new(_commentService, _unitOfWork);

		// Act
		Comment? comment = await handler.ExecuteAsync(command);

		// Assert
		Assert.That(comment, Is.Not.Null);
		Assert.Multiple(() =>
		{
			Assert.That(comment.ID, Is.EqualTo(1));
			Assert.That(comment.PageId, Is.EqualTo("mypageid"));
			Assert.That(comment.Text, Is.EqualTo("test comment"));
		});
	}

	// No deberia crear comentario con un text vacio
	[Test]
	public async Task ShouldNotCreateCommentOnEmptyText()
	{
		// Arrenge
		PostNewCommentCommand command = new()
		{
			UserId = 1,
			PageId = "mypageid",
			Text = ""
		};
		PostNewCommentHandler handler = new(_commentService, _unitOfWork);

		// Act
		Comment? comment = await handler.ExecuteAsync(command);

		// Assert
		Assert.That(comment, Is.Null);
	}


	// No deberia crear un comentario sin un page id
	[Test]
	public async Task ShouldNotCreateCommentOnEmptyPage()
	{
		// Arrenge
		PostNewCommentCommand command = new()
		{
			UserId = 1,
			PageId = "",
			Text = "test comment"
		};
		PostNewCommentHandler handler = new(_commentService, _unitOfWork);

		// Act
		Comment? comment = await handler.ExecuteAsync(command);

		// Assert
		Assert.That(comment, Is.Null);
	}
}
