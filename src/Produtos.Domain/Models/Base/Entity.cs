namespace Produtos.Domain.Models.Base
{
    public abstract class Entity
    {
        public Guid Id { get; set; } = new Guid();
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime ModifiedAt { get; set; } = DateTime.Now;

        public void Update()
        {
            ModifiedAt = DateTime.Now;
        }

        public void Delete()
        {
            Update();
            IsDeleted = true;
        }
    }
}
