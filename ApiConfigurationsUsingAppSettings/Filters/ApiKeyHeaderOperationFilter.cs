using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ApiConfigurationsUsingAppSettings.Filters
{
    public class ApiKeyHeaderOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation == null)
            {
                throw new Exception("Invalid operation");
            }

            operation.Parameters.Add(new OpenApiParameter
            {
                In = ParameterLocation.Header,
                Name = "ApiKey",
                Description = "pass the Api Key here",
                Schema = new OpenApiSchema
                {
                    Type = "String"
                }
            });
        }
    }
}