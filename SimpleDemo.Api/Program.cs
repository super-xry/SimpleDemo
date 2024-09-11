using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SimpleDemo.Api.Filter;
using SimpleDemo.Application;
using SimpleDemo.Infrastructure.Repository;
using SimpleDemo.Security.Extensions;
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
                options.Filters.Add<ModelStateAttribute>();
                options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
            }).AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
            });
            AddSwagger(builder.Services);

            builder.Services.AddDatabases(builder.Configuration);
            builder.Services.AddJwtAuth(builder.Configuration);
            builder.Services.AddCommereApiServices();

            var app = builder.Build();

            if (builder.Configuration["AutoMigrating"] == bool.TrueString)
            {
                var dbContext = app.Services.CreateScope().ServiceProvider.GetRequiredService<CommerceDbContext>();
                dbContext.Database.Migrate();
            }

            app.MapDefaultEndpoints();

            var isDevelopment = app.Environment.IsDevelopment();
            // Configure the HTTP request pipeline.
            if (isDevelopment)
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseExceptionHandler(applicationBuilder =>
            {
                applicationBuilder.UseCustomErrors(isDevelopment);
            });

            app.UseEventHandlers();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

        private static void AddSwagger(IServiceCollection services)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Simple demo commerce API",
                    Version = "v1",
                    Description = "SimpleDemo "
                });

                c.CustomSchemaIds(x => x.FullName);

                c.AddSecurityDefinition("Bearer", SwaggerSecuritySchemeHelper.GetApiSecurityScheme());

                c.AddSecurityRequirement(SwaggerSecuritySchemeHelper.GetSecurityRequirement());

                /*c.OperationFilter<AddAuthorizationHeaderOperationFilter>();

                c.OperationFilter<RemoveAuthorizationHeaderOperationFilter>();*/
            });
        }

        /// <summary>
        /// <see><cref>https://learn.microsoft.com/en-us/aspnet/core/fundamentals/routing?view=aspnetcore-7.0#parameter-transformers</cref></see>
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