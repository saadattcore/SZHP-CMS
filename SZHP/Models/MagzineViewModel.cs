using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SZHPCMS.Utilities;

namespace SZHPCMS.Models
{
    public class MagzineViewModel
    {
        public long MagzineId { get; set; }

        [GlobalDisplayNameAttribute("lblTitleEn")]
        [Required(ErrorMessage = "This field is required")]
        public string TitleEn { get; set; }

        [GlobalDisplayNameAttribute("lblTitleAr")]
        [Required(ErrorMessage = "This field is required")]
        public string TitleAr { get; set; }

        [GlobalDisplayNameAttribute("lblDate")]
        [DisplayFormat(DataFormatString="{0:MM/dd/yyyy}")]
        public Nullable<System.DateTime> Date { get; set; }

        public string FileDocName { get; set; }

        [GlobalDisplayNameAttribute("lblFileDocument")]
        public HttpPostedFileBase FileDoc { get; set; }
        public string CoverName { get; set; }

        [GlobalDisplayNameAttribute("lblCoverDoc")]
        public HttpPostedFileBase CoverDoc { get; set; }

        [GlobalDisplayNameAttribute("lblCreatedDate")]
        public Nullable<System.DateTime> CreatedDate { get; set; }

        [GlobalDisplayNameAttribute("lblRowStatus")]
        public string RowStatus { get; set; }
    }
}