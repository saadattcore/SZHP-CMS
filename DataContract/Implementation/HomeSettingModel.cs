using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContract.Implementation
{
    public class HomeSettingModel
    {
        public long HomeSettingId { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Youtube { get; set; }
        public string Instagram { get; set; }
        public DocumentModel WordDocument { get; set; }
        public DocumentModel ExcelDocument { get; set; }
        public DocumentModel PDFDocument { get; set; }
        public DocumentModel BannerDocument { get; set; }
        public Nullable<long> CreatedBy { get; set; }       
        public Nullable<long> Updated_By { get; set; }
        public Nullable<System.DateTime> Updated_Date { get; set; }
        public Nullable<long> RowStatusId { get; set; }
        public string RowStatus { get; set; }
    }
}
