using Produtos.Application.Responses.Base;

namespace Produtos.Application.Responses
{
    public class ProdutoResponse : ResponseBase
    {
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public string Descricao { get; set; }
        public CategoriaResponse Categoria { get; set; }
    }
}
