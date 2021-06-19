using System;
using System.Collections.Generic;

namespace Core.Entities.Products
{
    public class TechnicalSheet : BaseEntity
    {
        public IReadOnlyList<ProductAdditionalInfo> ProductAddtionalInfos { get; set; }
        public DateTimeOffset LastUpdateDate { get; set; }
        public DateTimeOffset ReferenceDate { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
    }
}