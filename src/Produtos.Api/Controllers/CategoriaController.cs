using Microsoft.AspNetCore.Mvc;
using Produtos.Application.Requests;
using Produtos.Api.Controllers.Base;
using Produtos.Domain.Notifications;
using Produtos.Application.Responses;
using Swashbuckle.AspNetCore.Annotations;
using Produtos.Domain.Interfaces.Notifications;
using Produtos.Application.Interfaces.UseCases.Base;

namespace Produtos.Api.Controllers
{
    public class CategoriaController(IHandler<DomainNotification> notifications) : MainController(notifications)
    {

        /// <summary>
        /// Criar Categoria
        /// </summary>
        /// <returns></returns>
        ///
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, null, typeof(CategoriaResponse))]
        public async Task<ActionResult<CategoriaResponse>> PostAsync(IUseCaseBase<CriarCategoriaRequest, CategoriaResponse> useCase, [FromBody] CriarCategoriaRequest request, CancellationToken cancellationToken)
            => ResponseGet(await useCase.Handle(request, cancellationToken));

        /// <summary>
        /// Editar Categoria
        /// </summary>
        /// <returns></returns>
        ///
        [HttpPut]
        [SwaggerResponse(StatusCodes.Status204NoContent, null, null)]
        public async Task<ActionResult<CategoriaResponse>> PutAsync(IUseCaseBase<EditarCategoriaRequest, CategoriaResponse> useCase, [FromBody] EditarCategoriaRequest request, CancellationToken cancellationToken)
        {
            await useCase.Handle(request, cancellationToken);
            return ResponsePutPatch();
        }


        /// <summary>
        /// Listar Categorias
        /// </summary>
        /// <returns></returns>
        ///
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, null, typeof(IEnumerable<CategoriaResponse>))]
        public async Task<ActionResult<IEnumerable<CategoriaResponse>>> GetAsync(IUseCaseBase<ListarCagegoriasRequest, IEnumerable<CategoriaResponse>> useCase, [FromQuery] string pesquisa, CancellationToken cancellationToken)
            => ResponseGet(await useCase.Handle(new ListarCagegoriasRequest { Pesquisa = pesquisa }, cancellationToken));

        /// <summary>
        /// Buscar Categoria por id
        /// </summary>
        /// <returns></returns>
        ///
        [HttpGet("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, null, typeof(CategoriaResponse))]
        public async Task<ActionResult<CategoriaResponse>> GetAsync(IUseCaseBase<BuscarCategoriaPorIdRequest, CategoriaResponse> useCase, [FromRoute] Guid id, CancellationToken cancellationToken)
            => ResponseGet(await useCase.Handle(new BuscarCategoriaPorIdRequest { Id = id}, cancellationToken));
    }
}
