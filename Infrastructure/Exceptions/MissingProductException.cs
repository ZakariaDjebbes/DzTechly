using System;
namespace Infrastructure.Exceptions
{
    public class MissingProductException : CustomException
    {
        public int ProductId { get; set; }

        public MissingProductException(string message, int productId) : base(message)
        {
            ProductId = productId;
        }
        public MissingProductException(string message, Exception innerException, int productId) : base(message, innerException)
        {
            ProductId = productId;
        }
    }
}