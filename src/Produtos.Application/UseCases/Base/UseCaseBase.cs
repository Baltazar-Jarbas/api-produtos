using Produtos.Domain.Notifications;
using Produtos.Domain.Interfaces.UoW;
using Produtos.Application.Requests.Base;
using Produtos.Domain.Interfaces.Notifications;
using Produtos.Application.Interfaces.UseCases.Base;

namespace Produtos.Application.UseCases.Base
{
    public abstract class UseCaseBase<TRequest, TResponse> : IUseCaseBase<TRequest, TResponse>
        where TRequest : RequestBase, new()
    {
        private readonly IUnitOfWork _unitOfWork;
        protected IHandler<DomainNotification> Notifications { get; }


        protected UseCaseBase(
            IHandler<DomainNotification> notifications, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            Notifications = notifications;
        }

        protected async Task<bool> CommitAsync(bool forceCommit = false)
        {
            if (forceCommit)
                return await _unitOfWork.CommitAsync();

            if (Notifications.HasNotifications())
                return false;

            return await _unitOfWork.CommitAsync();
        }

        public abstract Task<TResponse> HandleSafeMode(TRequest request, CancellationToken cancellationToken);

        public virtual async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
            => await HandleSafeMode(request, cancellationToken);
    }
}
