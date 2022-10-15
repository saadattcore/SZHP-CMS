using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContract.Implementation
{
    public class RulesAndRegulationModel
    {
        public long RuleId { get; set; }
        public string TitleEn { get; set; }
        public string TitleAr { get; set; }
        public string DocumentName { get; set; }
        public Nullable<int> Year { get; set; }   
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public string RowStatus { get; set; }
        public DocumentModel Document { get; set; }
    }

    public class ConditionModel
    {
        public long ConditionId { get; set; }
        public string TitleEn { get; set; }
        public string TitleAr { get; set; }
        public string DescriptionEn { get; set; }
        public string DescriptionAr { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string RowStatus { get; set; }
    }
}
