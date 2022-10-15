using DataAccess.CommonRespository;
using DataAccess.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class SiteMapRepository : GenericRepository<Site_Map>, ISiteMapRepository
    {
        public SiteMapRepository(ITJCMSEntities context)
            : base(context)
        {

        }

        public List<Site_Map> GetActiveSiteMaps(out int totalRecords, long objectToExclude = 0, int pageIndex = 0, int pageSize = 0)
        {
            int startIndex = (pageIndex * pageSize);

            int count = _dbSet.Where(x => x.Row_Status_Id != (long?)SZHPCMS.Common.RowStatus.Delete).Count();

            IQueryable<Site_Map> query = _dbSet.Where(x => x.Row_Status_Id != (long?)SZHPCMS.Common.RowStatus.Delete).OrderBy(x => x.Created_Date).Skip(startIndex).Take(pageSize);

            totalRecords = count;

            return objectToExclude > 0 ? query.Where(x => x.Site_Map_Id != objectToExclude).ToList() : query.ToList();

        }


        public List<Help> GetActiveHelp(string dataFor = "")
        {

            return dataFor == "web" ? _context.Helps.Where(x => x.Row_Status_Id == (long?)SZHPCMS.Common.RowStatus.Active).OrderByDescending(x => x.Created_Date).ToList() : _context.Helps.Where(x => x.Row_Status_Id != (long?)SZHPCMS.Common.RowStatus.Delete).OrderByDescending(x => x.Created_Date).ToList();
                       
        }


        public Help GetHelpByID(long id)
        {
            return _context.Helps.Find(id);
        }

        public void AddHelp(Help dbHelp)
        {
            _context.Helps.Add(dbHelp);
        }
    }
}
