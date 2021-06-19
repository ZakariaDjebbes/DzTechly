using Core.Entities.Products;

namespace Core.Specifications
{
    public class ProductTypeWithCategorySpecification :  BaseSpecification<ProductType>
    {
        public ProductTypeWithCategorySpecification()
        {
            AddInclude(x => x.ProductCategory);
        }
    }
}