using Produtos.Domain.Models;
using Produtos.Domain.Notifications;
using Produtos.Domain.Services.Base;
using Produtos.Domain.Interfaces.Services;
using Produtos.Domain.Interfaces.Notifications;
using Produtos.Domain.Interfaces.Repositories.Base;

namespace Produtos.Domain.Services
{
    public class ProdutoService : BaseServiceEntity<Produto>, IProdutoService
    {
        public ProdutoService(
            IBaseRepository<Produto> baseRepository, 
            IHandler<DomainNotification> notifications) : base(baseRepository, notifications)
        {
        }
    }
}
