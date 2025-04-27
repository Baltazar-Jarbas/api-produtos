using Produtos.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Produtos.Data.Configurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Produtos.Data.Configurations
{
    public class CategoriaEntityTypeConfiguration : EntityTypeConfiguration<Categoria>
    {
        public override void Configure(EntityTypeBuilder<Categoria> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Nome)
                .HasColumnType("varchar(100)")
                .IsRequired();


            builder.HasIndex(x => x.Nome)
                .IsUnique();
        }
    }
}
