using Microsoft.EntityFrameworkCore;
using SimpleDemo.Domain.DbEntity;
using SimpleDemo.Domain.DbEntity.CommerceEntity;
using static SimpleDemo.Shared.Constant.PermissionResources;

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

            InitDemoData(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            base.OnConfiguring(optionsBuilder);
        }

        private static void InitDemoData(ModelBuilder modelBuilder)
        {
            var adminId = Guid.NewGuid();
            var visitorId = Guid.NewGuid();

            modelBuilder.Entity<RoleEntity>().HasData(new List<RoleEntity>()
            {
                new ()
                {
                    Id = adminId,
                    CreatedBy = Shared.Constant.Common.DefaultCreator,
                    Name = "Admin",
                    Description = "Admin",
                },
                new ()
                {
                    Id = visitorId,
                    CreatedBy = Shared.Constant.Common.DefaultCreator,
                    Name = "Visitor",
                    Description = "Visitor",
                }
            });

            modelBuilder.Entity<PermissionEntity>().HasData(new List<PermissionEntity>()
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    CreatedBy =Shared.Constant.Common.DefaultCreator,
                    Description = "Full access for permission.",
                    CreatedDate = DateTimeOffset.UtcNow,
                    Label = Permission.Full,
                    RoleId = adminId,
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    CreatedBy = Shared.Constant.Common.DefaultCreator,
                    Description = "Full access for user.",
                    CreatedDate = DateTimeOffset.UtcNow,
                    Label = User.Full,
                    RoleId = adminId,
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    CreatedBy = Shared.Constant.Common.DefaultCreator,
                    Description = "Read the user self data.",
                    CreatedDate = DateTimeOffset.UtcNow,
                    Label = User.Read,
                    RoleId = visitorId,
                },
            });

            modelBuilder.Entity<UserEntity>().HasData(new List<UserEntity>()
            {
                new ()
                {
                    Id = Guid.NewGuid(),
                    CreatedBy = Shared.Constant.Common.DefaultCreator,
                    CreatedDate = DateTimeOffset.UtcNow,
                    Email = "admin@domain.com",
                    Name = "Admin",
                    NickName = "Judy",
                    Password = "Admin",
                    RoleId = adminId
                },
                new ()
                {
                    Id = Guid.NewGuid(),
                    CreatedBy = Shared.Constant.Common.DefaultCreator,
                    CreatedDate = DateTimeOffset.UtcNow,
                    Email = "visitor@domain.com",
                    Name = "Visitor",
                    NickName = "Hellen",
                    Password = "Hellen",
                    RoleId = adminId
                }
            });
        }
    }
}