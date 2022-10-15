using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContract.Implementation
{
    public class SubscriberModel
    {
        public long SubscriberId { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string Email { get; set; }
        public Nullable<long> SubscriptionTypeId { get; set; }
        public string SubscriptionType { get; set; }
        public Nullable<long> CreatedBy { get; set; } 
        public Nullable<long> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<long> RowStatusId { get; set; }
        public string RowStatus { get; set; }

    }
}
