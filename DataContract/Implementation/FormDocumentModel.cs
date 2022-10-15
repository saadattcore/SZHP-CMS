using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContract.Implementation
{
    public class FormDocumentModel
    {
        public long FormDocumentId { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string DocumentName { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public string RowStatus { get; set; }
        public DocumentModel Document { get; set; }
        public long FormID { get; set; }

    }
}
