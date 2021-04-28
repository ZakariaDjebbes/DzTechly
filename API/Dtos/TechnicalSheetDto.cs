using System;
using System.Collections.Generic;

namespace API.Dtos
{
    public class TechnicalSheetDto
    {
        public IDictionary<string, IReadOnlyList<ProductAdditionalInfoDto>> ProductAddtionalInfos { get; set; }
        public DateTimeOffset LastUpdateDate { get; set; }
        public DateTimeOffset ReferenceDate { get; set; }
    }
}