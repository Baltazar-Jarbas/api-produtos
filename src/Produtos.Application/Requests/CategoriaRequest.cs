using Produtos.Application.Requests.Base;

namespace Produtos.Application.Requests
{
    public abstract class CategoriaRequest : RequestBase { public string Nome { get; set; } }
    public class CriarCategoriaRequest : CategoriaRequest { }
    public class EditarCategoriaRequest : CategoriaRequest { public Guid Id { get; set; } }
    public class ListarCagegoriasRequest : RequestBase { public string Pesquisa { get; set; } }
    public class BuscarCategoriaPorIdRequest : RequestBase { public Guid Id { get; set; } }
}
