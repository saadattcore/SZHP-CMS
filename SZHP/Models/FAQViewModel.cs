using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SZHPCMS.Utilities;

namespace SZHPCMS.Models
{
    public class FAQViewModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Question En")]
        public string QuestionEnglish { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Question Ar")]
        public string QuestionArabic { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Answer En")]
        public string AnswerEnglish { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Answer Ar")]
        public string AnswerArabic { get; set; }

        [Display(Name = "FAQ Category")]
        //[Required(ErrorMessage = "This field is required")]
        public long? CategoryID { get; set; }

        [Display(Name = "Category")]
        public string CategoryName { get; set; }
        public long CreatedBy { get; set; }

        [Display(Name = "Status")]
        public string RowStatus { get; set; }
        public long? RowStatusID { get; set; }

        [GlobalDisplayNameAttribute("lblCreatedDate")]
        [DisplayFormat(DataFormatString="{0:MM/dd/yyyy}")]
        public DateTime? CreatedDate { get; set; }
        public List<System.Web.Mvc.SelectListItem> Categories { get; set; }

    }
}