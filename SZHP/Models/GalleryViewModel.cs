using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SZHPCMS.Utilities;
using System.ComponentModel.DataAnnotations;


namespace SZHPCMS.Models
{
    public class GalleryViewModel
    {
        public long GalleryId { get; set; }
        public Nullable<long> Document_Id { get; set; }
        public string ImageName { get; set; }

        [GlobalDisplayNameAttribute("lblTitleEn")]
        [Required(ErrorMessage = "This field is required")]
        public string TitleEn { get; set; }

        [GlobalDisplayNameAttribute("lblTitleAr")]
        [Required(ErrorMessage = "This field is required")]
        public string TitleAr { get; set; }

        [GlobalDisplayNameAttribute("lblRowStatus")]
        public string RowStatus { get; set; }
    }
}