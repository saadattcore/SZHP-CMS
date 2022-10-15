using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContract.Interfaces
{
    public interface ISetting
    {
        long Id { get; set; }
        string ClientNameEnglish { get; set; }
        string ClientNameArabic { get; set; }
        string CMSTitleEnglish { get; set; }
        string CMSTitleArabic { get; set; }
        long DocumentId { get; set; }
        string FaceBook { get; set; }
        string Twitter { get; set; }
        string Website { get; set; }
        string Email { get; set; }
        string Logo { get; set; }
    }
}
