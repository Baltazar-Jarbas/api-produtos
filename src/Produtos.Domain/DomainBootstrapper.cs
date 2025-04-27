using Produtos.Domain.Services;
using Produtos.Domain.Providers;
using Produtos.Domain.Notifications;
using Microsoft.Extensions.Configuration;
using Produtos.Domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using Produtos.Domain.Interfaces.Notifications;

namespace Produtos.Domain
{
    public static class DomainBootstrapper
    {
        public static IServiceCollection DomainConfigure(this IServiceCollection services, IConfiguration configuration)
        {
            var appConfig = new AppConfig
            {
                DbContext = new DbSettingsProvider
                {
                    AppConnectionString = configuration.GetConnectionString("Default")
                }
            };
            
            services.AddSingleton(typeof(AppConfig), appConfig);
            services.AddScoped<IProdutoService, ProdutoService>();
            services.AddScoped<ICategoriaService, CategoriaService>();
            services.AddScoped<IHandler<DomainNotification>, DomainNotificationHandler>();

            return services;
        } 
    }
}
