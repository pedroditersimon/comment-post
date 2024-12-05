using CommentPost.Application.Exceptions;
using CommentPost.Application.Services;
using CommentPost.Application.UseCases.Comments;
using CommentPost.Domain.Entities;
using NSubstitute;

namespace Application.UnitTests.UseCases.Comments;

public class PostNewReplyComment
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


	// Crear una respuesta con inputs correctos
	[Test]
	public async Task ShouldCreateReplyOnValidInputs()
	{
		// Arrenge
		_commentService.GetById(1).Returns(new Comment()
		{
			ID = 1,
			PageId = "mypageid"
		});

		_commentService.Create(Arg.Any<Comment>()).Returns(callInfo => callInfo.Arg<Comment>());

		PostNewReplyCommentCommand command = new()
		{
			UserId = 1,
			Text = "test comment",
			ReplyId = 1
		};
		PostNewReplyCommentHandler handler = new(_commentService, _unitOfWork);

		// Act
		Comment? comment = await handler.ExecuteAsync(command);

		// Assert
		Assert.That(comment, Is.Not.Null);
		Assert.Multiple(() =>
		{
			Assert.That(comment.ReplyId, Is.EqualTo(1));
			Assert.That(comment.PageId, Is.EqualTo("mypageid"));
			Assert.That(comment.Text, Is.EqualTo("test comment"));
		});
	}

	// inherit page id from original comment
	[Test]
	public async Task ShouldInheritPageId()
	{
		// Arrenge
		_commentService.GetById(1).Returns(new Comment()
		{
			ID = 1,
			PageId = "mypageid"
		});

		_commentService.Create(Arg.Any<Comment>()).Returns(callInfo => callInfo.Arg<Comment>());

		PostNewReplyCommentCommand command = new()
		{
			UserId = 1,
			Text = "test comment",
			ReplyId = 1
		};
		PostNewReplyCommentHandler handler = new(_commentService, _unitOfWork);

		// Act
		Comment? comment = await handler.ExecuteAsync(command);

		// Assert
		Assert.That(comment, Is.Not.Null);
		Assert.That(comment.PageId, Is.EqualTo("mypageid"));
	}




	// El reply-id de un comentario dentro de una conversación apunta al comentario original.
	[Test]
	public async Task ReplyId_ShouldPointToOriginalComment()
	{
		// Arrenge
		_commentService.GetById(2).Returns(new Comment()
		{
			ID = 2,
			ReplyId = 1
		});

		_commentService.Create(Arg.Any<Comment>()).Returns(callInfo => callInfo.Arg<Comment>());

		PostNewReplyCommentCommand command = new()
		{
			UserId = 1,
			Text = "test comment",
			ReplyId = 2
		};
		PostNewReplyCommentHandler handler = new(_commentService, _unitOfWork);

		// Act
		Comment? comment = await handler.ExecuteAsync(command);

		// Assert
		Assert.That(comment, Is.Not.Null);
		Assert.That(comment.ReplyId, Is.EqualTo(1)); // point to original comment
	}


	// No deberia crear una respuesta con un text vacio
	[Test]
	public async Task ShouldNotCreateReplyOnEmptyText()
	{
		// Arrenge
		PostNewReplyCommentCommand command = new()
		{
			UserId = 1,
			Text = "",
			ReplyId = 1
		};
		PostNewReplyCommentHandler handler = new(_commentService, _unitOfWork);

		// Act
		Comment? comment = await handler.ExecuteAsync(command);

		// Assert
		Assert.That(comment, Is.Null);
	}


	// No deberia crear una respuesta a un comentario inexistente
	[Test]
	public void ShouldNotCreateReplyOnNonExistingComment()
	{
		// Arrenge
		_commentService.Create(Arg.Any<Comment>()).Returns(callInfo => callInfo.Arg<Comment>());

		PostNewReplyCommentCommand command = new()
		{
			UserId = 1,
			Text = "test comment",
			ReplyId = 1
		};
		PostNewReplyCommentHandler handler = new(_commentService, _unitOfWork);

		// Act and Assert
		Assert.ThrowsAsync<NotFoundException>(async () => await handler.ExecuteAsync(command));
	}
}
