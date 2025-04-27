using Produtos.Application.Requests;
using Produtos.Application.Responses;
using Produtos.Application.UseCases.Produtos;
using Microsoft.Extensions.DependencyInjection;
using Produtos.Application.UseCases.Categorias;
using Produtos.Application.Interfaces.UseCases.Base;

namespace Produtos.Application
{
    public static class ApplicationBootstrapper
    {
        public static IServiceCollection ApplicationConfigure(this IServiceCollection services)
        {

            services.AddScoped<IUseCaseBase<CriarCategoriaRequest, CategoriaResponse>, CriarCategoriaUseCase>();
            services.AddScoped<IUseCaseBase<EditarCategoriaRequest, CategoriaResponse>, EditarCategoriaUseCase>();
            services.AddScoped<IUseCaseBase<BuscarCategoriaPorIdRequest, CategoriaResponse>, BuscarCategoriaPorIdUseCase>();
            services.AddScoped<IUseCaseBase<ListarCagegoriasRequest, IEnumerable<CategoriaResponse>>, ListarCagegoriasUseCase>();

            services.AddScoped<IUseCaseBase<RemoverProdutoRequest, bool>, RemoverProdutoUseCase>();
            services.AddScoped<IUseCaseBase<CriarProdutoRequest, ProdutoResponse>, CriarProdutoUseCase>();
            services.AddScoped<IUseCaseBase<EditarProdutoRequest, ProdutoResponse>, EditarProdutoUseCase>();
            services.AddScoped<IUseCaseBase<BuscarProdutoPorIdRequest, ProdutoResponse>, BuscarProdutoPorIdUseCase>();
            services.AddScoped<IUseCaseBase<ListarProdutoRequest, IEnumerable<ProdutoResponse>>, ListarProdutoUseCase>();

            return services;
        }
    }
}
