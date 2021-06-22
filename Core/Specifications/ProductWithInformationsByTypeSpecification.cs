using Core.Entities.Products;

namespace Core.Specifications
{
    public class ProductWithInformationsByTypeSpecification : BaseSpecification<Product>
    {
        public ProductWithInformationsByTypeSpecification(int id)
			: base(x => x.ProductTypeId == id)
		{
			AddInclude(x => x.ProductCategory);
			AddInclude(x => x.TechnicalSheet);
			AddInclude(x => x.ProductType);
			AddInclude(x => x.WaitingList);
			AddInclude("Reviews.AppUser");
			AddInclude("TechnicalSheet.ProductAddtionalInfos");
			AddInclude("TechnicalSheet.ProductAddtionalInfos.AdditionalInfoName");
			AddInclude("TechnicalSheet.ProductAddtionalInfos.AdditionalInfoName.AdditionalInfoCategory");
		}
    }
}