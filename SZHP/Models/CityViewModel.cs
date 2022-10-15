using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SZHPCMS.Models
{
    public class CityViewModel
    {
        public long City_Id { get; set; }
        public string City_Name_En { get; set; }
        public string City_Name_Ar { get; set; }
        public Nullable<long> Created_By { get; set; }
        public Nullable<System.DateTime> Created_Date { get; set; }
        public Nullable<long> Updated_By { get; set; }
        public Nullable<System.DateTime> Updated_Date { get; set; }
        public Nullable<long> Row_Status_Id { get; set; }

        public List<CityDescriptionViewModel> CityDescriptions { get; set; }
    }

    public class CityDescriptionViewModel
    {
        public long Desccription_Id { get; set; }
        public string Description_En { get; set; }
        public string Description_Ar { get; set; }
        public Nullable<long> City_Id { get; set; }
        public Nullable<long> Applicant_Category_Id { get; set; }
        public Nullable<long> Created_By { get; set; }
        public Nullable<System.DateTime> Created_Date { get; set; }
        public Nullable<long> Updated_By { get; set; }
        public Nullable<System.DateTime> Updated_Date { get; set; }
        public Nullable<long> Row_Status_Id { get; set; }

        public CityViewModel City { get; set; }
    }
}