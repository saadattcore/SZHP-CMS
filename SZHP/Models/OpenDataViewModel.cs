using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SZHPCMS.Utilities;

namespace SZHPCMS.Models
{
    public class OpenDataViewModel
    {
        public long OpenDataId { get; set; }

        [GlobalDisplayNameAttribute("lblTitleEn")]
        [Required(ErrorMessage = "This field is required")]
        public string TitleEn { get; set; }

        [GlobalDisplayNameAttribute("lblTitleAr")]
        [Required(ErrorMessage = "This field is required")]
        public string TitleAr { get; set; }

        [GlobalDisplayNameAttribute("lblPublicationData")]
        [Required(ErrorMessage = "This field is required")]
        [DisplayFormat(DataFormatString="{0:MM/dd/yyyy}")]
        public Nullable<System.DateTime> PublicationDate { get; set; }
        public string ExcelDocName { get; set; }

        [GlobalDisplayNameAttribute("lblExcelDoc")]
        //[Required(ErrorMessage = "Excel file is required")]
        public HttpPostedFileBase ExcelDoc { get; set; }
        public string PDFDocName { get; set; }

        [GlobalDisplayNameAttribute("lblPDFDoc")]
        //[Required(ErrorMessage = "PDF File is required")]
        public HttpPostedFileBase PDFDoc { get; set; }

        [GlobalDisplayNameAttribute("lblCreatedDate")]
        public Nullable<System.DateTime> CreatedDate { get; set; }

        [GlobalDisplayNameAttribute("lblRowStatus")]
        public string RowStatus { get; set; }

    }
}