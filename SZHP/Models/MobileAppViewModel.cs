using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SZHPCMS.Utilities;

namespace SZHPCMS.Models
{
    public class MobileAppViewModel
    {
        public long MobileApplicationId { get; set; }

        [GlobalDisplayNameAttribute("lblNameEnglish")]
        [Required(ErrorMessage = "This field is required")]
        public string NameEn { get; set; }

        [GlobalDisplayNameAttribute("lblNameArabic")]
        [Required(ErrorMessage = "This field is required")]
        public string NameAr { get; set; }
        public string DescriptionEn { get; set; }
        public string DescriptionAr { get; set; }
        public Nullable<long> DocumentId { get; set; }
        public HttpPostedFileBase Document { get; set; }
        public string DocumentName { get; set; }

        [Url(ErrorMessage = "Invalid url")]
        public string AppStoreURL { get; set; }
        public Nullable<long> IPhoneQRCodeDocId { get; set; }
        public HttpPostedFileBase IPhoneQRCodeDoc { get; set; }
        public string IPhoneQRCodeDocName { get; set; }

        [Url(ErrorMessage = "Invalid url")]
        public string PlayStoreURL { get; set; }
        public Nullable<long> AndroidQRCodeDocId { get; set; }
        public HttpPostedFileBase AndroidQRCodeDoc { get; set; }
        public string AndroidQRCodeDocName { get; set; }

        [Url(ErrorMessage = "Invalid url")]
        public string BlackBerryWorldURL { get; set; }
        public Nullable<long> BlackBerryQRCodDocId { get; set; }
        public HttpPostedFileBase BlackBerryQRCodDoc { get; set; }
        public string BlackBerryQRCodDocName { get; set; }

        [Url(ErrorMessage = "Invalid url")]
        public string WinStoreURL { get; set; }
        public Nullable<long> WinQRCodeDocId { get; set; }
        public HttpPostedFileBase WinQRCodeDoc { get; set; }
        public string WinQRCodeDocName { get; set; }

        [GlobalDisplayNameAttribute("lblCreatedDate")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public Nullable<System.DateTime> CreatedDate { get; set; }

        [GlobalDisplayNameAttribute("lblRowStatus")]
        public string RowStatus { get; set; }
    }
}