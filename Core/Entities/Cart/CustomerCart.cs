using System.Collections.Generic;

namespace Core.Entities.Cart
{
    public class CustomerCart
    {
        public CustomerCart(string id)
        {
            Id = id;
        }

        public CustomerCart()
        {
        }

        public string Id { get; set; }
        public List<CartItem> Items { get; set; } = new List<CartItem>();
        public int? DeliveryMethodId { get; set; }
        public string ClientSecret { get; set; }
        public string PaymentIntentId { get; set; }

    }
}