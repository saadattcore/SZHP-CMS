using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SZHPCMS.Utilities;

namespace SZHPCMS.Models
{
    public class RulesAndRegulationViewModel
    {
        public long RuleId { get; set; }

        [GlobalDisplayNameAttribute("lblTitleEn")]
        [Required(ErrorMessage = "This field is required")]
        public string TitleEn { get; set; }

        [GlobalDisplayNameAttribute("lblTitleAr")]
        [Required(ErrorMessage = "This field is required")]
        public string TitleAr { get; set; }

        [GlobalDisplayNameAttribute("lblYear")]
        //[Required(ErrorMessage = "This field is required")]
        public Nullable<int> Year { get; set; }

        [GlobalDisplayNameAttribute("lblCreatedDate")]
        public Nullable<System.DateTime> CreatedDate { get; set; }

        [GlobalDisplayNameAttribute("lblRowStatus")]
        public string RowStatus { get; set; }
        public string DocumentName { get; set; }
    }

    public class ConditionViewModel
    {
        public long ConditionId { get; set; }

        [GlobalDisplayNameAttribute("lblTitleEn")]
        [Required(ErrorMessage = "This field is required")]
        public string TitleEn { get; set; }

        [GlobalDisplayNameAttribute("lblTitleAr")]
        [Required(ErrorMessage = "This field is required")]
        public string TitleAr { get; set; }

        [GlobalDisplayNameAttribute("lblDescriptionEn")]
        [Required(ErrorMessage = "This field is required")]
        public string DescriptionEn { get; set; }

        [GlobalDisplayNameAttribute("lblDescriptionAr")]
        [Required(ErrorMessage = "This field is required")]
        public string DescriptionAr { get; set; }

        [GlobalDisplayNameAttribute("lblCreatedDate")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public Nullable<System.DateTime> CreatedDate { get; set; }

        [GlobalDisplayNameAttribute("lblRowStatus")]
        public string RowStatus { get; set; }

    }
}