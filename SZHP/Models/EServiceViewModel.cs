using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SZHPCMS.Utilities;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SZHPCMS.Models
{
    public class EServiceViewModel
    {
        public long EServiceID { get; set; }

        [GlobalDisplayNameAttribute("lblTitleEn")]
        [Required(ErrorMessage = "This field is required")]
        public string TitleEn { get; set; }

        [GlobalDisplayNameAttribute("lblTitleAr")]
        [Required(ErrorMessage = "This field is required")]
        public string TitleAr { get; set; }
        public Nullable<long> DocumentID { get; set; }

        [GlobalDisplayNameAttribute("lblVideoURL")]
        //[Required(ErrorMessage = "URL is required")]
        public string VideoURL { get; set; }

        [GlobalDisplayNameAttribute("lblDescriptionEn")]
        [Required(ErrorMessage = "This field is required")]
        public string DescriptionEn { get; set; }

        [GlobalDisplayNameAttribute("lblDescriptionAr")]
        [Required(ErrorMessage = "This field is required")]
        public string DescriptionAr { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<long> RowStatusId { get; set; }

        [GlobalDisplayNameAttribute("lblRowStatus")]
        public string RowStatus { get; set; }

        [GlobalDisplayNameAttribute("lblVideo")]
        public HttpPostedFileBase Image { get; set; }
        public string DocumentName { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public long CategoryID { get; set; }
        public List<SelectListItem> CategoryDropDown { get; set; }

        [GlobalDisplayNameAttribute("lblCategoryName")]
        public string CategoryName { get; set; }

    }

    public class EServiceCategoryViewModel
    {
        public long EServiceCategoryId { get; set; }

        [GlobalDisplayNameAttribute("lblNameEnglish")]
        [Required(ErrorMessage = ("This field is required"))]
        public string CategoryNameEn { get; set; }

        [GlobalDisplayNameAttribute("lblNameArabic")]
        [Required(ErrorMessage = ("This field is required"))]
        public string CategoryNameAr { get; set; }

        [GlobalDisplayNameAttribute("lblCreatedDate")]
        public Nullable<System.DateTime> CreatedDate { get; set; }

        [GlobalDisplayNameAttribute("lblRowStatus")]
        public string RowStatus { get; set; }
    }

    public class EParticipationViewModel
    {
        public long EParticipationId { get; set; }

        [GlobalDisplayNameAttribute("lblDescriptionEn")]
        [Required(ErrorMessage = "This field is required")]
        public string DescriptionEn { get; set; }

        [GlobalDisplayNameAttribute("lblDescriptionAr")]
        [Required(ErrorMessage = "This field is required")]
        public string DescriptionAr { get; set; }

        [GlobalDisplayNameAttribute("lblDocumentTextEn")]
        [Required(ErrorMessage = "This field is required")]
        public string DocumentTextEn { get; set; }

        [GlobalDisplayNameAttribute("lblDocumentTextAr")]
        [Required(ErrorMessage = "This field is required")]
        public string DocumentTextAr { get; set; }
        public string DocumentName { get; set; }
        public HttpPostedFileBase Document { get; set; }

        [GlobalDisplayNameAttribute("lblCreatedDate")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public Nullable<System.DateTime> CreatedDate { get; set; }

        [GlobalDisplayNameAttribute("lblRowStatus")]
        public string RowStatus { get; set; }


    }
}