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
    
    public partial class City_Description
    {
        public long Description_Id { get; set; }
        public string Description_En { get; set; }
        public string Description_Ar { get; set; }
        public Nullable<long> City_Id { get; set; }
        public Nullable<long> Applicant_Category_Id { get; set; }
        public Nullable<long> Created_By { get; set; }
        public Nullable<System.DateTime> Created_Date { get; set; }
        public Nullable<long> Updated_By { get; set; }
        public Nullable<System.DateTime> Updated_Date { get; set; }
        public Nullable<long> Row_Status_Id { get; set; }
    
        public virtual Applicant_Category Applicant_Category { get; set; }
        public virtual City City { get; set; }
    }
}
