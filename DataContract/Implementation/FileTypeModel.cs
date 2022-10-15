using DataContract.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContract.Implementation
{
    public class FileTypeModel : IFileType
    {
        public long FileTypeId
        {
            get;
            set;
        }

        public string NameEnglish
        {
            get;
            set;
        }

        public string NameArabic
        {
            get;
            set;
        }
    }
}
