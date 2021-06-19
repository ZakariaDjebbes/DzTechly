namespace Core.Entities.Products
{
    public class ProductAdditionalInfo : BaseEntity
    {
        public int AdditionalInfoNameId { get; set; }
        public AdditionalInfoName AdditionalInfoName { get; set; }
        public string AdditionalInfoValue { get; set; }
        public int TechnicalSheetId { get; set; }
        public TechnicalSheet TechnicalSheet { get; set; }
    }
}