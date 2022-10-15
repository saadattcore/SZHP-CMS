using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SZHPCMS.Utilities;
using System.ComponentModel.DataAnnotations;

namespace SZHPCMS.Models
{
    public class HomeSettingViewModel
    {
        public long HomeSettingId { get; set; }

        [GlobalDisplayNameAttribute("lblFacebook")]
        [Url(ErrorMessage = "Invalid url")]
        public string Facebook { get; set; }

        [GlobalDisplayNameAttribute("lblTwitter")]

        [Url(ErrorMessage = "Invalid url")]
        public string Twitter { get; set; }

        [GlobalDisplayNameAttribute("lblYoutube")]
        [Url(ErrorMessage = "Invalid url")]
        public string Youtube { get; set; }

        [GlobalDisplayNameAttribute("lblInstagram")]
        [Url(ErrorMessage = "Invalid url")]
        public string Instagram { get; set; }

        public string WordDocumentName { get; set; }
        public string ExcelDocumentName { get; set; }
        public string PDFDocumentName { get; set; }
        public string BannerDocumentName { get; set; }

        public Nullable<long> CreatedBy { get; set; }
        public Nullable<long> Updated_By { get; set; }
        public Nullable<System.DateTime> Updated_Date { get; set; }
        public Nullable<long> RowStatusId { get; set; }
        public string RowStatus { get; set; }
        public HttpPostedFileBase WordDocument { get; set; }
        public HttpPostedFileBase ExcelDocument { get; set; }
        public HttpPostedFileBase PDFDocument { get; set; }
        public HttpPostedFileBase BannerDocument { get; set; }

    }
}