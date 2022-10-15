using DataAccess.CommonRespository;
using DataAccess.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface ISiteMapRepository : IGenericRepository<Site_Map>
    {
        /// <summary>
        /// Get active site maps
        /// </summary>
        /// <returns></returns>
        List<Site_Map> GetActiveSiteMaps(out int totalRecords , long objectToExclude = 0, int pageIndex = 0, int pageSize = 0 );

        /// <summary>
        /// Get active help
        /// </summary>
        /// <returns></returns>
        List<Help> GetActiveHelp(string dataFor = "");

        /// <summary>
        /// Get help by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Help GetHelpByID(long id);

        /// <summary>
        /// Add help
        /// </summary>
        /// <param name="dbHelp"></param>
        void AddHelp(Help dbHelp);
    }
}
