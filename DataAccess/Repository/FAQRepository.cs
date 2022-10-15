using DataAccess.CommonRespository;
using DataAccess.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class FAQRepository : GenericRepository<FAQ>, IFAQRepository
    {
        public FAQRepository(ITJCMSEntities context)
            : base(context)
        {

        }

        public List<FAQ_Category> GetFAQCategories()
        {
            return this._context.FAQ_Category.ToList();
        }


        public List<FAQ> GetActiveFAQ(string dataFor = "")
        {
            return dataFor == "web" ? _dbSet.Where(x => x.Row_Status_Id == (long?)SZHPCMS.Common.RowStatus.Active).OrderByDescending(x => x.Created_Date).ToList() : _dbSet.Where(x => x.Row_Status_Id != (long?)SZHPCMS.Common.RowStatus.Delete).OrderByDescending(x => x.Created_Date).ToList();

        
        }
    }
}
