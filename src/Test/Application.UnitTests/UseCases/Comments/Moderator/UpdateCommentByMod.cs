using CommentPost.Application.Exceptions;
using CommentPost.Application.Services;
using CommentPost.Application.UseCases.Comments.Moderator;
using CommentPost.Domain.Entities;
using NSubstitute;

namespace Application.UnitTests.UseCases.Comments.Moderator
{
	public class UpdateCommentByMod
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

		// Actualizar el comentario por inputs validos
		[Test]
		public async Task ShouldUpdateCommentOnValidInputs()
		{
			// Arrenge
			_commentService.GetById(1).Returns(new Comment()
			{
				ID = 1,
				Text = "test comment"
			});

			_commentService.Update(Arg.Any<Comment>()).Returns(callInfo => callInfo.Arg<Comment>());

			UpdateCommentByModCommand command = new() { CommentId = 1, Text = "my new text" };
			UpdateCommentByModHandler handler = new(_commentService, _unitOfWork);

			// Act
			Comment comment = await handler.ExecuteAsync(command);

			// Assert
			Assert.That(comment, Is.Not.Null);
			Assert.Multiple(() =>
			{
				Assert.That(comment.ID, Is.EqualTo(1));
				Assert.That(comment.Text, Is.EqualTo("my new text"));
			});
		}

		// Not found al actualizar un comentario inexistente
		[Test]
		public void ShouldThrowNotFound_WhenCommentDosntExist()
		{
			// Arrenge
			UpdateCommentByModCommand command = new() { CommentId = 1 };
			UpdateCommentByModHandler handler = new(_commentService, _unitOfWork);

			// Act and Assert
			Assert.ThrowsAsync<NotFoundException>(async () => await handler.ExecuteAsync(command));
		}


		// Actualizar solo los inputs incluidos en el comando
		[Test]
		public async Task ShouldOnlyUpdateIncludedProps()
		{
			// Arrenge
			_commentService.GetById(1).Returns(new Comment()
			{
				ID = 1,
				Text = "test comment",
				PageId = "mypageid",
				Visibility = true,
				ReplyId = 2
			});

			_commentService.Update(Arg.Any<Comment>()).Returns(callInfo => callInfo.Arg<Comment>());

			UpdateCommentByModCommand command = new() { CommentId = 1, Text = "my new text" };
			UpdateCommentByModHandler handler = new(_commentService, _unitOfWork);

			// Act
			Comment comment = await handler.ExecuteAsync(command);

			// Assert
			Assert.That(comment, Is.Not.Null);
			Assert.Multiple(() =>
			{
				Assert.That(comment.ID, Is.EqualTo(1));
				Assert.That(comment.Text, Is.EqualTo("my new text"));
				Assert.That(comment.PageId, Is.EqualTo("mypageid"));
				Assert.That(comment.Visibility, Is.True);
				Assert.That(comment.ReplyId, Is.EqualTo(2));
			});
		}
	}
}
