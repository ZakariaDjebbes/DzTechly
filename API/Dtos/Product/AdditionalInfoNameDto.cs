namespace API.Dtos.Product
{
    public class AdditionalInfoNameDto
    {
        public string Name { get; set; }
        public string Unit { get; set; }
        public string AdditionalInfoCategoryName { get; set; }        
        public int ProductTypeId { get; set; }
        public int AdditionalInfoCategoryId { get; set; }
        public int AdditionalInfoNameId { get; set; }
    }
}