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
    
    public partial class Survey_Question
    {
        public Survey_Question()
        {
            this.Survey_Question_Answer = new HashSet<Survey_Question_Answer>();
            this.Survey_Question_Option = new HashSet<Survey_Question_Option>();
        }
    
        public long Survey_Question_Id { get; set; }
        public Nullable<long> Survey_Id { get; set; }
        public string Question_Ar { get; set; }
        public string Question_En { get; set; }
        public Nullable<long> Created_By { get; set; }
        public Nullable<System.DateTime> Created_Date { get; set; }
        public Nullable<long> Updated_By { get; set; }
        public Nullable<System.DateTime> Updated_Date { get; set; }
        public Nullable<long> Row_Status_Id { get; set; }
    
        public virtual Survey Survey { get; set; }
        public virtual ICollection<Survey_Question_Answer> Survey_Question_Answer { get; set; }
        public virtual ICollection<Survey_Question_Option> Survey_Question_Option { get; set; }
    }
}
