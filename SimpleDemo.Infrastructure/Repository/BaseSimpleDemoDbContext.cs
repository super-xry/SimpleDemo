using Microsoft.EntityFrameworkCore;
using SimpleDemo.Domain.DbEntity;
using SimpleDemo.Infrastructure.Extension;

namespace SimpleDemo.Infrastructure.Repository
{
    public abstract class BaseSimpleDemoDbContext<T>(DbContextOptions<T> options) : DbContext(options) where T : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.BuildGuid();

            modelBuilder.Entity<RoleEntity>().ToTable("Role").HasKey(x => x.Id);
            modelBuilder.Entity<RoleEntity>().HasIndex(x => x.Name).IsUnique();

            modelBuilder.Entity<PermissionEntity>().ToTable("Permission").HasKey(x => x.Id);
            modelBuilder.Entity<PermissionEntity>().HasIndex(x => x.Label).IsUnique();

            modelBuilder.Entity<LogEntity>().ToTable("Log").HasKey(x => x.Id);
            modelBuilder.Entity<LogEntity>().HasIndex(x => x.Title);
            modelBuilder.Entity<LogEntity>().HasIndex(x => x.Level);
            modelBuilder.Entity<LogEntity>().HasIndex(x => x.Category);
            modelBuilder.Entity<LogEntity>().HasIndex(x => x.CreatedDate);

            base.OnModelCreating(modelBuilder);
        }
    }
}