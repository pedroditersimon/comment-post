using CommentPost.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace CommentPost.API.Extensions;

public static class WebApplicationExtensions
{
	public static void MigrateDbContext(this WebApplication app)
	{
		using var scope = app.Services.CreateScope();
		DbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();

		// Create database using the DbContext
		//context.Database.EnsureCreated();

		// Create and sync database using 'dotnet ef migrations'
		context.Database.Migrate();
	}
}
