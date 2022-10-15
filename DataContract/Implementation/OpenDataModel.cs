using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContract.Implementation
{
    public class OpenDataModel
    {
        public long OpenDataId { get; set; }
        public string TitleEn { get; set; }
        public string TitleAr { get; set; }
        public Nullable<System.DateTime> PublicationDate { get; set; }
        public DocumentModel ExcelDoc { get; set; }
        public DocumentModel PDFDoc { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public string RowStatus { get; set; }
    }
}
