using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SZHPCMS.Utilities;
using System.ComponentModel.DataAnnotations;

namespace SZHPCMS.Models
{
    public class CannedEmailViewModel
    {
        public long CannedEmailId { get; set; }

        [GlobalDisplayNameAttribute("lblTitleEn")]
        [Required(ErrorMessage = "This field is required")]
        public string NameEn { get; set; }

        [GlobalDisplayNameAttribute("lblTitleAr")]
        [Required(ErrorMessage = "This field is required")]
        public string NameAr { get; set; }

        [GlobalDisplayNameAttribute("lblEmailFrom")]
        [Required(ErrorMessage = "This field is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string EmailFrom { get; set; }

        [GlobalDisplayNameAttribute("lblEmailTemplate")]
        [Required(ErrorMessage = "This field is required")]
        public string EmailTemplateEn { get; set; }

        [GlobalDisplayNameAttribute("lblEmailTemplateAr")]
        [Required(ErrorMessage = "This field is required")]
        public string EmailTemplateAr { get; set; }

        [GlobalDisplayNameAttribute("lblSubjectEn")]
        [Required(ErrorMessage = "This field is required")]
        public string SubjectEn { get; set; }

        [GlobalDisplayNameAttribute("lblSubjectAr")]
        [Required(ErrorMessage = "This field is required")]
        public string SubjectAr { get; set; }

        [GlobalDisplayNameAttribute("lblCreatedDate")]
        public Nullable<System.DateTime> CreatedDate { get; set; }

        [GlobalDisplayNameAttribute("lblRowStatus")]
        public string RowStatus { get; set; }
        public List<DocumentViewModel> Documents { get; set; }
    }
}