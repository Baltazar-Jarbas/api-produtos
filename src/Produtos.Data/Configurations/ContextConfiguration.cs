using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Produtos.Data.Configurations
{
    public static class ContextConfiguration
    {
        public static void UpdateDatabase(this IServiceProvider serviceProvider)
        {
            using var serviceScope = serviceProvider?.CreateScope();

            using var context = serviceScope?.ServiceProvider.GetService<ApplicationDbContext>();

            context?.Database.Migrate();
        }
    }
}
