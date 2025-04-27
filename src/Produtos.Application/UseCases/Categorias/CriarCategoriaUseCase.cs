using Produtos.Application.Mapper;
using Produtos.Application.Requests;
using Produtos.Domain.Notifications;
using Produtos.Application.Responses;
using Produtos.Domain.Interfaces.UoW;
using Produtos.Application.UseCases.Base;
using Produtos.Domain.Interfaces.Services;
using Produtos.Domain.Interfaces.Notifications;

namespace Produtos.Application.UseCases.Categorias
{
    public class CriarCategoriaUseCase : UseCaseBase<CriarCategoriaRequest, CategoriaResponse>
    {

        private readonly ICategoriaService _domainService;
        
        public CriarCategoriaUseCase(
            ICategoriaService domainService,
            IHandler<DomainNotification> notifications, 
            IUnitOfWork unitOfWork) : base(notifications, unitOfWork)
        {
            _domainService = domainService;
        }

        public override async Task<CategoriaResponse> HandleSafeMode(CriarCategoriaRequest request, CancellationToken cancellationToken)
        {
            var entity = await _domainService.RegisterAsync(request.ToEntity());
            await CommitAsync();

            return entity.ToResponse();
        }
    }
}
