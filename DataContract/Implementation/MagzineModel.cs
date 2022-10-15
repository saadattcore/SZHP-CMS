using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContract.Implementation
{
    public class MagzineModel
    {
        public long MagzineId { get; set; }
        public string TitleEn { get; set; }
        public string TitleAr { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public DocumentModel FileDoc { get; set; }
        public DocumentModel CoverDoc { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string RowStatus { get; set; }
    }
}
