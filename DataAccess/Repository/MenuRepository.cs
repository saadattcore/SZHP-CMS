using DataAccess.CommonRespository;
using DataContract.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Database;

namespace DataAccess.Repository
{
    public class MenuRepository : GenericRepository<Menu>, IMenuRepository
    {
        public MenuRepository(ITJCMSEntities context)
            : base(context)
        {

        }

        public List<Menu> GetActiveMenu()
        {
            return _context.Menus.Where(x => x.Row_Status_Id != (long?)SZHPCMS.Common.RowStatus.Delete).OrderByDescending(x => x.Created_Date).ToList();
        }

        public List<Menu> GetMenus(string location)
        {
            var menus = (from page in this._context.Pages
                         join menupage in this._context.Page_Menu
                         on page.Page_Id equals menupage.Page_Id
                         join menu in this._context.Menus
                         on menupage.Menu_Id equals menu.Menu_Id
                         where menu.Menu_Name_En == location && page.IsStandalone == true && page.Parent_Id == null
                         select menu).ToList();

            return menus;

        }

       
    }
}
