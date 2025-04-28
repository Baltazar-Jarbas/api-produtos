namespace Produtos.Domain.Models.Base
{
    public abstract class Entity
    {
        public Guid Id { get; set; } = new Guid();
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;

        public void Update()
        {
            ModifiedAt = DateTime.UtcNow;
        }

        public void Delete()
        {
            Update();
            IsDeleted = true;
        }
    }
}
