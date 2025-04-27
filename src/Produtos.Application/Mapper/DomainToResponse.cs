using Produtos.Domain.Models;
using Produtos.Application.Responses;

namespace Produtos.Application.Mapper
{
    public static class DomainToResponse
    {
        public static CategoriaResponse ToResponse(this Categoria src)
            => new()
            {
                Id = src.Id,
                Nome = src.Nome
            };

        public static ProdutoResponse ToResponse(this Produto src)
            => new()
            {
                Id =  src.Id,
                Nome = src.Nome,
                Valor = src.Valor,
                Descricao = src.Descricao,
                Categoria = src.Categoria?.ToResponse(),
            };
    }
}
