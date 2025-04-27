using Produtos.Application.Mapper;
using Produtos.Application.Requests;
using Microsoft.EntityFrameworkCore;
using Produtos.Application.Responses;
using Produtos.Domain.Notifications;
using Produtos.Domain.Interfaces.UoW;
using Produtos.Application.UseCases.Base;
using Produtos.Domain.Interfaces.Services;
using Produtos.Domain.Interfaces.Notifications;

namespace Produtos.Application.UseCases.Produtos
{
    public class EditarProdutoUseCase : UseCaseBase<EditarProdutoRequest, ProdutoResponse>
    {
        private readonly IProdutoService _domainService;

        public EditarProdutoUseCase(
            IProdutoService domainService,
            IHandler<DomainNotification> notifications,
            IUnitOfWork unitOfWork) : base(notifications, unitOfWork)
        {
            _domainService = domainService;
        }

        public override async Task<ProdutoResponse> HandleSafeMode(EditarProdutoRequest request, CancellationToken cancellationToken)
        {
            var entity = await _domainService.GetAllQuery.FirstOrDefaultAsync(x => x.Id.Equals(request.Id), cancellationToken);

            if (entity == null)
            {
                Notifications.Handle(DomainNotification.ModelValidation("EditarPRoduto", "Produto não encontrado"));
                return default;
            }

            request.ToEntity(entity);

            await CommitAsync();

            return entity.ToResponse();
        }
    }
}
