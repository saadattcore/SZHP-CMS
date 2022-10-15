using DataContract.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SZHPCMS.Models
{
    public class SettingViewModel
    {
        //public int Id { get; set; }
        //[Required]
        //[Display(Name="Name English")]
        //public string Client_Name_En { get; set; }
        //[Required]
        //[Display(Name = "Name Arabic")]
        //public string Cleint_Name_Ar { get; set; }
        //public Nullable<long> Document_Id { get; set; }
        //[Required]
        //public string FaceBook { get; set; }
        //[Required]
        //public string Twitter { get; set; }
        //[Required]
        //public string Website { get; set; }
        //[Required]
        //[DataType(DataType.EmailAddress)]
        //public string Email { get; set; }

        //public string Logo { get; set; }
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

        //[RegularExpression(@"^http(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&amp;%\$#_]*)?$", ErrorMessage = "Invalid url")]
        [Url(ErrorMessage="Invalid url")]
        public string Website
        {
            get;
            set;
        }

        [EmailAddress(ErrorMessage = "Invalid email address")]
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

        public string DocumentName { get; set; }

        public long RowStatusId
        {
            get;
            set;
        }


    }
}