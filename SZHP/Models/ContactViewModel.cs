using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SZHPCMS.Utilities;

namespace SZHPCMS.Models
{
    public class ContactViewModel
    {
        public long ContactId { get; set; }

        [GlobalDisplayNameAttribute("lblCityAr")]
        [Required(ErrorMessage = "This field is required")]
        public string CityAr { get; set; }

        [GlobalDisplayNameAttribute("lblCityEn")]
        [Required(ErrorMessage = "This field is required")]
        public string CityEn { get; set; }

         [GlobalDisplayNameAttribute("lblLatitude")]
        public Nullable<double> Latitude { get; set; }

         [GlobalDisplayNameAttribute("lblLongitude")]
        public Nullable<double> Longitude { get; set; }

        [GlobalDisplayNameAttribute("lblPhone")]
        [Required(ErrorMessage = "This field is required")]
        public string Phone { get; set; }

        [GlobalDisplayNameAttribute("lblFax")]
        [Required(ErrorMessage = "This field is required")]
        public string Fax { get; set; }

        [GlobalDisplayNameAttribute("lblPOBox")]
        [Required(ErrorMessage = "This field is required")]
        public string POBox { get; set; }

        [GlobalDisplayNameAttribute("lblEmail")]
        [Required(ErrorMessage = "This field is required")]
        public string Email { get; set; }

        [GlobalDisplayNameAttribute("lblWorkingHoursEn")]
        public string WorkingHoursEn { get; set; }

        [GlobalDisplayNameAttribute("lblWorkingHoursAr")]
        public string WorkingHoursAr { get; set; }

        [RegularExpression("([1-9][0-9]*)",ErrorMessage="Invalid number")]
        [Display(Name="Order")]
        public short? DisplayOrder { get; set; }

        [GlobalDisplayNameAttribute("lblRowStatus")]
        public string RowStatus { get; set; }
    }
}