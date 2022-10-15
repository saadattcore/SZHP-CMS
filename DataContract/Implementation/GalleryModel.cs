using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContract.Implementation
{
    public class GalleryModel
    {
        public long GalleryId { get; set; }
        public Nullable<long> DocumentId { get; set; }
        public DocumentModel Document { get; set; }
        public string TitleEn { get; set; }
        public string TitleAr { get; set; }
        public Nullable<long> CreatedBy { get; set; }        
        public Nullable<long> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<long> RowStatusId { get; set; }
        public string RowStatus { get; set; }
    }
}
