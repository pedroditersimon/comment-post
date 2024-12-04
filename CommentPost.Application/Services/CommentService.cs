using CommentPost.Application.Repositories;

namespace CommentPost.Application.Services;

public class CommentService
{

	readonly IUnitOfWork _unityOfWork;
	readonly ICommentRepository _commentRepository;

	public CommentService(IUnitOfWork unitOfWork)
	{
		_commentRepository = unitOfWork.CommentRepository;
		_unityOfWork = unitOfWork;
	}
}
