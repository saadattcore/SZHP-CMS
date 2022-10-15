using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SZHPCMS.Utilities;

namespace SZHPCMS.Models
{
    public class SiteMapViewModel
    {
        public long SiteMapId { get; set; }

        [GlobalDisplayNameAttribute("lblNameEnglish")]
        [Required(ErrorMessage = "This field is required")]
        public string NameEn { get; set; }

        [GlobalDisplayNameAttribute("lblNameArabic")]
        [Required(ErrorMessage = "This field is required")]
        public string NameAr { get; set; }

        [GlobalDisplayNameAttribute("lblURL")]
        //[Required(ErrorMessage = "Title english is required")]
        public string URL { get; set; }
        public Nullable<long> ParentId { get; set; }

        [GlobalDisplayNameAttribute("lblParentName")]
        public string ParentName { get; set; }

        [GlobalDisplayNameAttribute("lblCreatedDate")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public Nullable<System.DateTime> CreatedDate { get; set; }

        [GlobalDisplayNameAttribute("lblRowStatus")]
        public string RowStatus { get; set; }
        public List<System.Web.Mvc.SelectListItem> SiteMaps { get; set; }
    }

    public class HelpViewModel
    {
        public long HelpId { get; set; }

        [GlobalDisplayNameAttribute("lblTitleEn")]
        [Required(ErrorMessage = "This field is required")]
        public string TitleEn { get; set; }

        [GlobalDisplayNameAttribute("lblTitleAr")]
        [Required(ErrorMessage = "This field is required")]
        public string TitleAr { get; set; }

        [GlobalDisplayNameAttribute("lblDescriptionEn")]
        [Required(ErrorMessage = "This field is required")]
        public string DescriptionEn { get; set; }

        [GlobalDisplayNameAttribute("lblDescriptionAr")]
        [Required(ErrorMessage = "This field is required")]
        public string DescriptionAr { get; set; }

        [GlobalDisplayNameAttribute("lblCreatedDate")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public Nullable<System.DateTime> CreatedDate { get; set; }

        [GlobalDisplayNameAttribute("lblRowStatus")]
        public string RowStatus { get; set; }

    }


}