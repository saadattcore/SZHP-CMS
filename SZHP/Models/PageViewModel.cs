using SZHPCMS.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SZHPCMS.Models
{
    public class PageViewModel
    {
        public long Page_Id { get; set; }

        [GlobalDisplayNameAttribute("lblPageNameEn")]
        [Required(ErrorMessage = "This field is required")]
        public string Page_Name_En { get; set; }

        [GlobalDisplayNameAttribute("lblContentEn")]
        //[Required(ErrorMessage = "Content english is required")]
        public string Page_Content_En { get; set; }

        [GlobalDisplayNameAttribute("lblParentPage")]
        public Nullable<long> Parent_Id { get; set; }

        [GlobalDisplayNameAttribute("lblParentPage")]
        public string ParentName { get; set; }

        [GlobalDisplayNameAttribute("lblMetaTitleEn")]
        public string Meta_Title_En { get; set; }

        [GlobalDisplayNameAttribute("lblMetaKeywordsEn")]
        public string Meta_Keywords_En { get; set; }

        [GlobalDisplayNameAttribute("lblMetaDescEn")]
        public string Meta_Description_En { get; set; }

        [GlobalDisplayNameAttribute("lblStandalone")]
        public bool IsStandalone { get; set; }

        [GlobalDisplayNameAttribute("lblPageNameAr")]
        [Required(ErrorMessage = "This field is required")]
        public string Page_Name_Ar { get; set; }

        [GlobalDisplayNameAttribute("lblContentAr")]
        //[Required(ErrorMessage = "Content arabic is required")]
        public string Page_Content_Ar { get; set; }

        [GlobalDisplayNameAttribute("lblMetaTitleAr")]
        public string Meta_Title_Ar { get; set; }

        [GlobalDisplayNameAttribute("lblMetaKeywordsAr")]
        public string Meta_Keywords_Ar { get; set; }

        [GlobalDisplayNameAttribute("lblMetaDescriptionAr")]
        public string Meta_Description_Ar { get; set; }
        public Nullable<long> Created_By { get; set; }
        public Nullable<long> Updated_By { get; set; }
        public Nullable<System.DateTime> Updated_Date { get; set; }

        [GlobalDisplayNameAttribute("lblCreatedDate")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? Created_Date { get; set; }
        public Nullable<long> Row_Status_Id { get; set; }

        [GlobalDisplayNameAttribute("lblRowStatus")]
        public string RowStatus { get; set; }
        public List<System.Web.Mvc.SelectListItem> DropDownPages { get; set; }
        public List<long> PageMenus { get; set; }
        public string Url { get; set; }

    }
}