using DataAccess.CommonRespository;
using DataAccess.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IPageRepository : IGenericRepository<Page>
    {
        /// <summary>
        /// Get active page . Pages which are not deleted.
        /// </summary>
        /// <returns></returns>
        List<Page> GetActivePages(long excludePage = 0, bool isAdmin = true);

        /// <summary>
        /// Get page menus
        /// </summary>
        /// <returns></returns>
        List<String> GetPageMenus(long pageID);

        /// <summary>
        /// Delete page menu .
        /// </summary>
        /// <param name="id"></param>
        void DeletePageMenus(List<Page_Menu> pageMenus);

        /// <summary>
        /// Get the latest id from page table
        /// </summary>
        /// <returns></returns>
        long NextSequenceID();

        /// <summary>
        /// Get menus by location
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        List<Page> GetMenus(string location);

        /// <summary>
        /// Get All Menus
        /// </summary>
        /// <returns></returns>
        List<Page> GetAllMenus();
    }    
}
