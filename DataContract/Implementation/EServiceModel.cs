using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContract.Implementation
{
    public class EServiceModel
    {
        public long EServiceID { get; set; }
        public string TitleEn { get; set; }
        public string TitleAr { get; set; }
        public Nullable<long> DocumentID { get; set; }
        public string VideoURL { get; set; }
        public string DescriptionEn { get; set; }
        public string DescriptionAr { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<long> RowStatusId { get; set; }
        public string RowStatus { get; set; }
        public DocumentModel Document { get; set; }
        public string CategoryName { get; set; }
        public long CategoryID { get; set; }

    }

    public class EServiceCategoryModel
    {
        public long EServiceCategoryId { get; set; }
        public string CategoryNameEn { get; set; }
        public string CategoryNameAr { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string RowStatus { get; set; }
        public List<EServiceModel> EServices { get; set; }

    }

    public class EParticipationModel
    {
        public long EParticipationId { get; set; }
        public string DescriptionEn { get; set; }
        public string DescriptionAr { get; set; }
        public string DocumentTextEn { get; set; }
        public string DocumentTextAr { get; set; }
        public Nullable<long> DocumentId { get; set; }
        public DocumentModel Document { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string RowStatus { get; set; }


    }
}
