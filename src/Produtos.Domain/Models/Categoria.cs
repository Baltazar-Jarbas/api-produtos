using Produtos.Domain.Models.Base;

namespace Produtos.Domain.Models
{
    public class Categoria : Entity
    {
        public string Nome { get; set; }
        
        public List<Produto> Produtos { get; set; }
    }
}
