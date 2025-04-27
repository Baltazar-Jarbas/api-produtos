using Produtos.Domain.Models.Base;

namespace Produtos.Domain.Models
{
    public class ImagemProduto : Entity 
    {
        public Guid ProdutoId { get; set; }
        public string Arquivo { get; set; }

        public Produto Produto { get; set; }
    }
}
