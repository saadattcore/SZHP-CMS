using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContract.Interfaces
{
    public interface IFileType
    {
        long FileTypeId { get; set; }
        string NameEnglish { get; set; }
        string NameArabic { get; set; }
    }
}
