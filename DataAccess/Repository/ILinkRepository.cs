using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Database;
using DataAccess.CommonRespository;

namespace DataAccess.Repository
{
    public interface ILinkRepository  : IGenericRepository<Link>
    {
        /// <summary>
        /// Get active links .
        /// </summary>
        /// <returns></returns>
        List<Link> GetActiveLinks();
    }
}
