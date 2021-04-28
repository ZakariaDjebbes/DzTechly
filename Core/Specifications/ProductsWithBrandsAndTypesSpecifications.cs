using Core.Entities.Product;
using Core.Specifications.SpecificationParams;

namespace Core.Specifications
{
	public class ProductWithBrandAndTypeSpecification : BaseSpecification<Product>
	{
		public ProductWithBrandAndTypeSpecification(ProductSpecificationParams specParams) 
			: base(x => 
			(string.IsNullOrEmpty(specParams.Search) || x.Name.ToLower().Contains(specParams.Search)) &&
			(!specParams.CategoryId.HasValue || x.ProductCategoryId == specParams.CategoryId) &&
			(!specParams.TypeId.HasValue || x.ProductTypeId == specParams.TypeId) &&
			(!specParams.MinPrice.HasValue || x.Price >= specParams.MinPrice) &&
			(!specParams.MaxPrice.HasValue || x.Price <= specParams.MaxPrice)
		)
		{
			AddInclude(x => x.ProductCategory);
			AddInclude(x => x.ProductType);
			AddInclude(x => x.TechnicalSheet);
			AddInclude("Reviews.AppUser");
			AddInclude("TechnicalSheet.ProductAddtionalInfos");
			AddInclude("TechnicalSheet.ProductAddtionalInfos.AdditionalInfoName");
			AddInclude("TechnicalSheet.ProductAddtionalInfos.AdditionalInfoCategory");

			SetOrderBy(x => x.Name);
			ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1),  specParams.PageSize); 

			if (!string.IsNullOrEmpty(specParams.Sort))
			{
				switch (specParams.Sort)
				{
					case "priceAsc":
						SetOrderBy(x => x.Price);
						break;
					case "priceDesc":
						SetOrderByDescending(x => x.Price);
						break;
					default:
						SetOrderBy(x => x.Name);
						break;
				}
			}
		}

		public ProductWithBrandAndTypeSpecification(int id)
			: base(x => x.Id == id)
		{
			AddInclude(x => x.ProductCategory);
			AddInclude(x => x.TechnicalSheet);
			AddInclude(x => x.ProductType);
			AddInclude("Reviews.AppUser");
			AddInclude("TechnicalSheet.ProductAddtionalInfos");
			AddInclude("TechnicalSheet.ProductAddtionalInfos.AdditionalInfoName");
			AddInclude("TechnicalSheet.ProductAddtionalInfos.AdditionalInfoCategory");
		}
	}
}