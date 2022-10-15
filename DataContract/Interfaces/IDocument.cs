using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContract.Interfaces
{
   public interface IDocument 
    {
        long DocumentId { get; set; }
        long FileTypeId { get; set; }
        string FileName { get; set; }
        string Extenstion { get; set; }
    }
}
