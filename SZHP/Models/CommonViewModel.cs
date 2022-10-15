using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SZHPCMS.Utilities;

namespace SZHPCMS.Models
{
    public class CallCenterPollViewModel
    {
        public long CallCenterRatingId { get; set; }
        public Nullable<int> Rating { get; set; }

        [GlobalDisplayNameAttribute("lblRating")]
        public string RatingName { get; set; }
        public Nullable<long> CreatedBy { get; set; }

        [GlobalDisplayNameAttribute("lblCreatedDate")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> RowStatusId { get; set; }

        [GlobalDisplayNameAttribute("lblRowStatus")]
        public string RowStatus { get; set; }
    }

    public class FeedBackViewModel
    {
        public long FeedBackId { get; set; }

        [GlobalDisplayNameAttribute("lblNameEnglish")]
        public string Name { get; set; }

        [GlobalDisplayNameAttribute("lblEmail")]
        public string Email { get; set; }

        [GlobalDisplayNameAttribute("lblComments")]
        public string Comments { get; set; }

        [GlobalDisplayNameAttribute("lblPageName")]
        public string PageName { get; set; }

        [GlobalDisplayNameAttribute("lblCreatedDate")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public Nullable<System.DateTime> CreatedDate { get; set; }

        [GlobalDisplayNameAttribute("lblRowStatus")]
        public string RowStatus { get; set; }
    }

    public class PollViewModel
    {

        public long Poll_Id { get; set; }

        [Display(Name = "Title En")]
        [Required(ErrorMessage = "This field is required")]
        public string Poll_En { get; set; }

        [Display(Name = "Title Ar")]
        [Required(ErrorMessage = "This field is required")]
        public string Poll_Ar { get; set; }

        [Display(Name = "Poll Opt En")]
        [Required(ErrorMessage = "This field is required")]
        public string PollOpt_En { get; set; }

        [Display(Name = "Poll Opt Ar")]
        [Required(ErrorMessage = "This field is required")]
        public string PollOpt_Ar { get; set; }

        [Display(Name = "Created Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public Nullable<System.DateTime> Created_Date { get; set; }
        public Nullable<long> Created_By { get; set; }
        public Nullable<System.DateTime> Updated_Date { get; set; }
        public Nullable<long> Updated_By { get; set; }
        public Nullable<long> Row_Status_Id { get; set; }

        [Display(Name = "Row Status")]
        public string RowStatus { get; set; }
        public List<PollOptionViewModel> Options { get; set; }
    }

    public class PollOptionViewModel
    {
        public long Poll_Option_Id { get; set; }

        [Display(Name = "Option En")]
        [Required(ErrorMessage = "This field is required")]
        public string Option_En { get; set; }

        [Display(Name = "Option Ar")]
        [Required(ErrorMessage = "This field is required")]
        public string Option_Ar { get; set; }

        [Display(Name = "Poll")]
        [Required(ErrorMessage = "This field is required")]
        public long Poll_Id { get; set; }

        [Display(Name = "Created Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public Nullable<System.DateTime> Created_Date { get; set; }
        public Nullable<long> Created_By { get; set; }
        public Nullable<System.DateTime> Updated_Date { get; set; }
        public Nullable<long> Updated_By { get; set; }
        public Nullable<long> Row_Status_Id { get; set; }

        [Display(Name = "Row Status")]
        public string RowStatus { get; set; }
        public List<SelectListItem> Polls { get; set; }
        public string Poll_En { get; set; }
        public string Poll_Ar { get; set; }
    }

    public class VoteStatsViewModel
    {
        [Display(Name="Poll Option")]
        public string PollOptionEn { get; set; }
        public string PollOptionAr { get; set; }
        public long Count { get; set; }
        public int Percent { get; set; }

        public string Poll { get; set; }
    }

    public class HappinessStatsViewModel
    {
        public string Option { get; set; }
        public long Count { get; set; }
        public int Percent { get; set; }

    }
}