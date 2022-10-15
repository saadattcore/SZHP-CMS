using DataContract.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContract.Implementation
{
    public class RowStatusModel 
    {
        public long RowStatusId
        {
            get;
            set;
        }

        public string TitleEnglish
        {
            get;
            set;
        }

        public string TitleArabic
        {
            get;
            set;
        }

        public DateTime? CreatedBy
        {
            get;
            set;
        }

        public DateTime? CreatedDate
        {
            get;
            set;
        }

        public DateTime? UpdatedDate
        {
            get;
            set;
        }
    }
}
