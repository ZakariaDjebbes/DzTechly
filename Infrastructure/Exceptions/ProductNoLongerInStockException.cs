using System;
namespace Infrastructure.Exceptions
{
    public class ProductNoLongerInStockException : CustomException
    {
        public int ProductId { get; set; }
        public ProductNoLongerInStockException(string message, int productId) : base(message)
        {
            ProductId = productId;
        }

        public ProductNoLongerInStockException(string message, Exception innerException, int productId) : base(message, innerException)
        {
            ProductId = productId;
        }
    }
}