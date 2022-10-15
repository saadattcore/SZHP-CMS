using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataContract.Implementation
{
    public class ApplicantContentCategoryModel
    {
        public long Applicant_Content_Id { get; set; }
        public string Title_Ar { get; set; }
        public string Title_En { get; set; }
        public string Description_Ar { get; set; }
        public string Description_En { get; set; }
        public Nullable<long> Applicant_Category { get; set; }
        public Nullable<long> Created_By { get; set; }
        public Nullable<System.DateTime> Created_Date { get; set; }
        public Nullable<long> Updated_By { get; set; }
        public Nullable<System.DateTime> Updated_Date { get; set; }
        public Nullable<long> Row_Status_Id { get; set; }
        public string Row_Status { get; set; }
        public string ParentSubCategory { get; set; }

        public List<ApplicantContentSubCategoryModel> ApplicantContentSubCategories { get; set; }
        public ApplicantCategoryModel ApplicantCategory { get; set; }
    }

    public class ApplicantContentSubCategoryModel
    {
        public long Applicant_Content_SubCategory_Id { get; set; }
        public string Title_En { get; set; }
        public string Title_Ar { get; set; }
        public string Description_En { get; set; }
        public string Description_Ar { get; set; }
        public Nullable<long> Applicant_Content_Id { get; set; }
        public Nullable<long> Created_By { get; set; }
        public Nullable<System.DateTime> Created_Date { get; set; }
        public Nullable<long> Updated_By { get; set; }
        public Nullable<System.DateTime> Updated_Date { get; set; }
        public Nullable<long> Row_Status_Id { get; set; }
        public string Row_Status { get; set; }
    }
}