// using Core.Entities.Products;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;

// namespace Infrastructure.Data.Config
// {
//     public class AdditionalInfoNameConfiguration : IEntityTypeConfiguration<AdditionalInfoName>
//     {
//         public void Configure(EntityTypeBuilder<AdditionalInfoName> builder)
//         {
//             builder.HasOne(x => x.AdditionalInfoCategory).WithMany().OnDelete(DeleteBehavior.Cascade);
//         }
//     }
// }