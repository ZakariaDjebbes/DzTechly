using System.Linq;
using System.Collections.Generic;
using Core.Entities.Identity;

namespace Core.Entities.Product
{
    public class Product : BaseEntity
    {
        public Product()
        {
        }

        public Product(IReadOnlyList<AppUser> waitingList, IReadOnlyList<Review> reviews, string name, string description, decimal price, string pictureUrl, ProductType productType, int productTypeId, ProductCategory productCategory, int productCategoryId, TechnicalSheet technicalSheet, int technicalSheetId, int quantity)
        {
            Name = name;
            Description = description;
            Price = price;
            PictureUrl = pictureUrl;
            ProductType = productType;
            ProductTypeId = productTypeId;
            Reviews = reviews;
            ProductCategory = productCategory;
            ProductCategoryId = productCategoryId;
            TechnicalSheet = technicalSheet;
            TechnicalSheetId = technicalSheetId;
            Quantity = quantity;
            WaitingList = waitingList;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public ProductType ProductType { get; set; }
        public int ProductTypeId { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public int ProductCategoryId { get; set; }
        public TechnicalSheet TechnicalSheet { get; set; }
        public int TechnicalSheetId { get; set; }
        public int Quantity { get; set; }
        public IReadOnlyList<Review> Reviews { get; set; }
        public IReadOnlyList<AppUser> WaitingList { get; set; }

		public int GetReviewsAverage()
		{
            if(Reviews != null && Reviews.Count > 0)
			    return (int)Reviews.Select(x => x.Stars).DefaultIfEmpty(0).Average();
            else
                return -1;
		}

        public int GetReviewsNumber() => Reviews.Count;

        public int GetWaitingCustomersCount() => WaitingList.Count;

        public bool IsInStock() => Quantity != 0;
    }
}
