using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SimpleDemo.Api.Filter
{
    public class AddAuthorizationHeaderOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters ??= new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "x-simple-app",
                In = ParameterLocation.Header,
                Required = true,
                Schema = new OpenApiSchema
                {
                    Type = "string"
                },
                Description = "Bearer <token>"
            });
        }
    }

    public class RemoveAuthorizationHeaderOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var actionName = context.ApiDescription.ActionDescriptor.DisplayName;

            if (string.IsNullOrEmpty(actionName) || !actionName.Contains("login", StringComparison.OrdinalIgnoreCase)) return;

            var authorizationHeader = operation.Parameters.FirstOrDefault(p => p.Name == "x-simple-app");
            if (authorizationHeader == null) return;
            operation.Parameters.Remove(authorizationHeader);
        }
    }
}