using Produtos.Domain.Models.Base;

namespace Produtos.Domain.Models
{
    public class Produto : Entity 
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public Guid CategoriaId { get; set; }

        public Categoria Categoria { get; set; }
        public List<ImagemProduto> Imagens { get; set; }
    }
}
