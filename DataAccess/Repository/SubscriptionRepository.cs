using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Database;
using DataAccess.CommonRespository;

namespace DataAccess.Repository
{
    public class SubscriptionRepository : GenericRepository<Subscriber>, ISubscriptionRepository
    {
        public SubscriptionRepository(ITJCMSEntities context)
            : base(context)
        {

        }

        public List<Subscriber> GetActiveSubscribers()
        {
            return this._dbSet.Where(x => x.Row_Status_Id != (long?)SZHPCMS.Common.RowStatus.Delete).OrderByDescending(x => x.Created_Date).ToList();
        }
    }
}
