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
    
    public partial class Page_Menu
    {
        public long Page_Menu_Id { get; set; }
        public Nullable<long> Page_Id { get; set; }
        public Nullable<long> Menu_Id { get; set; }
    
        public virtual Menu Menu { get; set; }
        public virtual Page Page { get; set; }
    }
}
