using CommentPost.Application.Repositories;
using CommentPost.Application.Services;
using CommentPost.Infrastructure.EntityFramework;
using CommentPost.Infrastructure.EntityFramework.Repositories;

namespace CommentPost.API.Extensions;

public static class ServiceExtensions
{
	public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
	{
		// Repositories
		services.AddScoped<IUserRepository, UserRepository>();
		services.AddScoped<ICommentRepository, CommentRepository>();

		// unit of work
		services.AddScoped<IUnitOfWork, UnitOfWork>();

		// services
		//services.AddScoped<IUserService>();
		services.AddScoped<ICommentService, CommentService>();

		return services;
	}
}
