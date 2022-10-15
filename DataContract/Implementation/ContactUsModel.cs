using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContract.Implementation
{
    public class ContactUsModel
    {
        public long ContactUsId { get; set; }
        public string FullName { get; set; }
        public string MobilePhoneNumber { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Inquiry { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public string RowStatus { get; set; }
    }

    public class ContactModel
    {
        public long ContactId { get; set; }
        public string CityAr { get; set; }
        public string CityEn { get; set; }
        public string LocationEn { get; set; }
        public string LocationAr { get; set; }
        public Nullable<double> Latitude { get; set; }
        public Nullable<double> Longitude { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string POBox { get; set; }
        public string Email { get; set; }
        public string WorkingHoursEn { get; set; }
        public string WorkingHoursAr { get; set; }
        public short? DisplayOrder { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string RowStatus { get; set; }

    }
}
