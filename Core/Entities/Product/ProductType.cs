namespace Core.Entities.Products
{
	public class ProductType : BaseEntity
	{
		public int ProductCategoryId { get; set; }
		public ProductCategory ProductCategory { get; set; }
		public string Name { get; set; }
	}
}