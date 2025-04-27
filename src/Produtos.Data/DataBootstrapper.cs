using Produtos.Data.UoW;
using Produtos.Domain.Interfaces.UoW;
using Produtos.Data.Repositories.Base;
using Microsoft.Extensions.DependencyInjection;
using Produtos.Domain.Interfaces.Repositories.Base;

namespace Produtos.Data
{
    public static class DataBootstrapper
    {
        public static IServiceCollection DataConfigure(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            return services;
        }
    }
}
