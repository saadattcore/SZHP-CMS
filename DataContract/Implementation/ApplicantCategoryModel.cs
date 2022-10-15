using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataContract.Implementation
{
    public class ApplicantCategoryModel
    {
        public long Applicant_Category_Id { get; set; }
        public string Category_Name_En { get; set; }
        public string Category_Name_Ar { get; set; }
        public Nullable<long> Parent_Id { get; set; }
        public string Header_Description_En { get; set; }
        public string Header_Description_Ar { get; set; }
        public Nullable<long> Created_By { get; set; }
        public Nullable<System.DateTime> Created_Date { get; set; }
        public Nullable<long> Updated_By { get; set; }
        public Nullable<System.DateTime> Updated_Date { get; set; }
        public Nullable<long> Row_Status_Id { get; set; }
        public string Row_Status { get; set; }
        public string Parent { get; set; }

        public List<CityDescriptionModel> CityDescriptions { get; set; }
        public List<ApplicantCategoryModel> ApplicantCategoryModels { get; set; }
    }
}