using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SZHPCMS.Utilities;

namespace SZHPCMS.Models
{
    public class CareerIndustryViewModel
    {
        public long CareerIndustryId { get; set; }

        [GlobalDisplayNameAttribute("lblNameArabic")]
        [Required(ErrorMessage="This field is required")]
        public string NameAr { get; set; }
        public string DescriptionAr { get; set; }

        [GlobalDisplayNameAttribute("lblNameEnglish")]
        [Required(ErrorMessage = "This field is required")]
        public string NameEn { get; set; }
        public string DescriptionEn { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }

        [GlobalDisplayNameAttribute("lblRowStatus")]
        public string RowStatus { get; set; }
    }
}