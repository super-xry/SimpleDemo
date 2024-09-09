using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using SimpleDemo.Application;
using SimpleDemo.Infrastructure.Repository;
using SimpleDemo.ServiceDefaults;
using System.Text.RegularExpressions;

namespace SimpleDemo.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.AddServiceDefaults();

            // Add services to the container.

            builder.Services.AddControllers(options =>
            {
                options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDatabases(builder.Configuration);
            builder.Services.AddCommereApiServices();

            var app = builder.Build();

            if (builder.Configuration["AutoMigrating"] == bool.TrueString)
            {
                var dbContext = app.Services.CreateScope().ServiceProvider.GetRequiredService<CommerceDbContext>();
                dbContext.Database.Migrate();
            }

            app.MapDefaultEndpoints();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

        /// <summary>
        /// <see cref="https://learn.microsoft.com/en-us/aspnet/core/fundamentals/routing?view=aspnetcore-7.0#parameter-transformers"/>
        /// </summary>
        public class SlugifyParameterTransformer : IOutboundParameterTransformer
        {
            public string? TransformOutbound(object? value)
            {
                if (value == null) { return null; }

                return Regex.Replace(value.ToString()!,
                    "([a-z])([A-Z])",
                    "$1-$2",
                    RegexOptions.CultureInvariant,
                    TimeSpan.FromMilliseconds(100)).ToLowerInvariant();
            }
        }
    }
}