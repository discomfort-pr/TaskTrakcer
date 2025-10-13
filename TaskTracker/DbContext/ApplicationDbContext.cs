using TaskTracker.Models;
using Task = TaskTracker.Models.Task;
using TaskStatus = TaskTracker.Models.TaskStatus;

namespace TaskTracker.DbContext;

using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Task> Tasks { get; set; }
    public DbSet<TaskStatus> TaskStatuses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();
        
        modelBuilder.Entity<TaskStatus>().HasData(
            new TaskStatus { Id = 1, Status = "New" },
            new TaskStatus { Id = 2, Status = "In Progress" },
            new TaskStatus { Id = 3, Status = "Done" }
        );
    }
}