using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContract.Implementation
{
    public class TrainingPlanModel
    {
        public long TrainingPlanId { get; set; }
        public Nullable<long> TrainingProgramId { get; set; }
        public string TrainingProgram { get; set; }
        public string LocationAr { get; set; }
        public string ThemeAr { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string AudienceAr { get; set; }
        public string NotesAr { get; set; }
        public string LocationEn { get; set; }
        public string AudienceEn { get; set; }
        public string NotesEn { get; set; }
        public string ThemeEn { get; set; }
        public Nullable<long> CreatedBy { get; set; }      
        public Nullable<long> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<long> RowStatusId { get; set; }
        public string RowStatus { get; set; }
    }
}
