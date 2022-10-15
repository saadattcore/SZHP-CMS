using DataAccess.CommonRespository;
using DataAccess.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IFAQRepository : IGenericRepository<FAQ>
    {
        /// <summary>
        /// Get FAQ categories
        /// </summary>
        /// <returns></returns>
        List<FAQ_Category> GetFAQCategories();

        /// <summary>
        /// Get active FAQ.
        /// </summary>
        /// <returns></returns>
        List<FAQ> GetActiveFAQ(string dataFor = "");
    }
}
