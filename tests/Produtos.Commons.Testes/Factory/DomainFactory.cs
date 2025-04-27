using Bogus;
using Produtos.Domain.Models;

namespace Produtos.Commons.Testes.Factory
{
    public static class DomainFactory
    {
        public static Produto GetProduto
          => new Faker<Produto>()
            .RuleFor(x => x.Id, f => f.Random.Guid())
            .RuleFor(x => x.Nome, f => f.Random.Word())
            .RuleFor(x => x.Categoria, f => GetCategoria)
            .RuleFor(x => x.Descricao, f => f.Lorem.Paragraph())
            .RuleFor(x => x.Valor, f => f.Random.Decimal(999, 999999));

        public static Categoria GetCategoria
            => new Faker<Categoria>()
            .RuleFor(x => x.Id, f=> f.Random.Guid())
            .RuleFor(x => x.Nome, f => f.Random.Words(2));
    }
}
