using System.ComponentModel.DataAnnotations;

namespace API.Dtos.Product
{
    public class productToUpdateDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string PictureUrl { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public string ProductCategory { get; set; }
        [Required]
        public string ProductType { get; set; }
        [Required]
        public TechnicalSheetDto TechnicalSheet { get; set; }
    }
}