using Produtos.Data;
using Produtos.Domain;
using Produtos.Application;
using Produtos.Data.Configurations;
using Produtos.Api.Configurations.Swagger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services
    .DomainConfigure(builder.Configuration)
    .DataConfigure()
    .ApplicationConfigure()
    .AddSwaggerDocumentation(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UsePathBase($"/product");

app.UseSwaggerDocumentation();

//Enabled to run migrations
app.Services.GetService<IServiceScopeFactory>()?.CreateScope().ServiceProvider?.UpdateDatabase();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
