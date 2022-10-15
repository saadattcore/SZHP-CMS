using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SZHPCMS.Models
{
    public class ApplicantCategoryViewModel
    {
        public long Applicant_Category_Id { get; set; }
        public string Category_Name_En { get; set; }
        public string Category_Name_Ar { get; set; }
        public Nullable<long> Parent_Id { get; set; }
        public string Header_Description_En { get; set; }
        public string Header_Description_Ar { get; set; }
        public Nullable<long> Created_By { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public Nullable<System.DateTime> Created_Date { get; set; }
        public Nullable<long> Updated_By { get; set; }
        public Nullable<System.DateTime> Updated_Date { get; set; }
        public Nullable<long> Row_Status_Id { get; set; }
        public string Row_Status { get; set; }
        public string Parent { get; set; }

        public List<ApplicantContentCategoryViewModel> ApplicantContentCategoriesViewModel { get; set; }
        public List<ApplicantCategoryViewModel> CategoryViewModels { get; set; }
    }
}