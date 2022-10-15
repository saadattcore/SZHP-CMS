using DataContract.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContract.Implementation
{
    public class SettingModel
    {
        public long Id
        {
            get;
            set;
        }

        public string ClientNameEnglish
        {
            get;
            set;
        }

        public string ClientNameArabic
        {
            get;
            set;
        }

        public long DocumentId
        {
            get;
            set;
        }
        public string DocumentName { get; set; }
        public string FaceBook
        {
            get;
            set;
        }

        public string Twitter
        {
            get;
            set;
        }

        public string Website
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }

        public string Logo
        {
            get;
            set;
        }

        public long RowStatusId
        {
            get;
            set;
        }
        public string CMSTitleEnglish
        {
            get;
            set;
        }

        public string CMSTitleArabic
        {
            get;
            set;
        }

        public long UpdatedBy { get; set; }
        public DocumentModel Document { get; set; }
    }
}
