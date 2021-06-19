using Core.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class TechnicalSheetConfiguration : IEntityTypeConfiguration<TechnicalSheet>
    {
        public void Configure(EntityTypeBuilder<TechnicalSheet> builder)
        {
            builder.HasOne(oi => oi.Product).WithOne(oi => oi.TechnicalSheet).HasForeignKey<TechnicalSheet>(k => k.ProductId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}