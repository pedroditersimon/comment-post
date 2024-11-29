using CommentPost.Application.Repositories;
using CommentPost.Application.Services;
using CommentPost.Infrastructure.EntityFramework.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CommentPost.Infrastructure.EntityFramework.Extensions;

public static class InfraestructureServiceExtensions
{
	public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
	{

		services.AddScoped<ICommentRepository, CommentRepository>();
		services.AddScoped<IUnitOfWork, UnitOfWork>();

		return services;
	}
}
