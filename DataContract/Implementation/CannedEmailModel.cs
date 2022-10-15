using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContract.Implementation
{
    public class CannedEmailModel
    {
        public long CannedEmailId { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string EmailFrom { get; set; }
        public string EmailTemplateEn { get; set; }
        public string EmailTemplateAr { get; set; }
        public string SubjectEn { get; set; }
        public string SubjectAr { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<long> RowStatusId { get; set; }
        public string RowStatus { get; set; }
        public List<DocumentModel> Documents { get; set; }
    }
}
