using Microsoft.EntityFrameworkCore;
using SimpleDemo.Domain.DbEntity.AdminEntity;

namespace SimpleDemo.Infrastructure.Repository
{
    public class AdminDbContext(DbContextOptions<AdminDbContext> options) : BaseSimpleDemoDbContext<AdminDbContext>(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>().ToTable("User").HasKey(x => x.Id);
            modelBuilder.Entity<UserEntity>().HasIndex(x => x.Name).IsUnique();
            modelBuilder.Entity<UserEntity>().HasIndex(x => x.Email).IsUnique();
            modelBuilder.Entity<UserEntity>().HasIndex(x => x.RoleId);

            base.OnModelCreating(modelBuilder);
        }
    }
}