using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.CommonRespository;
using DataAccess.Database;

namespace DataAccess.Repository
{
    public interface ISettingRepository : IGenericRepository<Setting>
    {
        /// <summary>
        /// Get first setting object . Only one setting would be table
        /// </summary>
        /// <returns></returns>
        Setting GetFirst();       
    }
}
