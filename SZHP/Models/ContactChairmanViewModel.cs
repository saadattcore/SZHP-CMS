using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SZHPCMS.Utilities;

namespace SZHPCMS.Models
{
    public class ContactChairmanViewModel
    {
        public long ContactChairmanId { get; set; }

        [GlobalDisplayNameAttribute("lblSenderName")]
        public string SenderName { get; set; }

        [GlobalDisplayNameAttribute("lblSenderEmail")]
        public string SenderEmail { get; set; }

        [GlobalDisplayNameAttribute("lblSubject")]
        public string EmailSubject { get; set; }

        [GlobalDisplayNameAttribute("lblBody")]
        public string EmailBody { get; set; }

        [GlobalDisplayNameAttribute("lblCreatedDate")]
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string RowStatus { get; set; }
    }
}