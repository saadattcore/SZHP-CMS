using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SZHPCMS.Utilities;
using System.ComponentModel.DataAnnotations;

namespace SZHPCMS.Models
{
    public class JobApplicantViewModel
    {
        public long ApplicantID { get; set; }
        public Nullable<long> CareerJobId { get; set; }

        [GlobalDisplayNameAttribute("lblFirstName")]
        public string FirstName { get; set; }

        [GlobalDisplayNameAttribute("lblLastName")]
        public string LastName { get; set; }

        [GlobalDisplayNameAttribute("lblDateOfBirth")]
        //[DisplayFormat(ApplyFormatInEditMode=true,DataFormatString="{0:MM/dd/yyyy}")]
        public Nullable<System.DateTime> DOB { get; set; }

        [GlobalDisplayNameAttribute("lblMaritalStatus")]
        public string MaritalStatus { get; set; }

        [GlobalDisplayNameAttribute("lblHomeAddress")]
        public string HomeAddress { get; set; }

        [GlobalDisplayNameAttribute("lblPhone")]
        public string Phone { get; set; }

        [GlobalDisplayNameAttribute("lblEmail")]
        public string Email { get; set; }

        [GlobalDisplayNameAttribute("lblMobile")]
        public string Mobile { get; set; }

        [GlobalDisplayNameAttribute("lblEducation")]
        public string Eduction { get; set; }

        [GlobalDisplayNameAttribute("lblExperience")]
        public string Experience { get; set; }

        [GlobalDisplayNameAttribute("lblSkills")]
        public string Skills { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> RowStatusId { get; set; }

        [GlobalDisplayNameAttribute("lblRowStatus")]
        public string RowStatus { get; set; }
        public Nullable<long> DocumentId { get; set; }
        public DocumentViewModel Resume { get; set; }

        [GlobalDisplayNameAttribute("lblJobApplied")]
        public string JobApplied { get; set; }
    }
}