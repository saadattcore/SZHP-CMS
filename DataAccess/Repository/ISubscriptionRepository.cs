using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Database;
using DataAccess.CommonRespository;

namespace DataAccess.Repository
{
    public interface ISubscriptionRepository : IGenericRepository<Subscriber>
    {
        /// <summary>
        /// Get active subscribers . Subscribers which are not deleted
        /// </summary>
        /// <returns></returns>
        List<Subscriber> GetActiveSubscribers();
    }
}
