using System.Text.Json;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Authorization;

namespace Produtos.Api.Configurations.Swagger
{
    internal class AuthorizeCheckOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var hasAnonymous =
                context.MethodInfo.DeclaringType.GetCustomAttributes(true).OfType<AllowAnonymousAttribute>().Any()
                || context.MethodInfo.GetCustomAttributes(true).OfType<AllowAnonymousAttribute>().Any();

            if (!hasAnonymous)
            {
                operation.Responses.Add("400", new OpenApiResponse { Description = "BadRequest", Content = GetBadRequestContent });
                operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });
                operation.Responses.Add("403", new OpenApiResponse { Description = "Forbidden" });
                operation.Responses.Add("404", new OpenApiResponse { Description = "NotFound" });
                operation.Responses.Add("500", new OpenApiResponse { Description = "InternalServerError" });


                operation.Security = new List<OpenApiSecurityRequirement>
                {
                    new OpenApiSecurityRequirement
                    {
                        [
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference()
                                {
                                    Id = "jwt_auth",
                                    Type = ReferenceType.SecurityScheme
                                }
                            }
                        ] =  Array.Empty<string>()
                    }
                };
            }
        }

        private static Dictionary<string, OpenApiMediaType> GetBadRequestContent
            => new()
            {
                {
                     "BadRequestResult",
                    new OpenApiMediaType
                    {
                        Schema = new OpenApiSchema
                        {
                            Type = "json",
                            Example = new OpenApiString(JsonSerializer.Serialize(new Dictionary<string, string[]> { { "EXE-001", new[] { "Validation falied example" } } }))
                        }
                    }
                }
            };
    }
}

