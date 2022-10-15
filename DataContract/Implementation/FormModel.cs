using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContract.Implementation
{
    public class FormModel
    {
        public long FormId { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Nullable<long> CreatedBy { get; set; }        
        public Nullable<long> UpdatedBy { get; set; }
        public string RowStatus { get; set; }
        public List<FormDocumentModel> Documents { get; set; }
        
    }
}
