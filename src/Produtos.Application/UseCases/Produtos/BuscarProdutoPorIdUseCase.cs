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
    public class BuscarProdutoPorIdUseCase : UseCaseBase<BuscarProdutoPorIdRequest, ProdutoResponse>
    {
        private readonly IProdutoService _domainService;

        public BuscarProdutoPorIdUseCase(
            IProdutoService domainService,
            IHandler<DomainNotification> notifications,
            IUnitOfWork unitOfWork) : base(notifications, unitOfWork)
        {
            _domainService = domainService;
        }

        public override async Task<ProdutoResponse> HandleSafeMode(BuscarProdutoPorIdRequest request, CancellationToken cancellationToken)
        {
            var entity = await _domainService.GetAllQueryAsNoTracking
                .Include(x => x.Categoria)
                .FirstOrDefaultAsync(x => x.Id.Equals(request.Id));

            return entity.ToResponse();
        }
    }

}
