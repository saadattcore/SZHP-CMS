using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContract.Implementation
{
    public class ResearchStudyModel
    {
        public long ResearchStudyId { get; set; }
        public string TitleEn { get; set; }
        public string TitleAr { get; set; }
        public string DescriptionEn { get; set; }
        public string DescriptionAr { get; set; }
        public string ThemeEn { get; set; }
        public string ThemeAr { get; set; }
        public Nullable<long> ResearchStudyCategoryId { get; set; }
        public string CategoryName { get; set; }
        public Nullable<long> DocumentId { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }      
        public Nullable<long> UpdatedBy { get; set; }
        public Nullable<long> RowStatusId { get; set; }
        public string RowStatus { get; set; }
    }
}
