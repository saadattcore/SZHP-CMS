using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContract.Implementation
{
    public class CareerIndustryModel
    {
        public long CareerIndustryId { get; set; }
        public string NameAr { get; set; }
        public string DescriptionAr { get; set; }
        public string NameEn { get; set; }
        public string DescriptionEn { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public string RowStatus { get; set; }
    }
}
