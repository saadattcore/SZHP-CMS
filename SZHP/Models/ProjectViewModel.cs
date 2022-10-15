using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SZHPCMS.Utilities;

namespace SZHPCMS.Models
{
    public class ProjectViewModel
    {
        public long ProjectId { get; set; }

        [GlobalDisplayNameAttribute("lblTitleEn")]
        [Required(ErrorMessage = "This field is required")]
        public string TitleEn { get; set; }

        [GlobalDisplayNameAttribute("lblTitleAr")]
        [Required(ErrorMessage = "This field is required")]
        public string TitleAr { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [GlobalDisplayNameAttribute("lblDescriptionEn")]
        public string DescriptionEn { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [GlobalDisplayNameAttribute("lblDescriptionAr")]
        public string DescriptionAr { get; set; }
        public Nullable<long> DocumentId { get; set; }
        public string DocumentName { get; set; }

        [GlobalDisplayNameAttribute("lblCreatedDate")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public Nullable<System.DateTime> CreatedDate { get; set; }

        [GlobalDisplayNameAttribute("lblRowStatus")]
        public string RowStatus { get; set; }
    }
}