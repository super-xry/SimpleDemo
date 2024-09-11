using Microsoft.OpenApi.Models;
using System.Diagnostics.CodeAnalysis;

namespace SimpleDemo.Security.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class SwaggerSecuritySchemeHelper
    {
        public static OpenApiSecurityScheme GetApiSecurityScheme()
        {
            return new OpenApiSecurityScheme
            {
                Description = @"JWT Authorization header using the Bearer scheme.

Enter 'Bearer' [space] and then your token in the text input below.

Example: 'Bearer <token>'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            };
        }

        public static OpenApiSecurityRequirement GetSecurityRequirement()
        {
            return new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            };
        }
    }
}