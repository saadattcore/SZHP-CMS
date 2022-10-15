using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContract.Implementation
{
    public class SiteMapModel
    {
        public long SiteMapId { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string URL { get; set; }
        public Nullable<long> ParentId { get; set; }
        public string ParentName { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> UpdatedBy { get; set; }      
        public string RowStatus { get; set; }
    }

    public class HelpModel
    {
        public long HelpId { get; set; }
        public string TitleEn { get; set; }
        public string TitleAr { get; set; }
        public string DescriptionEn { get; set; }
        public string DescriptionAr { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> UpdatedBy { get; set; }     
        public string RowStatus { get; set; }
    
    }

}
