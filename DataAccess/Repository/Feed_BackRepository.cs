using DataAccess.CommonRespository;
using DataAccess.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class Feed_BackRepository : GenericRepository<Feed_Back>, IFeed_BackRepository
    {
        public Feed_BackRepository(ITJCMSEntities context)
            : base(context)
        {

        }
       
    }
}
