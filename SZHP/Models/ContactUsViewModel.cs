using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SZHPCMS.Utilities;


namespace SZHPCMS.Models
{
    public class ContactUsViewModel
    {
        public long ContactUsId { get; set; }

        [GlobalDisplayNameAttribute("lblFullName")]
        public string FullName { get; set; }

        [GlobalDisplayNameAttribute("lblMobilePhoneNumber")]
        public string MobilePhoneNumber { get; set; }

        [GlobalDisplayNameAttribute("lblEmail")]
        public string Email { get; set; }

        [GlobalDisplayNameAttribute("lblSubject")]
        public string Subject { get; set; }

        [GlobalDisplayNameAttribute("lblInquiry")]
        public string Inquiry { get; set; }

        [GlobalDisplayNameAttribute("lblCreatedDate")]
        [DisplayFormat(DataFormatString="{0:MM/dd/yyyy}")]
        public Nullable<System.DateTime> CreatedDate { get; set; }

        [GlobalDisplayNameAttribute("lblRowStatus")]
        public string RowStatus { get; set; }
    }


}