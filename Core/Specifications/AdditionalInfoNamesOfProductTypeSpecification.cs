using Core.Entities.Products;

namespace Core.Specifications
{
    public class AdditionalInfoNamesOfProductTypeSpecification : BaseSpecification<AdditionalInfoName>
    {
        public AdditionalInfoNamesOfProductTypeSpecification(int productTypeId) : base(x => x.ProductTypeId == productTypeId)
        {
            AddInclude(x => x.AdditionalInfoCategory);
        }

        public AdditionalInfoNamesOfProductTypeSpecification(ProductType productType) : base (x=> x.ProductTypeId == productType.Id)
        {
            AddInclude(x => x.AdditionalInfoCategory);
        }
    }
}