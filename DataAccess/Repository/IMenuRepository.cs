using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.CommonRespository;
using DataAccess.Database;

namespace DataAccess.Repository
{
    public interface IMenuRepository : IGenericRepository<Menu>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        List<Menu> GetMenus(string location);

        /// <summary>
        /// Get active menus
        /// </summary>
        /// <returns></returns>
        List<Menu> GetActiveMenu();

    }
}
