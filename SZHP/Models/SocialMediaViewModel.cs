using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SZHPCMS.Utilities;

namespace SZHPCMS.Models
{
    public class SocialMediaViewModel
    {
        public long Social_Media_Id { get; set; }

        [GlobalDisplayName("lblTitleEn")]
        [Required(ErrorMessage = "This field is required")]
        public string Title_En { get; set; }

        [GlobalDisplayName("lblTitleAr")]
        [Required(ErrorMessage = "This field is required")]
        public string Title_Ar { get; set; }

        [GlobalDisplayName("URL")]
        [Required(ErrorMessage = "This field is required")]
        [Url(ErrorMessage="Invalid url")]
        public string Url { get; set; }
        public string Document_Id { get; set; }

        [GlobalDisplayName("lblToolTipEn")]
        public string Tool_Tip_En { get; set; }

        [GlobalDisplayName("lblToolTipAr")]
        public string Tool_Tip_Ar { get; set; }

        [GlobalDisplayName("lblCreatedDate")]
        [DisplayFormat(DataFormatString="{0:MM/dd/yyyy}")]
        public Nullable<System.DateTime> Created_Date { get; set; }
        public Nullable<System.DateTime> Update_Date { get; set; }
        public Nullable<long> Created_By { get; set; }
        public Nullable<long> Updated_By { get; set; }

        [GlobalDisplayName("lblRowStatus")]
        public string RowStatus { get; set; }
        public DocumentViewModel Document { get; set; }

        //[Required(ErrorMessage = "This field is required")]
        public HttpPostedFileBase ImageFile { get; set; }
    }
}