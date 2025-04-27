using Produtos.Domain.Models.Base;

namespace Produtos.Domain.Interfaces.Services.Base
{
    public interface IBaseServiceEntity<TEntity> : IBaseService where TEntity : Entity
    {
        Task<TEntity> RegisterAsync(TEntity entity);
        IQueryable<TEntity> GetAllQuery { get; }
        IQueryable<TEntity> GetAllQueryAsNoTracking { get; }
        Task<TEntity> GetByIdAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
    }
}

