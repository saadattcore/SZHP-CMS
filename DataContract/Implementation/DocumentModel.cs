using DataContract.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContract.Implementation
{
    public class DocumentModel : IDocument
    {
        public long DocumentId
        {
            get;
            set;
        }

        public long FileTypeId
        {
            get;
            set;
        }

        public string FileName
        {
            get;
            set;
        }

        public string Extenstion
        {
            get;
            set;
        }

        public long RowStatusId
        {
            get;
            set;
        }
    }
}
