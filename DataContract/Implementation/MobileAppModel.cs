using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContract.Implementation
{
    public class MobileAppModel
    {
        public long MobileApplicationId { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string DescriptionEn { get; set; }
        public string DescriptionAr { get; set; }
        public Nullable<long> DocumentId { get; set; }
        public DocumentModel Document { get; set; }
        public string DocumentName { get; set; }
        public string AppStoreURL { get; set; }
        public Nullable<long> IPhoneQRCodeDocId { get; set; }
        public DocumentModel IPhoneQRCodeDoc { get; set; }
        public string PlayStoreURL { get; set; }
        public Nullable<long> AndroidQRCodeDocId { get; set; }
        public DocumentModel AndroidQRCodeDoc { get; set; }
        public string BlackBerryWorldURL { get; set; }
        public Nullable<long> BlackBerryQRCodDocId { get; set; }
        public DocumentModel BlackBerryQRCodDoc { get; set; }
        public string WinStoreURL { get; set; }
        public Nullable<long> WinQRCodeDocId { get; set; }
        public DocumentModel WinQRCodeDoc { get; set; }

        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> UpdatedBy { get; set; }   
        public Nullable<long> RowStatusId { get; set; }
        public string RowStatus { get; set; }
        
    }
}
