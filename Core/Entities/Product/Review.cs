using System;
using Core.Entities.Identity;

namespace Core.Entities.Products
{
    public class Review : BaseEntity
    {
        public Review()
        {
        }

        public Review(string appUserId, int productId, string comment, int stars)
        {
            Comment = comment;
            Stars = stars;
            AppUserId = appUserId;
            ProductId = productId;
        }

        public string Comment { get; set; }
        public int Stars { get; set; }
        public DateTimeOffset ReviewDate { get; set; } = DateTimeOffset.Now;
        public DateTimeOffset PurchaseDate { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public int ProductId { get; set; }
    }
}