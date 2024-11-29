using Microsoft.Extensions.DependencyInjection;

namespace CommentPost.Infrastructure.EntityFramework.Extensions;

public static class ApplicationDBContextExtensions
{
	public static IServiceCollection AddApplicationDBContext(this IServiceCollection services, string connectionString)
	{
		/*
// configure DBContext of PostgreDBService, using loaded settings
services.AddDbContext<TodoDBContext>((IServiceProvider provider, DbContextOptionsBuilder optionsBuilder) =>
{
	PostgresDBSettings dbSettings = provider.GetRequiredService<IOptions<PostgresDBSettings>>().Value;
	var connectionString = $"Host={dbSettings.Host};Username={dbSettings.User};Password={dbSettings.Pass};Database={dbSettings.DbName}";
	optionsBuilder.UseNpgsql(connectionString);
	//optionsBuilder.UseLazyLoadingProxies();
	optionsBuilder.AddInterceptors(
		new ReadExampleInterceptor(),
new SecondLevelCacheInterceptor(provider.GetRequiredService<IMemoryCache>())

	);
});
*/
		return services;
	}
}
