using CommentPost.Application.Repositories;
using CommentPost.Application.Services;
using CommentPost.Infrastructure.EntityFramework;
using CommentPost.Infrastructure.EntityFramework.Repositories;

namespace CommentPost.API.Extensions;

public static class ServiceExtensions
{
	public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
	{
		services.AddScoped<IUserRepository, UserRepository>();
		services.AddScoped<ICommentRepository, CommentRepository>();
		services.AddScoped<IUnitOfWork, UnitOfWork>();

		return services;
	}
}
