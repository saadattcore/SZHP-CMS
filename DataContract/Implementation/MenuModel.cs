using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataContract.Interfaces;

namespace DataContract.Implementation
{
    public class MenuModel 
    {
        public long Id { get; set; }
        public string TitleEnglish { get; set; }
        public string TitleArabic { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public long? RowStatusID { get; set; }
        public string RowStatus { get; set; }

    }
}
