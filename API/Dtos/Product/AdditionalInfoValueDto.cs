namespace API.Dtos.Product
{
    public class AdditionalInfoValueDto
    {
        public int? NameId { get; set; }
        public string Value { get; set; }
        public string Unit { get; set; }
        public string Name { get; set; }
        public int? CategoryId { get; set; }
        public string Category { get; set; }

        public AdditionalInfoValueDto()
        {
            
        }
    }
}