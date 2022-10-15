using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContract.Implementation
{
    public class TechnicalSupportModel
    {
        public long TechnicalSupportId { get; set; }
        public string ReferenceNo { get; set; }
        public string PersonName { get; set; }
        public string PersonMobileNo { get; set; }
        public string PersonEmailId { get; set; }
        public string UserType { get; set; }
        public string Category { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string DocumentName { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    }
}
