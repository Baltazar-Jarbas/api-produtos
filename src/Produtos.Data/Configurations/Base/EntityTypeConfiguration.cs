using Produtos.Domain.Models.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Produtos.Data.Configurations.Base
{
    public abstract class EntityTypeConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : Entity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();

            builder.Property(x => x.CreatedAt)
                   .HasDefaultValueSql("now()")
                   .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Save);

            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }
}
