using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SZHPCMS.Utilities;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SZHPCMS.Models
{
    public class EventViewModel
    {
        public long Id { get; set; }

        [GlobalDisplayName("lblEventType")]
        public long EventTypeID { get; set; }

        [GlobalDisplayName("lblEventType")]
        public string EventTypeName { get; set; }

        [GlobalDisplayName("lblTitleAr")]
        [Required(ErrorMessage = "This field is required")]
        public string TitleArabic { get; set; }

        [GlobalDisplayName("lblDescriptionAr")]
        [Required(ErrorMessage = "This field is required")]
        public string DescriptionAr { get; set; }

        [GlobalDisplayName("lblTitleEn")]
        [Required(ErrorMessage = "This field is required")]
        public string TitleEnglish { get; set; }

        [GlobalDisplayName("lblDescriptionEn")]
        [Required(ErrorMessage = "This field is required")]
        public string DescriptionEn { get; set; }

        [GlobalDisplayName("lblStartDate")]
        [Required(ErrorMessage = "This field is required")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public System.DateTime StartDate { get; set; }

        [GlobalDisplayName("lblEndDate")]
        [Required(ErrorMessage = "This field is required")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public System.DateTime EndDate { get; set; }

        [GlobalDisplayName("lblLocationAr")]
        //[Required(ErrorMessage = "Location arabic is required")]
        public string LocationAr { get; set; }

        [GlobalDisplayName("lblLocationEn")]
        //[Required(ErrorMessage = "Location english is required")]
        public string LocationEn { get; set; }
        public long RowStatusId { get; set; }

        [GlobalDisplayName("lblRowStatus")]
        public string RowStatus { get; set; }
        public IEnumerable<SelectListItem> EventTypes { get; set; }
        public List<DocumentViewModel> Documents { get; set; }
    }
}