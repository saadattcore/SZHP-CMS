using DataAccess.CommonRespository;
using DataAccess.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class SocialMediaRepository : GenericRepository<Social_Media>, ISocialMediaRepository
    {
        public SocialMediaRepository(ITJCMSEntities context)
            : base(context)
        {

        }




        public List<Social_Media> GetActiveSocialMedia(bool isAdmin = true)
        {
            return isAdmin ? _dbSet.Where(x => x.Row_Status_Id != (long?)SZHPCMS.Common.RowStatus.Delete).OrderByDescending(x => x.Created_By).ToList() : _dbSet.Where(x => x.Row_Status_Id == (long?)SZHPCMS.Common.RowStatus.Active).OrderByDescending(x => x.Created_By).ToList();
        }
    }
}
