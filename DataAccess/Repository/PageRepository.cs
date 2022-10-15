using DataAccess.CommonRespository;
using DataAccess.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccess.Repository
{
    public class PageRepository : GenericRepository<Page>, IPageRepository
    {
        public PageRepository(ITJCMSEntities context)
            : base(context)
        {

        }

        public List<Page> GetActivePages(long excludePage = 0 , bool isAdmin = true)
        {
            IQueryable<Page> objQuery = isAdmin ? this._context.Pages.Where(x => x.Row_Status_Id != (long?)SZHPCMS.Common.RowStatus.Delete).OrderByDescending(x => x.Created_Date) : this._context.Pages.Where(x => x.Row_Status_Id == (long?)SZHPCMS.Common.RowStatus.Active);

            if(isAdmin)
            if (excludePage > 0)
                objQuery = objQuery.Where(x => x.Page_Id != excludePage);

            return objQuery.ToList();
        }

        public List<String> GetPageMenus(long pageID)
        {
            var page = this.GetByID(pageID);

            List<string> pageMenus = new List<string>();

            foreach (var item in page.Page_Menu)
                pageMenus.Add(item.Menu.Menu_Name_En);

            return pageMenus;
        }

        public void DeletePageMenus(List<Page_Menu> pageMenus)
        {
            _context.Page_Menu.RemoveRange(pageMenus);
        }


        public long NextSequenceID()
        {
            var page = _context.Pages.OrderByDescending(x => x.Page_Id).FirstOrDefault();

            return page != null ? page.Page_Id + 1 : 1;
        }

        public List<Page> GetMenus(string location)
        {

            _context.Configuration.LazyLoadingEnabled = false; 

            var pages = (from pageMenu in _context.Page_Menu
                                join menu in _context.Menus
                                on pageMenu.Menu_Id equals menu.Menu_Id
                                join page in _context.Pages
                                on pageMenu.Page_Id equals page.Page_Id
                                where page.IsStandalone == false && page.Parent_Id == null && menu.Menu_Name_En == location && menu.Row_Status_Id != (long)SZHPCMS.Common.RowStatus.Delete && page.Row_Status_Id == (long)SZHPCMS.Common.RowStatus.Active
                                select page).OrderBy(x => x.Created_Date).Select(x => new { ParentPage = x, ChildPages = x.Page1.Where(y => y.Row_Status_Id == (long?)SZHPCMS.Common.RowStatus.Active) }).ToList();

           

            var result =  pages.Select(x => x.ParentPage).ToList();

            //_context.Configuration.LazyLoadingEnabled = true;

            return result;
        }

        public List<Page> GetAllMenus()
        {

            _context.Configuration.LazyLoadingEnabled = false;

            var pages = (from pageMenu in _context.Page_Menu
                         join menu in _context.Menus
                         on pageMenu.Menu_Id equals menu.Menu_Id
                         join page in _context.Pages
                         on pageMenu.Page_Id equals page.Page_Id
                         where  menu.Row_Status_Id == (long)SZHPCMS.Common.RowStatus.Active && page.Row_Status_Id == (long)SZHPCMS.Common.RowStatus.Active
                         select page).OrderBy(x => x.Created_Date).Select(x => new { ParentPage = x, ChildPages = x.Page1.Where(y => y.Row_Status_Id == (long?)SZHPCMS.Common.RowStatus.Active) }).ToList();

            var result = pages.Select(x => x.ParentPage).ToList();
            return result;
        }
    }
}
