// using Core.Entities.Products;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;

// namespace Infrastructure.Data.Config
// {
//     public class AdditionalInfoCateogryConfiguration : IEntityTypeConfiguration<AdditionalInfoCategory>
//     {
//         public void Configure(EntityTypeBuilder<AdditionalInfoCategory> builder)
//         {
//             builder.HasMany(c => c.InfoNames).WithOne().HasForeignKey(x => x.AdditionalInfoCategoryId).OnDelete(DeleteBehavior.Cascade);
//         }
//     }
// }