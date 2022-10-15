using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataContract.Interfaces;
using DataContract.Implementation;
using DataAccess.Repository;
using DataAccess.CommonRespository;
using DataAccess.Database;
using SZHPCMS.Common;

namespace BusinessLogic.BusinessHandler
{
    public class MenuBH
    {
        private readonly IUnitOfWork _uow;
        public MenuBH(IUnitOfWork uow)
        {
            _uow = uow;
        }

        /// <summary>
        /// Add new menu in data base
        /// </summary>
        /// <param name="menu">Menu object to save</param>
        /// <returns>Last inserted menu</returns>
        public MenuModel AddMenu(MenuModel menu)
        {

            Menu objMenu = new Menu()
            {
                Menu_Id = menu.Id,
                Menu_Name_Ar = menu.TitleArabic,
                Menu_Name_En = menu.TitleEnglish,
                Row_Status_Id = (long?)RowStatus.Active,
                Created_Date = DateTime.Now

            };

            _uow.MenuRepository.Add(objMenu);
            _uow.Save();

            menu.Id = objMenu.Menu_Id;

            return menu;
        }

        /// <summary>
        /// Get all menu from data base
        /// </summary>
        /// <returns>Menu list</returns>
        public List<MenuModel> GetAllMenu()
        {
            return _uow.MenuRepository.GetAll().Select(x => new MenuModel()
            {

                Id = x.Menu_Id,
                TitleEnglish = x.Menu_Name_En,
                TitleArabic = x.Menu_Name_Ar,
                CreatedDate = x.Created_Date

            }).ToList();
        }

        /// <summary>
        /// Get menu by ID
        /// </summary>
        /// <param name="menuID">Menu id to fetch</param>
        /// <returns>Menu from db</returns>
        public MenuModel GetByID(long menuID)
        {
            var returnMenu = _uow.MenuRepository.GetByID(menuID);

            MenuModel objMenu = new MenuModel() { Id = returnMenu.Menu_Id, TitleEnglish = returnMenu.Menu_Name_En, TitleArabic = returnMenu.Menu_Name_Ar, CreatedDate = returnMenu.Created_Date };
            return objMenu;

        }

        /// <summary>
        /// Update menu in data base
        /// </summary>
        /// <param name="menu">Menu object to update</param>
        /// <returns>Number of rows effected</returns>
        public int UpdateMenu(MenuModel menu)
        {
            Menu dbMenu = _uow.MenuRepository.GetByID(menu.Id);

            dbMenu.Menu_Name_En = menu.TitleEnglish;
            dbMenu.Menu_Name_Ar = menu.TitleArabic;
            dbMenu.Updated_Date = System.DateTime.Now;

            return _uow.Save();
        }

        /// <summary>
        /// Delete menu object from data base
        /// </summary>
        /// <param name="menu">Menu object to delete</param>
        /// <returns></returns>
        public int Delete(int menuID)
        {
            Menu dbMenu = _uow.MenuRepository.GetByID(menuID);
            dbMenu.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Delete;

            return _uow.Save();
        }

        /// <summary>
        /// Get all menus whose row status = null
        /// </summary>
        /// <returns></returns>
        public List<MenuModel> GetActiveMenus()
        {
            var collection = _uow.MenuRepository.GetActiveMenu().Select(model => new MenuModel()
            {

                Id = model.Menu_Id,
                TitleArabic = model.Menu_Name_Ar,
                TitleEnglish = model.Menu_Name_En,
                RowStatusID = model.Row_Status_Id,
                CreatedDate = model.Created_Date

            }).ToList();

            foreach (var item in collection)
            {
                item.RowStatus = Enum.GetName(typeof(RowStatus), item.RowStatusID);
            }

            return collection;
        }

        /// <summary>
        /// Update menus status .
        /// </summary>
        /// <param name="menus"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int UpdateRowStatus(IEnumerable<long> menusID, RowStatus status)
        {
            foreach (var id in menusID)
            {
                Menu dbMenu = _uow.MenuRepository.GetByID(id);
                dbMenu.Row_Status_Id = (long?)status;
            }

            return _uow.Save();
        }

    }
}
