namespace Produtos.Domain.Interfaces.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> CommitAsync();
    }
}
