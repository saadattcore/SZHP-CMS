using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContract.Implementation
{
    public class EventModel
    {
        public long Id { get; set; }
        public long EventTypeID { get; set; }
        public string EventTypeName { get; set; }
        public string TitleArabic { get; set; }
        public string DescriptionAr { get; set; }
        public string TitleEnglish { get; set; }
        public string DescriptionEn { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string LocationAr { get; set; }
        public string LocationEn { get; set; }
        public long CreatedBy { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public long RowStatusId { get; set; }
        public string RowStatus { get; set; }
        public List<DocumentModel> Documents { get; set; }
        public string FileName { get; set; }

    }
}
