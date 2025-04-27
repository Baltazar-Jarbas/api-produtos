using Produtos.Application.Requests.Base;

namespace Produtos.Application.Requests
{
    public abstract class ProdutoRequest : RequestBase
    {
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public string Descricao { get; set; }
        public Guid CategoriaId { get; set; }
    }

    public class CriarProdutoRequest : ProdutoRequest { }
    public class EditarProdutoRequest : ProdutoRequest { public Guid Id { get; set; } }
    public class BuscarProdutoPorIdRequest : RequestBase { public Guid Id { get; set; } }
    public class RemoverProdutoRequest : RequestBase { public Guid Id { get; set; } }
    public class ListarProdutoRequest : RequestBase { public string Nome { get; set; } public Guid? CategoriaId { get; set; } public decimal? PrecoInicial { get; set; } public decimal? PrecoFinal { get; set; } }
}
