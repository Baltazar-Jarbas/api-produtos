using Produtos.Application.Mapper;
using Produtos.Application.Requests;
using Microsoft.EntityFrameworkCore;
using Produtos.Domain.Notifications;
using Produtos.Application.Responses;
using Produtos.Domain.Interfaces.UoW;
using Produtos.Application.UseCases.Base;
using Produtos.Domain.Interfaces.Services;
using Produtos.Domain.Interfaces.Notifications;

namespace Produtos.Application.UseCases.Categorias
{
    public class ListarCagegoriasUseCase : UseCaseBase<ListarCagegoriasRequest, IEnumerable<CategoriaResponse>>
    {
        private readonly ICategoriaService _domainService;

        public ListarCagegoriasUseCase(
            ICategoriaService domainService,
            IHandler<DomainNotification> notifications,
            IUnitOfWork unitOfWork) : base(notifications, unitOfWork)
        {
            _domainService = domainService;
        }

        public override async Task<IEnumerable<CategoriaResponse>> HandleSafeMode(ListarCagegoriasRequest request, CancellationToken cancellationToken)
        {
            var query = _domainService.GetAllQueryAsNoTracking;
            if (!string.IsNullOrEmpty(request.Pesquisa))
                query = query.Where(x => x.Nome.Contains(request.Pesquisa));

            var entities = await query.ToListAsync(cancellationToken);

            return entities.Select(x => x.ToResponse());
        }
    }
}
