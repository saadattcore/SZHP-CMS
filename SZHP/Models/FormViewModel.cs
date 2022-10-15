using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SZHPCMS.Utilities;

namespace SZHPCMS.Models
{
    public class FormViewModel
    {
        public long FormId { get; set; }

        [GlobalDisplayNameAttribute("lblNameEnglish")]
        [Required(ErrorMessage = "This field is required")]
        public string NameEn { get; set; }

        [GlobalDisplayNameAttribute("lblNameArabic")]
        [Required(ErrorMessage = "This field is required")]
        public string NameAr { get; set; }

        [GlobalDisplayNameAttribute("lblCreatedDate")]
        [DisplayFormat(DataFormatString="{0:MM/dd/yyyy}")]
        public DateTime? CreatedDate { get; set; }
        public List<FormDocumentViewModel> Documents { get; set; }

        [GlobalDisplayNameAttribute("lblRowStatus")]
        public string RowStatus { get; set; }
    }
}