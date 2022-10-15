using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SZHPCMS.Utilities;

namespace SZHPCMS.Models
{
    public class MenuViewModel
    {
        public long Id { get; set; }

        [GlobalDisplayNameAttribute("lblNameEnglish")]
        [Required(ErrorMessage = "This field is required")]
        public string TitleEnglish { get; set; }

       [GlobalDisplayNameAttribute("lblNameArabic")]
        [Required(ErrorMessage = "This field is required")]
        public string TitleArabic { get; set; }

        [DisplayFormat(DataFormatString="{0:MM/dd/yyyy}")]
        [GlobalDisplayNameAttribute("lblCreatedDate")]
        public DateTime CreatedDate { get; set; }

        [Display(Name="Status")]
        public string RowStatus { get; set; }     
        public string CreatedBy { get; set; }
    }
}