using DataAccess.CommonRespository;
using DataAccess.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class GalleryRepository : GenericRepository<Gallery>, IGalleryRepository
    {
        public GalleryRepository(ITJCMSEntities context)
            : base(context)
        {

        }
        public List<Gallery> GetActiveGalleries()
        {
            return this._dbSet.Where(x => x.Row_Status_Id != (long)SZHPCMS.Common.RowStatus.Delete).OrderByDescending(x => x.Created_Date).ToList();
        }
    }
}
