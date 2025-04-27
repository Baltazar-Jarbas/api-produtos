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
    public class EditarCategoriaUseCase : UseCaseBase<EditarCategoriaRequest, CategoriaResponse>
    {
        private readonly ICategoriaService _domainService;

        public EditarCategoriaUseCase(
            ICategoriaService domainService,
            IHandler<DomainNotification> notifications,
            IUnitOfWork unitOfWork) : base(notifications, unitOfWork)
        {
            _domainService = domainService;
        }

        public override async Task<CategoriaResponse> HandleSafeMode(EditarCategoriaRequest request, CancellationToken cancellationToken)
        {
            var entity = await _domainService.GetAllQuery.FirstOrDefaultAsync(x => x.Id.Equals(request.Id), cancellationToken);

            if (entity == null) 
            {
                Notifications.Handle(DomainNotification.ModelValidation("EditarCategoria", "Categoria não encontrada"));
                return default;
            }

            request.ToEntity(entity);

            await CommitAsync();

            return entity.ToResponse();
        }
    }
}
