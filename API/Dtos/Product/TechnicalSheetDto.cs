using System;
using System.Collections.Generic;

namespace API.Dtos.Product
{
    public class TechnicalSheetDto
    {
        public int Id { get; set; }
        public IDictionary<string, IReadOnlyList<ProductAdditionalInfoDto>> ProductAddtionalInfos { get; set; }
        public DateTimeOffset LastUpdateDate { get; set; }
        public DateTimeOffset ReferenceDate { get; set; }
    }
}