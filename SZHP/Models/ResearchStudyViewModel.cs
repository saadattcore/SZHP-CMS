using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SZHPCMS.Utilities;
using System.ComponentModel.DataAnnotations;

namespace SZHPCMS.Models
{
    public class ResearchStudyViewModel
    {
        public long ResearchStudyId { get; set; }

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

        [GlobalDisplayNameAttribute("lblThemeEnglish")]
        public string ThemeEn { get; set; }

        [GlobalDisplayNameAttribute("lblThemeArabic")]
        public string ThemeAr { get; set; }

        [GlobalDisplayNameAttribute("lblCategory")]
        [Required(ErrorMessage = "Category is required")]
        public Nullable<long> ResearchStudyCategoryId { get; set; }
        public Nullable<long> DocumentId { get; set; }

        [GlobalDisplayNameAttribute("lblCategory")]
        public string CategoryName { get; set; }

        [GlobalDisplayNameAttribute("lblRowStatus")]
        public string RowStatus { get; set; }

        [GlobalDisplayNameAttribute("lblCreatedDate")]
        [DisplayFormat(DataFormatString="{0:mm/dd/yyyy}")]
        public DateTime? CreatedDate { get; set; }
        public List<SelectListItem> Categories { get; set; }
    }
}