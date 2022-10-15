using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContract.Implementation
{
    public class Feed_BackModel
    {
        public long Feed_Back_Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Comments { get; set; }
        public string IPAddress { get; set; }
        public Nullable<long> Page_Id { get; set; }
        public string Section { get; set; }
        public Nullable<System.DateTime> Created_Date { get; set; }
        public long Created_By { get; set; }
        public Nullable<long> Row_Status_Id { get; set; }

    }

  
}
