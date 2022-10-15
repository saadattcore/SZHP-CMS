using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SZHPCMS.Utilities;

namespace SZHPCMS.Models
{
    public class SubscriberViewModel
    {
        public long SubscriberId { get; set; }
        [GlobalDisplayNameAttribute("lblNameArabic")]
        public string NameAr { get; set; }

        [GlobalDisplayNameAttribute("lblNameEnglish")]
        public string NameEn { get; set; }

        [GlobalDisplayNameAttribute("lblEmail")]
        public string Email { get; set; }
        public Nullable<long> SubscriptionTypeId { get; set; }

        [GlobalDisplayNameAttribute("lblSubscriptionType")]
        public string SubscriptionType { get; set; }

        [GlobalDisplayNameAttribute("lblRowStatus")]
        public string RowStatus { get; set; }
    }
}