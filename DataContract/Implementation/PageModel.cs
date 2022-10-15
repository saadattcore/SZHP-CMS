using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContract.Implementation
{
    public class PageModel
    {
        public long Page_Id { get; set; }
        public string Page_Name_En { get; set; }
        public string Page_Content_En { get; set; }
        public Nullable<long> Parent_Id { get; set; }
        public string ParentName { get; set; }
        public string Meta_Title_En { get; set; }
        public string Meta_Keywords_En { get; set; }
        public string Meta_Description_En { get; set; }
        public Nullable<bool> IsStandalone { get; set; }
        public string Page_Name_Ar { get; set; }
        public string Page_Content_Ar { get; set; }
        public string Meta_Title_Ar { get; set; }
        public string Meta_Keywords_Ar { get; set; }
        public string Meta_Description_Ar { get; set; }
        public Nullable<long> Created_By { get; set; }
        public Nullable<System.DateTime> Created_Date { get; set; }
        public Nullable<long> Updated_By { get; set; }
        public Nullable<System.DateTime> Updated_Date { get; set; }
        public Nullable<long> Row_Status_Id { get; set; }
        public string RowStatus { get; set; }
        public List<long> PageMenus { get; set; }
        public string Url { get; set; }
        public List<PageModel> SubMenus { get; set; }

    }
}
