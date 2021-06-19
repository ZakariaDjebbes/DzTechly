using Core.Entities.Products;

namespace Core.Specifications
{
    public class ProductTypeOfCategorySpecification : BaseSpecification<ProductType>
    {
        public ProductTypeOfCategorySpecification(int categoryId): base(x => x.ProductCategoryId == categoryId)
        {
        }
    }
}