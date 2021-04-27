namespace Core.Entities.Product
{
    public class AdditionalInfoName : BaseEntity
    {
        public string Name { get; set; }
        public string Unit { get; set; }
        public int ProductTypeId { get; set; }
        public ProductType ProductType { get; set; }
    }
}