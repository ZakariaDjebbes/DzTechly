using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Dtos.Product
{
    public class ProductToCreate
    {
        public int? Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public string PictureUrl { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int ProductTypeId { get; set; }
        [Required]
        public int ProductCategoryId { get; set; }
        public ICollection<AdditionalInfoValueDto> AdditionalInformations { get; set; }
    }
}