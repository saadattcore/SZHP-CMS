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
    
    public partial class Feed_Back
    {
        public long Feed_Back_Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Comments { get; set; }
        public Nullable<long> Page_Id { get; set; }
        public string Section { get; set; }
        public Nullable<System.DateTime> Created_Date { get; set; }
        public Nullable<long> Created_By { get; set; }
        public Nullable<long> Row_Status_Id { get; set; }
        public string IPAddress { get; set; }
    }
}
