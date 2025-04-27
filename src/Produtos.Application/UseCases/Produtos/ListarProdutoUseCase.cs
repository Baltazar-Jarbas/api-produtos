using Produtos.Application.Mapper;
using Microsoft.EntityFrameworkCore;
using Produtos.Application.Requests;
using Produtos.Application.Responses;
using Produtos.Domain.Notifications;
using Produtos.Domain.Interfaces.UoW;
using Produtos.Application.UseCases.Base;
using Produtos.Domain.Interfaces.Services;
using Produtos.Domain.Interfaces.Notifications;

namespace Produtos.Application.UseCases.Produtos
{
    public class ListarProdutoUseCase : UseCaseBase<ListarProdutoRequest, IEnumerable<ProdutoResponse>>
    {
        private readonly IProdutoService _domainService;

        public ListarProdutoUseCase(
            IProdutoService domainService,
            IHandler<DomainNotification> notifications,
            IUnitOfWork unitOfWork) : base(notifications, unitOfWork)
        {
            _domainService = domainService;
        }

        public override async Task<IEnumerable<ProdutoResponse>> HandleSafeMode(ListarProdutoRequest request, CancellationToken cancellationToken)
        {
            var query = _domainService.GetAllQueryAsNoTracking;

            if(request.CategoriaId is not null)
                query = query.Where(x => x.CategoriaId == request.CategoriaId);
            if (request.PrecoInicial is not null)
                query = query.Where(x => x.Valor >= request.PrecoInicial);
            if(!string.IsNullOrEmpty(request.Nome))
                query = query.Where(x => x.Nome.Contains(request.Nome));

            var entities = await query.ToListAsync(cancellationToken);

            return entities.Select(x => x.ToResponse());
        }
    }
}
