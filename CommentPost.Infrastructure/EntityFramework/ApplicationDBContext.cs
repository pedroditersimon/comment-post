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
		//modelBuilder.ApplyConfiguration(new TodoGoalSeed());

		// dont include Soft deleted entities in any queries
		//modelBuilder.Entity<TodoTask>().HasQueryFilter(t => !t.IsDeleted);
		//modelBuilder.Entity<TodoGoal>().HasQueryFilter(t => !t.IsDeleted);
	}
}
