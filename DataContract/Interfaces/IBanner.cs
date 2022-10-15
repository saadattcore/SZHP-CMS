using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContract.Interfaces
{
    public interface IBanner
    {
          long Id { get; set; }
          string TitleArabic { get; set; }
          string TitleEnglish { get; set; }
    }
}
