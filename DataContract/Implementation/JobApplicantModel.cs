using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContract.Implementation
{
    public class JobApplicantModel
    {
        public long ApplicantID { get; set; }
        public Nullable<long> CareerJobId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        public string MaritalStatus { get; set; }
        public string HomeAddress { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Eduction { get; set; }
        public string Experience { get; set; }
        public string Skills { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> RowStatusId { get; set; }
        public string RowStatus { get; set; }
        public Nullable<long> DocumentId { get; set; }
        public DocumentModel Resume { get; set; }
        public string JobApplied { get; set; }

    }
}
