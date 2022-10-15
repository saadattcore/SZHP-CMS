using DataContract.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SZHPCMS.Utilities;
using System.ComponentModel.DataAnnotations;

namespace SZHPCMS.Models
{
    public class BannerViewModel
    {
        public long BannerId { get; set; }

        [GlobalDisplayNameAttribute("lblTitleAr")]
        [Required(ErrorMessage = "This field is required")]
        public string TitleAr { get; set; }

        [GlobalDisplayNameAttribute("lblTitleEn")]
        [Required(ErrorMessage = "This field is required")]
        public string TitleEn { get; set; }

        [GlobalDisplayNameAttribute("lblCreatedDate")]
        [DisplayFormat(DataFormatString="{0:MM/dd/yyyy}")]
        public DateTime CreatedDate { get; set; }

        [GlobalDisplayNameAttribute("lblRowStatus")]
        public string RowStatus { get; set; }
        public List<DocumentViewModel> Documents { get; set; }
    }
}