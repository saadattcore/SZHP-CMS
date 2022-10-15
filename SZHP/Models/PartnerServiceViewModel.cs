using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SZHPCMS.Utilities;

namespace SZHPCMS.Models
{
    public class PartnerServiceViewModel
    {
        public long PartnerServiceId { get; set; }

        [GlobalDisplayNameAttribute("lblNameEnglish")]
        [Required(ErrorMessage = "This field is required")]
        public string NameEn { get; set; }

        [GlobalDisplayNameAttribute("lblNameArabic")]
        [Required(ErrorMessage = "This field is required")]
        public string NameAr { get; set; }

        [GlobalDisplayNameAttribute("lblURL")]
        [Url(ErrorMessage="Invalid url")]
        public string Url { get; set; }

        public Nullable<long> DocumentId { get; set; }
        public string DocumentName { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<long> UpdatedBy { get; set; }

        [GlobalDisplayNameAttribute("lblCreatedDate")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime CreatedDate { get; set; }

        [GlobalDisplayNameAttribute("lblRowStatus")]
        public string RowStatus { get; set; }

    }
}