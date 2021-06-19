namespace API.Dtos.Product
{
    public class ProductAdditionalInfoDto
    {
        public int Id { get; set; }
        public int AdditionalInfoNameId { get; set; }
        public string AdditionalInfoName { get; set; }
        public string Unit { get; set; }
        public string AdditionalInfoValue { get; set; }
    }
}