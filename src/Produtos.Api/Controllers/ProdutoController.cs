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
    public class ProdutoController(IHandler<DomainNotification> notifications) : MainController(notifications)
    {

        /// <summary>
        /// Criar Produto
        /// </summary>
        /// <returns></returns>
        ///
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status201Created, null, typeof(ProdutoResponse))]
        public async Task<ActionResult<ProdutoResponse>> PostAsync(IUseCaseBase<CriarProdutoRequest, ProdutoResponse> useCase, [FromBody] CriarProdutoRequest request, CancellationToken cancellationToken)
            => ResponsePost("", "", await useCase.Handle(request, cancellationToken));

        /// <summary>
        /// Editar Produto
        /// </summary>
        /// <returns></returns>
        ///
        [HttpPut]
        [SwaggerResponse(StatusCodes.Status204NoContent, null, null)]
        public async Task<ActionResult> PutAsync(IUseCaseBase<EditarProdutoRequest, ProdutoResponse> useCase, [FromBody] EditarProdutoRequest request, CancellationToken cancellationToken)
        {
            await useCase.Handle(request, cancellationToken);
            return ResponsePutPatch();
        }


        /// <summary>
        /// Listar Produtos
        /// </summary>
        /// <returns></returns>
        ///
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, null, typeof(IEnumerable<ProdutoResponse>))]
        public async Task<ActionResult<IEnumerable<ProdutoResponse>>> GetAsync(IUseCaseBase<ListarProdutoRequest, IEnumerable<ProdutoResponse>> useCase, [FromQuery] ListarProdutoRequest request, CancellationToken cancellationToken)
            => ResponseGet(await useCase.Handle(request, cancellationToken));

        /// <summary>
        /// Buscar Produto por id
        /// </summary>
        /// <returns></returns>
        ///
        [HttpGet("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, null, typeof(ProdutoResponse))]
        public async Task<ActionResult<ProdutoResponse>> GetAsync(IUseCaseBase<BuscarProdutoPorIdRequest, ProdutoResponse> useCase, [FromRoute] Guid id, CancellationToken cancellationToken)
            => ResponseGet(await useCase.Handle(new BuscarProdutoPorIdRequest { Id = id }, cancellationToken));

        /// <summary>
        /// Remover Produto por id
        /// </summary>
        /// <returns></returns>
        ///
        [HttpDelete("{id}")]
        [SwaggerResponse(StatusCodes.Status204NoContent, null, null)]
        public async Task<ActionResult> DeleteAsync(IUseCaseBase<RemoverProdutoRequest, bool> useCase, [FromRoute] Guid id, CancellationToken cancellationToken)
        {
            await useCase.Handle(new RemoverProdutoRequest { Id = id }, cancellationToken);
            return ResponsePutPatch();
        }
    }
}
