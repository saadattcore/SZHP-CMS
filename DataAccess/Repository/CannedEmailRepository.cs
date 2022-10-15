using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Database;
using DataAccess.CommonRespository;

namespace DataAccess.Repository
{
    public class CannedEmailRepository : GenericRepository<Canned_Email> , ICannedEmailRepository
    {
        public CannedEmailRepository(ITJCMSEntities context)
            : base(context)
        {

        }

        public List<Canned_Email> GetActiveEmailTemplates()
        {
            return _dbSet.Where(x => x.Row_Status_Id != (long?)SZHPCMS.Common.RowStatus.Delete).OrderByDescending(x => x.Created_Date).ToList();
        }
    }
}
