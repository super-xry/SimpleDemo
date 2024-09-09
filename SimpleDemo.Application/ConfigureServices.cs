using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleDemo.Application.Commerce.User.Handler;
using SimpleDemo.Application.Commerce.User.Query;
using SimpleDemo.Application.DataTransfer;
using SimpleDemo.Domain.DbEntity.CommerceEntity;
using SimpleDemo.Infrastructure.Command;
using SimpleDemo.Infrastructure.Query;
using SimpleDemo.Infrastructure.Repository;

namespace SimpleDemo.Application
{
    public static class ConfigureServices
    {
        public static void AddCommereApiServices(this IServiceCollection services)
        {
            services.AddServiceBus();
            services.AddScoped<IQueryHandler<UserQuery, Task<ICollection<UserDto>>>, UserQueryHandler>();
        }

        public static void AddAdminApiServices(this IServiceCollection services)
        {
            services.AddServiceBus();
        }

        public static void AddCommonService(this IServiceCollection services)
        {
            services.AddServiceBus();
        }

        public static void AddDatabases(this IServiceCollection services, IConfiguration configuration)
        {
            var commerceDbConnectionString = configuration.GetConnectionString("CommerceDbConnectionString");
            services.AddDbContext<CommerceDbContext>(options => options.UseSqlite(commerceDbConnectionString));

            services.AddTransient<ICommerceRepository, CommerceRepository>();
            services.AddTransient<Func<ICommerceRepository>>(provider => () => provider.CreateScope().ServiceProvider.GetRequiredService<ICommerceRepository>());
        }

        private static void AddServiceBus(this IServiceCollection services)
        {
            services.AddScoped<ICommandBus>(it =>
            {
                var commandBus = new InProcessCommandBus(t => (ICommandHandler)it.GetService(t)!);

                return commandBus;
            });
            services.AddScoped<IQueryBus>(it =>
            {
                var queryBus = new InProcessQueryBus(t => (IQueryHandler)it.GetService(t)!);
                return queryBus;
            });
        }
    }
}