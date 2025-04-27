using System.Reflection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Produtos.Api.Configurations.Swagger
{
    public static class SwaggerSetup
    {
        public static void AddSwaggerDocumentation(this IServiceCollection services, IConfiguration configuration)
        {
            ArgumentNullException.ThrowIfNull(services);
            ArgumentNullException.ThrowIfNull(configuration);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("Produtos",
                    new OpenApiInfo
                    {
                        Title = $"Produtos.Api",
                        Version = "V1",
                        Description = "Gerenciamento de produtos"
                    });

                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));

                OpenApiSecurityScheme securityDefinition = new OpenApiSecurityScheme()
                {
                    Name = "Bearer",
                    BearerFormat = "JWT",
                    Scheme = "bearer",
                    Description = "Specify the authorization token.",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                };
                c.AddSecurityDefinition("jwt_auth", securityDefinition);

                OpenApiSecurityScheme securityScheme = new OpenApiSecurityScheme()
                {
                    Reference = new OpenApiReference()
                    {
                        Id = "jwt_auth",
                        Type = ReferenceType.SecurityScheme
                    }
                };

                OpenApiSecurityRequirement securityRequirements = new OpenApiSecurityRequirement()
                {
                    {securityScheme, Array.Empty<string>()},
                };

                c.AddSecurityRequirement(securityRequirements);
                c.OperationFilter<AuthorizeCheckOperationFilter>();
                c.CustomSchemaIds(x => x.FullName);
            });
        }

        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/v1/swagger.json", "Produtos Api");
                c.RoutePrefix = string.Empty;
                c.DocExpansion(DocExpansion.None);
            });
            return app;
        }
    }
}

