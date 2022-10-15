using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SZHPCMS.Utilities;
using System.ComponentModel.DataAnnotations;

namespace SZHPCMS.Models
{
    public class TrainingPlanViewModel
    {
        public long TrainingPlanId { get; set; }

        [GlobalDisplayNameAttribute("lblTrainingProgram")]
        [Required(ErrorMessage="This field is required")]
        public Nullable<long> TrainingProgramId { get; set; }

        [GlobalDisplayNameAttribute("lblTrainingProgram")]
        public string TrainingProgram { get; set; }

        [GlobalDisplayNameAttribute("lblLocationArabic")]
        [Required(ErrorMessage = "This field is required")]
        public string LocationAr { get; set; }

        [GlobalDisplayNameAttribute("lblThemeArabic")]
        [Required(ErrorMessage = "This field is required")]
        public string ThemeAr { get; set; }

        [GlobalDisplayNameAttribute("lblDate")]
        [Required(ErrorMessage = "This field is required")]
        [DisplayFormat(DataFormatString="{0:MM/dd/yyyy}")]
        public Nullable<System.DateTime> Date { get; set; }

        [GlobalDisplayNameAttribute("lblAudienceArabic")]
        [Required(ErrorMessage = "This field is required")]
        public string AudienceAr { get; set; }

        [GlobalDisplayNameAttribute("lblNotesArabic")]
        [Required(ErrorMessage = "This field is required")]
        public string NotesAr { get; set; }

        [GlobalDisplayNameAttribute("lblLocationEnglish")]
        [Required(ErrorMessage = "This field is required")]
        public string LocationEn { get; set; }

        [GlobalDisplayNameAttribute("lblAudienceEnglish")]
        [Required(ErrorMessage = "This field is required")]
        public string AudienceEn { get; set; }

        [GlobalDisplayNameAttribute("lblNotesEnglish")]
        [Required(ErrorMessage = "This field is required")]
        public string NotesEn { get; set; }

        [GlobalDisplayNameAttribute("lblThemeEnglish")]
        [Required(ErrorMessage = "This field is required")]
        public string ThemeEn { get; set; }

        [GlobalDisplayNameAttribute("lblRowStatus")]
        public string RowStatus { get; set; }

        public IEnumerable<SelectListItem> TrainingPrograms { get; set; }
    }
}