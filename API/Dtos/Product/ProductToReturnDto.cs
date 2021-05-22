using System.Collections.Generic;
using Core.Entities.Product;

namespace API.Dtos.Product
{
    public class ProductToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public string ProductType { get; set; }
        public string ProductCategory { get; set; }
        public int Quantity { get; set; }
        public int WaitingCustomersCount { get; set; }
        public bool IsInStock { get; set; }
        public int ReviewsAverage { get; set; }
        public int ReviewsNumber { get; set; }
        public TechnicalSheetDto TechnicalSheet { get; set; }
        public IReadOnlyList<UserForWaitingDto> WaitingCustomers { get; set; }
    }
}