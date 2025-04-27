using Produtos.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Produtos.Data.Configurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Produtos.Data.Configurations
{
    public class ImagemProdutoEntityTypeConfiguration : EntityTypeConfiguration<ImagemProduto>
    {
        public override void Configure(EntityTypeBuilder<ImagemProduto> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Arquivo)
                .HasColumnType("text")
                .IsRequired();

            builder.HasOne(x => x.Produto)
                .WithMany(x => x.Imagens)
                .HasForeignKey(x => x.ProdutoId);

            builder.HasIndex(x => x.Arquivo)
                .IsUnique();
        }
    }
}
