using Microsoft.EntityFrameworkCore;
using SimpleDemo.Domain.DbEntity.CommerceEntity;

namespace SimpleDemo.Infrastructure.Repository
{
    public class CommerceDbContext(DbContextOptions<CommerceDbContext> options)
        : BaseSimpleDemoDbContext<CommerceDbContext>(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>().ToTable("User").HasKey(x => x.Id);
            modelBuilder.Entity<UserEntity>().HasIndex(x => x.Name).IsUnique();
            modelBuilder.Entity<UserEntity>().HasIndex(x => x.NickName);
            modelBuilder.Entity<UserEntity>().HasIndex(x => x.RoleId);

            base.OnModelCreating(modelBuilder);
        }
    }
}