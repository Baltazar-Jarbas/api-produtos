using Produtos.Domain.Notifications;
using Produtos.Domain.Interfaces.Notifications;
using Produtos.Domain.Interfaces.Services.Base;

namespace Produtos.Domain.Services.Base
{
    public class BaseService : IBaseService
    {
        protected IHandler<DomainNotification> Notifications { get; }

        public BaseService(IHandler<DomainNotification> notifications)
        {
            Notifications = notifications;
        }
    }
}
