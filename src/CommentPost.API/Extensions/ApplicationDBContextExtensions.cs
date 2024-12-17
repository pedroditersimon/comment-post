using CommentPost.Infrastructure.Configurations;
using CommentPost.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CommentPost.API.Extensions;

public static class ApplicationDBContextExtensions
{
	public static IServiceCollection AddApplicationDBContext(this IServiceCollection services)
	{
		// configure DBContext of PostgreDBService, using loaded settings
		services.AddDbContext<ApplicationDBContext>((IServiceProvider provider, DbContextOptionsBuilder optionsBuilder) =>
		{
			PostgreSQLSettings dbSettings = provider.GetRequiredService<IOptions<PostgreSQLSettings>>().Value;
			var connectionString = $"Host={dbSettings.Host};Username={dbSettings.User};Password={dbSettings.Pass};Database={dbSettings.DbName}";

			optionsBuilder.UseNpgsql(connectionString);

			//optionsBuilder.UseLazyLoadingProxies();

			/*
			optionsBuilder.AddInterceptors(
				new ReadExampleInterceptor(),
				new SecondLevelCacheInterceptor(provider.GetRequiredService<IMemoryCache>())
			);
			*/
		});

		services.AddScoped<ApplicationDBContext>();
		services.AddScoped<DbContext, ApplicationDBContext>();

		return services;
	}
}
