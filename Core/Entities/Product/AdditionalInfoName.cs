using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities.Products
{
    public class AdditionalInfoName : BaseEntity
    {
        public string Name { get; set; }
        public string Unit { get; set; }
        public int ProductTypeId { get; set; }
        public ProductType ProductType { get; set; }
        public int AdditionalInfoCategoryId { get; set; }
        [ForeignKey("AdditionalInfoCategoryId")]
        public AdditionalInfoCategory AdditionalInfoCategory { get; set; }
    }
}