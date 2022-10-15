using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContract.Interfaces
{
    public interface IMenu
    {
        long Id { get; set; }
        string TitleEnglish { get; set; }
        string TitleArabic { get; set; }
        string CreatedBy { get; set; }
        DateTime? CreatedDate { get; set; }
        DateTime? UpdatedDate { get; set; }
        long? RowStatusID { get; set; }


    }
}
