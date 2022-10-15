using DataAccess.CommonRespository;
using DataAccess.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class BannerRepository : GenericRepository<Banner>, IBannerRepository
    {
        public BannerRepository(ITJCMSEntities context)
            : base(context)
        {

        }

        public List<Banner> GetActiveBanners()
        {
            return this._dbSet.Where(x => x.Row_Status_Id != (long?)SZHPCMS.Common.RowStatus.Delete).OrderByDescending(x => x.Created_Date).ToList();
        }

        public Banner GetBanner(long banner, long docID)
        {
            _context.Configuration.LazyLoadingEnabled = false;           

            var dto = _context.Banners.Where(x => x.Banner_Id == banner).Select(x => new { Banner = x, BannerDocs = x.Banner_Documents.Where(y => y.Banner_Document_Id == docID).ToList() }).FirstOrDefault();
        
            return dto.Banner;
        }

    }  
}
