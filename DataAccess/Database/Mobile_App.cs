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
    
    public partial class Mobile_App
    {
        public long Mobile_Application_Id { get; set; }
        public string Name_En { get; set; }
        public string Name_Ar { get; set; }
        public string Description_En { get; set; }
        public string Description_Ar { get; set; }
        public Nullable<long> Document_Id { get; set; }
        public string App_Store_URL { get; set; }
        public Nullable<long> IPhone_QR_Code_Doc_Id { get; set; }
        public string Play_Store_URL { get; set; }
        public Nullable<long> Android_QR_Code_Doc_Id { get; set; }
        public string BlackBerry_World_URL { get; set; }
        public Nullable<long> BlackBerry_QR_Code_Doc_Id { get; set; }
        public string Win_Store_URL { get; set; }
        public Nullable<long> Win_QR_Code_Doc_Id { get; set; }
        public Nullable<long> Created_By { get; set; }
        public Nullable<System.DateTime> Created_Date { get; set; }
        public Nullable<long> Updated_By { get; set; }
        public Nullable<System.DateTime> Updated_Date { get; set; }
        public Nullable<long> Row_Status_Id { get; set; }
    
        public virtual Document Document { get; set; }
        public virtual Document Document1 { get; set; }
        public virtual Document Document2 { get; set; }
        public virtual Document Document3 { get; set; }
        public virtual Document Document4 { get; set; }
    }
}
