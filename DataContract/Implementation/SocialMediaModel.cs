using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContract.Implementation
{
   public class SocialMediaModel
    {
        public long Social_Media_Id { get; set; }
        public string Title_En { get; set; }
        public string Title_Ar { get; set; }
        public string Url { get; set; }
        public string Document_Id { get; set; }
        public string Tool_Tip_En { get; set; }
        public string Tool_Tip_Ar { get; set; }
        public Nullable<System.DateTime> Created_Date { get; set; }
        public Nullable<System.DateTime> Update_Date { get; set; }
        public Nullable<long> Created_By { get; set; }
        public Nullable<long> Updated_By { get; set; }
        public Nullable<long> Row_Status_Id { get; set; }
        public string RowStatus { get; set; }
        public DocumentModel Document { get; set; }
    }
    
}
