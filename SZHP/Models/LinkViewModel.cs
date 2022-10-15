using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SZHPCMS.Utilities;
using System.ComponentModel.DataAnnotations;

namespace SZHPCMS.Models
{
    public class LinkViewModel
    {
        public long LinkID { get; set; }

        [GlobalDisplayNameAttribute("lblTitleAr")]
        [Required(ErrorMessage = "This field is required")]
        public string TitleAr { get; set; }

        [GlobalDisplayNameAttribute("lblTitleEn")]
        [Required(ErrorMessage = "This field is required")]
        public string TitleEn { get; set; }

        [GlobalDisplayNameAttribute("lblURL")]
        [Required(ErrorMessage = "This field is required")]
        public string LinkUrl { get; set; }

        [GlobalDisplayNameAttribute("lblCreatedDate")]
        public Nullable<System.DateTime> CreatedDate { get; set; }

        [GlobalDisplayNameAttribute("lblRowStatus")]
        public string RowStatus { get; set; }
        public List<DocumentViewModel> Documents { get; set; }
    }
}