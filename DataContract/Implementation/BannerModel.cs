using DataContract.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContract.Implementation
{
    public class BannerModel 
    {
        public long BannerId { get; set; }
        public string TitleAr { get; set; }
        public string TitleEn { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<long> RowStatusId { get; set; }
        public string RowStatus { get; set; }
        public List<DocumentModel> Documents { get; set; }
        
    }
}
