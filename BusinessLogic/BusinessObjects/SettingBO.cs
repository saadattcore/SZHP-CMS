using DataContract.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.BusinessObjects
{
    public class SettingBO : ISetting
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
           get
           {
               throw new NotImplementedException();
           }
           set
           {
               throw new NotImplementedException();
           }
       }

       public string CMSTitleArabic
       {
           get
           {
               throw new NotImplementedException();
           }
           set
           {
               throw new NotImplementedException();
           }
       }
    }
}
