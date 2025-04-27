using Produtos.Domain.Models;
using Produtos.Application.Requests;

namespace Produtos.Application.Mapper
{
    public static class RequestToDomain
    {
        public static Categoria ToEntity(this CriarCategoriaRequest src)
            => new()
            {
                Nome = src.Nome,
            };

        public static Categoria ToEntity(this EditarCategoriaRequest src, Categoria entity)
        {
            entity.Update();
            entity.Id = src.Id;
            entity.Nome = src.Nome;

            return entity;
        }

        public static Produto ToEntity(this CriarProdutoRequest src)
            => new()
            {
                Nome = src.Nome,
                Valor = src.Valor,
                Descricao = src.Descricao,
                CategoriaId = src.CategoriaId,
            };

        public static Produto ToEntity(this EditarProdutoRequest src, Produto entity)
        {
            entity.Update();
            entity.Id = src.Id;
            entity.Nome = src.Nome;
            entity.Valor = src.Valor;
            entity.Descricao = src.Descricao;
            entity.CategoriaId = src.CategoriaId;

            return entity;
        }
            
    }
}
