using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SZHPCMS.Utilities;
using System.ComponentModel.DataAnnotations;

namespace SZHPCMS.Models
{
    public class CareerJobViewModel
    {
        public long CareerJobId { get; set; }


        [Required(ErrorMessage = "This field is required")]
        public Nullable<long> CareerIndustryId { get; set; }

        [GlobalDisplayNameAttribute("lblCareerIndustry")]
        public string CareerIndustry { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public Nullable<long> CareerTypeId { get; set; }

        [GlobalDisplayNameAttribute("lblCareerType")]
        public string CareerType { get; set; }

        [GlobalDisplayNameAttribute("lblTitleAr")]
        [Required(ErrorMessage = "This field is required")]
        public string TitleAr { get; set; }

        [GlobalDisplayNameAttribute("lblDescriptionAr")]
        public string DescriptionAr { get; set; }

        [GlobalDisplayNameAttribute("lblLocationAr")]
        [Required(ErrorMessage = "This field is required")]
        public string LocationAr { get; set; }

        [GlobalDisplayNameAttribute("lblSalaryAr")]
        [Required(ErrorMessage = "This field is required")]
        public string SalaryAr { get; set; }

        [GlobalDisplayNameAttribute("lblSalaryCurrencyAr")]
        [Required(ErrorMessage = "This field is required")]
        public string SalaryCurrencyAr { get; set; }

        [GlobalDisplayNameAttribute("lblMinEductaionAr")]
        [Required(ErrorMessage = "This field is required")]
        public string MinumumEductionAr { get; set; }

        [GlobalDisplayNameAttribute("lblMinExperienceAr")]
        [Required(ErrorMessage = "This field is required")]
        public string MinumumExperienceAr { get; set; }

        [GlobalDisplayNameAttribute("lblDesiredSkillAr")]
        public string DesiredSkillsAr { get; set; }

        [GlobalDisplayNameAttribute("lblBenefitsAr")]
        public string BenefitsAr { get; set; }

        [GlobalDisplayNameAttribute("lblResponsibilitiesAr")]
        public string ResponsibilitiesAr { get; set; }

        [GlobalDisplayNameAttribute("lblShiftAr")]
        [Required(ErrorMessage = "This field is required")]
        public string ShiftAr { get; set; }

        [GlobalDisplayNameAttribute("lblTitleEn")]
        [Required(ErrorMessage = "This field is required")]
        public string TitleEn { get; set; }

        [GlobalDisplayNameAttribute("lblDescriptionEn")]
        public string DescriptionEn { get; set; }

        [GlobalDisplayNameAttribute("lblLocationEn")]
        [Required(ErrorMessage = "This field is required")]
        public string LocationEn { get; set; }

        [GlobalDisplayNameAttribute("lblSalaryEn")]
        [Required(ErrorMessage = "This field is required")]
        public string SalaryEn { get; set; }

        [GlobalDisplayNameAttribute("lblSalaryCurrencyEn")]
        [Required(ErrorMessage = "This field is required")]
        public string SalaryCurrencyEn { get; set; }

        [GlobalDisplayNameAttribute("lblMinimumEducationEn")]
        [Required(ErrorMessage = "This field is required")]
        public string MinumumEductionEn { get; set; }

        [GlobalDisplayNameAttribute("lblMinimumExperienceEn")]
        [Required(ErrorMessage = "This field is required")]
        public string MinumumExperienceEn { get; set; }

        [GlobalDisplayNameAttribute("lblDesiredSkillsEn")]
        public string DesiredSkillsEn { get; set; }

        [GlobalDisplayNameAttribute("lblBenefitsEn")]
        public string BenefitsEn { get; set; }

        [GlobalDisplayNameAttribute("lblResponsibilitiesEn")]
        public string ResponsibilitiesEn { get; set; }

        [GlobalDisplayNameAttribute("lblShiftEn")]
        [Required(ErrorMessage = "This field is required")]
        public string ShiftEn { get; set; }

        [GlobalDisplayNameAttribute("lblCreatedDate")]
        public Nullable<System.DateTime> CreatedDate { get; set; }

        [GlobalDisplayNameAttribute("lblRowStatus")]
        public string RowStatus { get; set; }

        public List<DocumentViewModel> Documents { get; set; }
        public List<SelectListItem> CareerTypeDDL { get; set; }
        public List<SelectListItem> CareerIndustryDDL { get; set; }
    }
}