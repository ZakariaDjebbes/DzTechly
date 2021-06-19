using System.Collections.Generic;

namespace Core.Entities.Products
{
    public class AdditionalInfoCategory : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<AdditionalInfoName> InfoNames { get; set; }
    }
}