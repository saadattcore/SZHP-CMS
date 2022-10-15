using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;
using SZHPCMS.Utilities;

namespace SZHPCMS.Models
{
    public class NewsViewModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Title En")]
        public string TitleEnglish { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Title Ar")]
        public string TitleArabic { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Description En")]
        public string DescriptionEnglish { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Description Ar")]
        public string DescriptionArabic { get; set; }

        //[Display(Name = "News Category")]
        //[Required(ErrorMessage = "News Category is required")]
        //public long CategoryID { get; set; }

        [Display(Name = "Category")]
        public string CategoryName { get; set; }

        [DisplayFormat(DataFormatString="{0:MM/dd/yyyy}")]
        [GlobalDisplayNameAttribute("lblCreatedDate")]
        public DateTime? CreatedDate { get; set; }
        public List<System.Web.Mvc.SelectListItem> Categories { get; set; }

        [Display(Name = "Upload Image")]
        public HttpPostedFileBase ImageFile { get; set; }

        [Display(Name = "Status")]
        public string RowStatus { get; set; }
        public List<DocumentViewModel> Documents { get; set; }

    }

    public class NewsCategoryViewModel
    {
        public long CategoryID { get; set; }
        public string NameEnglish { get; set; }
        public string NameArabic { get; set; }
        public long? CreatedBy { get; set; }
        public System.DateTime? CreatedDate { get; set; }
        public long? UpdatedBy { get; set; }
        public System.DateTime? UpdatedDate { get; set; }
        public long? RowStatusId { get; set; }
    }
}