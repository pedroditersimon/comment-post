using CommentPost.Application.Repositories;
using CommentPost.Application.Services;
using CommentPost.Domain.Entities;

public class UserService : GenericService<User, int>, IUserService
{
	readonly IUnitOfWork _unityOfWork;
	readonly IUserRepository _userRepository;

	public UserService(IUnitOfWork unitOfWork)
		: base(unitOfWork.UserRepository)
	{
		_userRepository = unitOfWork.UserRepository;
		_unityOfWork = unitOfWork;
	}


	public Task<User?> GetByExternalId(string externalId)
	{
		return _userRepository.GetByExternalId(externalId);
	}

	public Task<User?> GetByUsername(string username)
	{
		return _userRepository.GetByUsername(username);
	}
}