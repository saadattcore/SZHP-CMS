using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContract.Implementation
{
    public class ContactChairmanModel
    {
        public long ContactChairmanId { get; set; }
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public string EmailSubject { get; set; }
        public string EmailBody { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<long> RowStatusId { get; set; }
        public string RowStatus { get; set; }
    }
}
