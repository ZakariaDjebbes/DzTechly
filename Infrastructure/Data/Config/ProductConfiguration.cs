using Core.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
	public class ProductConfiguration : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder.Property(p => p.Id).IsRequired();
			builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
			builder.Property(p => p.Description).IsRequired().HasMaxLength(180);
			builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
			builder.Property(p => p.PictureUrl).IsRequired();
			builder.HasOne(t => t.ProductType).WithMany().HasForeignKey(p => p.ProductTypeId);
			builder.HasOne(t => t.ProductCategory).WithMany().HasForeignKey(p => p.ProductCategoryId);
			builder.HasMany(o => o.Reviews).WithOne().OnDelete(DeleteBehavior.Cascade);
			builder.HasOne(o => o.TechnicalSheet).WithOne().OnDelete(DeleteBehavior.Cascade);
		} 
	}
}
