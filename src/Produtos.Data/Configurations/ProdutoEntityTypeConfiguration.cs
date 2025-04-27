using Produtos.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Produtos.Data.Configurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Produtos.Data.Configurations
{
    public class ProdutoEntityTypeConfiguration : EntityTypeConfiguration<Produto> 
    {
        public override void Configure(EntityTypeBuilder<Produto> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Nome)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(x => x.Descricao)
                .HasColumnType("text");


            builder.HasIndex(x => x.Nome)
                .IsUnique();

            builder.HasOne(x => x.Categoria)
                .WithMany(f => f.Produtos)
                .HasForeignKey(x => x.CategoriaId);
        }
    }
}
