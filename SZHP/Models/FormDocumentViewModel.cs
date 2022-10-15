using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SZHPCMS.Utilities;

namespace SZHPCMS.Models
{
    public class FormDocumentViewModel
    {
        public long FormDocumentId { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [GlobalDisplayNameAttribute("lblNameEnglish")]
        public string NameEn { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [GlobalDisplayNameAttribute("lblNameArabic")]
        public string NameAr { get; set; }
        public string DocumentName { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<long> UpdatedBy { get; set; }

        [GlobalDisplayNameAttribute("lblRowStatus")]
        public string RowStatus { get; set; }

        //[Required(ErrorMessage = "This field is required")]
        public HttpPostedFileBase File { get; set; }
    }
}