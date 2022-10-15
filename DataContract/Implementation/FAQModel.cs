using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContract.Implementation
{
    public class FAQModel
    {
        public long Id { get; set; }
        public string QuestionEnglish { get; set; }
        public string QuestionArabic { get; set; }
        public string AnswerEnglish { get; set; }
        public string AnswerArabic { get; set; }
        public long? CategoryID { get; set; }
        public string CategoryName { get; set; }
        public long CreatedBy { get; set; }
        public string RowStatus { get; set; }
        public long? RowStatusID { get; set; }
        public DateTime? CreatedDate { get; set; }
    }

    public class FAQCategoryModel
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
