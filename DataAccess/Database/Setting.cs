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
    
    public partial class Setting
    {
        public long Setting_Id { get; set; }
        public string Client_Name_En { get; set; }
        public string Cleint_Name_Ar { get; set; }
        public Nullable<long> Document_Id { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public Nullable<long> Created_By { get; set; }
        public Nullable<System.DateTime> Created_Date { get; set; }
        public Nullable<long> Updated_By { get; set; }
        public Nullable<System.DateTime> Updated_Date { get; set; }
        public Nullable<long> Row_Status_Id { get; set; }
        public string CMS_Title_En { get; set; }
        public string CMS_Title_Ar { get; set; }
    
        public virtual Document Document { get; set; }
    }
}
