using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SZHPCMS.Utilities;

namespace SZHPCMS.Models
{
    public class TechnicalSupportViewModel
    {
        public long TechnicalSupportId { get; set; }

        [GlobalDisplayNameAttribute("lblRefNo")]
        public string ReferenceNo { get; set; }

        [GlobalDisplayNameAttribute("lblPersonName")]
        public string PersonName { get; set; }

        [GlobalDisplayNameAttribute("lblContactNo")]
        public string PersonMobileNo { get; set; }

        [GlobalDisplayNameAttribute("lblPersonEmail")]
        public string PersonEmailId { get; set; }

        [GlobalDisplayNameAttribute("lblUserType")]
        public string UserType { get; set; }

        [GlobalDisplayNameAttribute("lblSupportCategory")]
        public string Category { get; set; }

        [GlobalDisplayNameAttribute("lblSummary")]
        public string Summary { get; set; }

        [GlobalDisplayNameAttribute("lblDescription")]
        public string Description { get; set; }
        public string DocumentName { get; set; }

        [GlobalDisplayNameAttribute("lblCreatedDate")]
        [DisplayFormat(DataFormatString="{0:MM/dd/yyyy}")]
        public Nullable<System.DateTime> CreatedDate { get; set; }
    }
}