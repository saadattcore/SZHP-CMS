//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class Contact
    {
        public long Contact_Id { get; set; }
        public string City_Ar { get; set; }
        public string City_En { get; set; }
        public string Location_En { get; set; }
        public string Location_Ar { get; set; }
        public Nullable<double> Latitude { get; set; }
        public Nullable<double> Longitude { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string PO_Box { get; set; }
        public string Email { get; set; }
        public string Working_Hours_En { get; set; }
        public string Working_Hours_Ar { get; set; }
        public Nullable<long> Created_By { get; set; }
        public Nullable<System.DateTime> Created_Date { get; set; }
        public Nullable<long> Updated_By { get; set; }
        public Nullable<System.DateTime> Updated_Date { get; set; }
        public Nullable<long> Row_Status_Id { get; set; }
        public Nullable<short> DisplayOrder { get; set; }
    }
}