using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContract.Implementation
{
    public class CountryModel
    {
        public long CountryId { get; set; }
        public string CountryNameEn { get; set; }
        public string CountryNameAr { get; set; }
        public string NationalityEn { get; set; }
        public string NationalityAr { get; set; }
    }
}
