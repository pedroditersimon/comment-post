using CommentPost.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CommentPost.Infrastructure.EntityFramework;

public class ApplicationDBContext : DbContext
{

	public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
		: base(options)
	{

	}


	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		// Handling seeds with IEntityTypeConfiguration
		//modelBuilder.ApplyConfiguration(new TodoTaskSeed());

		// dont include Soft deleted entities in any queries
		modelBuilder.Entity<User>().HasQueryFilter(c => !c.IsDeleted);
		modelBuilder.Entity<Comment>().HasQueryFilter(c => !c.IsDeleted);
	}
}
