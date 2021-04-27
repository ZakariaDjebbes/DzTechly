namespace Core.Entities.Product
{
    public class ProductAdditionalInfo : BaseEntity
    {
        public int AdditionalinfoCategoryId { get; set; }
        public AdditionalInfoCategory AdditionalInfoCategory { get; set; }
        public int AdditionalInfoNameId { get; set; }
        public AdditionalInfoName AdditionalInfoName { get; set; }
        public string AdditionalInfoValue { get; set; }
    }
}