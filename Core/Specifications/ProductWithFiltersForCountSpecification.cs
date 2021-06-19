using Core.Entities.Products;
using Core.Specifications.SpecificationParams;

namespace Core.Specifications
{
	public class ProductWithFiltersForCountSpecification : BaseSpecification<Product>
	{
		public ProductWithFiltersForCountSpecification(ProductSpecificationParams specParams)
			: base(x =>
			(string.IsNullOrEmpty(specParams.Search) || x.Name.ToLower().Contains(specParams.Search)) &&
			(!specParams.CategoryId.HasValue || x.ProductCategoryId == specParams.CategoryId) &&
			(!specParams.TypeId.HasValue || x.ProductTypeId == specParams.TypeId) &&
			(!specParams.MinPrice.HasValue || x.Price >= specParams.MinPrice) &&
			(!specParams.MaxPrice.HasValue || x.Price <= specParams.MaxPrice) &&
			((specParams.InStock && x.Quantity > 0) || (!specParams.InStock && x.Quantity >= 0))
			)
		{

		}
	}
}