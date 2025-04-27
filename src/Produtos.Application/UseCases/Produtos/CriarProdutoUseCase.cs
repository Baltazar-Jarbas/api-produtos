using Produtos.Application.Mapper;
using Produtos.Application.Requests;
using Produtos.Application.Responses;
using Produtos.Domain.Notifications;
using Produtos.Domain.Interfaces.UoW;
using Produtos.Application.UseCases.Base;
using Produtos.Domain.Interfaces.Services;
using Produtos.Domain.Interfaces.Notifications;

namespace Produtos.Application.UseCases.Produtos
{
    public class CriarProdutoUseCase : UseCaseBase<CriarProdutoRequest, ProdutoResponse>
    {
        private readonly IProdutoService _domainService;

        public CriarProdutoUseCase(
            IProdutoService domainService,
            IHandler<DomainNotification> notifications,
            IUnitOfWork unitOfWork) : base(notifications, unitOfWork)
        {
            _domainService = domainService;
        }

        public override async Task<ProdutoResponse> HandleSafeMode(CriarProdutoRequest request, CancellationToken cancellationToken)
        {
            var entity = await _domainService.RegisterAsync(request.ToEntity());
            await CommitAsync();

            return entity.ToResponse();
        }
    }
}
