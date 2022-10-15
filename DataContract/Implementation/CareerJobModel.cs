using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContract.Implementation
{
    public class CareerJobModel
    {
        public long CareerJobId { get; set; }
        public Nullable<long> CareerIndustryId { get; set; }
        public string CareerIndustry { get; set; }
        public Nullable<long> CareerTypeId { get; set; }
        public string CareerType { get; set; }
        public string TitleAr { get; set; }
        public string DescriptionAr { get; set; }
        public string LocationAr { get; set; }
        public string SalaryAr { get; set; }
        public string SalaryCurrencyAr { get; set; }
        public string MinumumEductionAr { get; set; }
        public string MinumumExperienceAr { get; set; }
        public string DesiredSkillsAr { get; set; }
        public string BenefitsAr { get; set; }
        public string ResponsibilitiesAr { get; set; }
        public string ShiftAr { get; set; }
        public string TitleEn { get; set; }
        public string DescriptionEn { get; set; }
        public string LocationEn { get; set; }
        public string SalaryEn { get; set; }
        public string SalaryCurrencyEn { get; set; }
        public string MinumumEductionEn { get; set; }
        public string MinumumExperienceEn { get; set; }
        public string DesiredSkillsEn { get; set; }
        public string BenefitsEn { get; set; }
        public string ResponsibilitiesEn { get; set; }
        public string ShiftEn { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<long> RowStatusId { get; set; }
        public string RowStatus { get; set; }
        public List<DocumentModel> Documents { get; set; }
    }
}
