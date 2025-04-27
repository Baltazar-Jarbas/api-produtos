using Produtos.Domain.Models.Base;
using Produtos.Domain.Notifications;
using Produtos.Domain.Interfaces.Notifications;
using Produtos.Domain.Interfaces.Services.Base;
using Produtos.Domain.Interfaces.Repositories.Base;

namespace Produtos.Domain.Services.Base
{
    public abstract class BaseServiceEntity<TEntity> : BaseService, IBaseServiceEntity<TEntity> where TEntity : Entity
    {
        protected IBaseRepository<TEntity> BaseRepository { get; }

        public virtual IQueryable<TEntity> GetAllQuery => BaseRepository.GetAllQuery;

        public virtual IQueryable<TEntity> GetAllQueryAsNoTracking => BaseRepository.GetAllQueryNoTracking;

        public BaseServiceEntity(IBaseRepository<TEntity> baseRepository,
                                 IHandler<DomainNotification> notifications)
            : base(notifications)
        {
            BaseRepository = baseRepository;
        }

        public virtual async Task<TEntity> GetByIdAsync(Guid id)
        {
            if (id == default)
                return default;

            return await BaseRepository.GetByIdAsync(id);
        }

        public virtual async Task<bool> ExistsAsync(Guid id)
            => await BaseRepository.ExistsAsync(id);

        public virtual async Task<TEntity> RegisterAsync(TEntity entity)
        {
            if (entity.Id == default)
            {
                entity.Id = Guid.NewGuid();
                entity.CreatedAt = DateTime.UtcNow;
                entity.ModifiedAt = DateTime.UtcNow;
            }

            var registeredEntity = await BaseRepository.AddAsync(entity);
            return registeredEntity;
        }
    }
}
