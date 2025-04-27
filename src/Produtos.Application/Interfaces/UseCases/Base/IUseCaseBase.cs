using Produtos.Application.Requests.Base;
using Produtos.Application.Responses.Base;

namespace Produtos.Application.Interfaces.UseCases.Base
{
    public interface IUseCaseBase<TRequest, TResponse> 
        where TRequest : RequestBase, new() 
    {

        Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
