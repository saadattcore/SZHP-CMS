using DataContract.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SZHPCMS.Models
{
    public class DocumentViewModel : IDocument
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