using Core.Entities.Product;

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