using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContract.Implementation
{
    public class NewsModel
    {
        public long Id { get; set; }        
        public string TitleEnglish { get; set; }     
        public string TitleArabic { get; set; } 
        public string DescriptionEnglish { get; set; }
        public string DescriptionArabic { get; set; }
        public long CategoryID { get; set; }
        public string CategoryName { get; set; }      
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public long CreatedBy { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string RowStatus { get; set; }
        public long? RowStatusID { get; set; }
        public List<DocumentModel> Documents { get; set; }
    }

    public class NewsCategoryModel
    {
        public long CategoryID { get; set; }
        public string NameEnglish { get; set; }
        public string NameArabic { get; set; }
        public long? CreatedBy { get; set; }
        public System.DateTime? CreatedDate { get; set; }
        public long? UpdatedBy { get; set; }
        public System.DateTime? UpdatedDate { get; set; }
        public long? RowStatusId { get; set; }
    }
}
