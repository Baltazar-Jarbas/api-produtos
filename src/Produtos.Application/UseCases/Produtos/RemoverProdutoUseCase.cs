using Produtos.Application.Requests;
using Microsoft.EntityFrameworkCore;
using Produtos.Domain.Notifications;
using Produtos.Domain.Interfaces.UoW;
using Produtos.Application.UseCases.Base;
using Produtos.Domain.Interfaces.Services;
using Produtos.Domain.Interfaces.Notifications;

namespace Produtos.Application.UseCases.Produtos
{
    public class RemoverProdutoUseCase : UseCaseBase<RemoverProdutoRequest, bool>
    {
        private readonly IProdutoService _domainService;

        public RemoverProdutoUseCase(
            IProdutoService domainService,
            IHandler<DomainNotification> notifications,
            IUnitOfWork unitOfWork) : base(notifications, unitOfWork)
        {
            _domainService = domainService;
        }

        public override async Task<bool> HandleSafeMode(RemoverProdutoRequest request, CancellationToken cancellationToken)
        {
            var entity = await _domainService.GetAllQuery.FirstOrDefaultAsync(x => x.Id.Equals(request.Id), cancellationToken);

            if (entity == null)
            {
                Notifications.Handle(DomainNotification.ModelValidation("RemoverPRoduto", "Produto não encontrado"));
                return false;
            }

            entity.Delete();

            return await CommitAsync();
        }
    }
}
