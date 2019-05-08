using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class SwisharkContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<ProjectTask> ProjectTasks { get; set; }
        public virtual DbSet<ProjectMember> ProjectMembers { get; set; }

        public SwisharkContext() { }

        public SwisharkContext(DbContextOptions<SwisharkContext> options)
            : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Swishark;Username=postgres;Password=123;");
        }
    }
}