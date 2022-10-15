using SZHPCMS.Common;
using DataAccess.CommonRespository;
using DataAccess.Database;
using DataContract.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.BusinessHandler
{
    public class PageBH
    {
        private readonly IUnitOfWork _uow;

        public PageBH(IUnitOfWork uow)
        {
            _uow = uow;
        }

        /// <summary>
        /// Get FAQ object by ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PageModel GetByID(long id)
        {
            var dbPage = _uow.PageRepository.GetByID(id);

            var modelPage = new PageModel()
            {
                Page_Id = dbPage.Page_Id,
                Page_Name_En = dbPage.Page_Name_En,
                Page_Name_Ar = dbPage.Page_Name_Ar,
                Page_Content_En = dbPage.Page_Content_En,
                Page_Content_Ar = dbPage.Page_Content_Ar,
                Meta_Title_En = dbPage.Meta_Title_En,
                Meta_Title_Ar = dbPage.Meta_Title_Ar,
                Meta_Keywords_En = dbPage.Meta_Keywords_En,
                Meta_Keywords_Ar = dbPage.Meta_Title_Ar,
                Meta_Description_Ar = dbPage.Meta_Description_Ar,
                Meta_Description_En = dbPage.Meta_Description_En,
                Parent_Id = dbPage.Parent_Id,
                IsStandalone = dbPage.IsStandalone,
                Row_Status_Id = dbPage.Row_Status_Id,
                Created_Date = dbPage.Created_Date,
                Url = dbPage.Url

            };

            modelPage.PageMenus = new List<long>();

            foreach (var item in dbPage.Page_Menu)
            {
                modelPage.PageMenus.Add(item.Menu.Menu_Id);
            }

            return modelPage;
        }

        /// <summary>
        /// Get all Pages from db.
        /// </summary>
        /// <returns></returns>
        public List<PageModel> GetAll()
        {
            var collection = _uow.PageRepository.GetAll().Select(dbPage => new PageModel()
            {

                Page_Id = dbPage.Page_Id,
                Page_Name_En = dbPage.Page_Name_En,
                Page_Name_Ar = dbPage.Page_Name_Ar,
                Page_Content_En = dbPage.Page_Content_En,
                Page_Content_Ar = dbPage.Page_Content_Ar,
                Meta_Title_En = dbPage.Meta_Title_En,
                Meta_Title_Ar = dbPage.Meta_Title_Ar,
                Meta_Keywords_En = dbPage.Meta_Keywords_En,
                Meta_Keywords_Ar = dbPage.Meta_Title_Ar,
                Meta_Description_Ar = dbPage.Meta_Description_Ar,
                Meta_Description_En = dbPage.Meta_Description_En,
                Parent_Id = dbPage.Parent_Id,
                IsStandalone = dbPage.IsStandalone,
                Row_Status_Id = dbPage.Row_Status_Id,
                Created_Date = dbPage.Created_Date


            }).ToList();


            foreach (var item in collection)
            {
                item.RowStatus = item.Row_Status_Id == null ? "N/A" : Enum.GetName(typeof(RowStatus), item.Row_Status_Id);
            }

            return collection;
        }

        /// <summary>
        /// Get active Pages objects from db. Whose status are not delete.
        /// </summary>
        /// <returns></returns>
        public List<PageModel> GetActivePages(long excludePage = 0, bool isAdmin = true)
        {
            //var collection = _uow.PageRepository.GetActivePages(excludePage, isAdmin).Select(dbPage => new PageModel()
            //{
            //    Page_Id = dbPage.Page_Id,
            //    Page_Name_En = dbPage.Page_Name_En,
            //    Page_Name_Ar = dbPage.Page_Name_Ar,
            //    Page_Content_En = dbPage.Page_Content_En,
            //    Page_Content_Ar = dbPage.Page_Content_Ar,
            //    Meta_Title_En = dbPage.Meta_Title_En,
            //    Meta_Title_Ar = dbPage.Meta_Title_Ar,
            //    Meta_Keywords_En = dbPage.Meta_Keywords_En,
            //    Meta_Keywords_Ar = dbPage.Meta_Title_Ar,
            //    Meta_Description_Ar = dbPage.Meta_Description_Ar,
            //    Meta_Description_En = dbPage.Meta_Description_En,
            //    Parent_Id = dbPage.Parent_Id,
            //    IsStandalone = dbPage.IsStandalone,
            //    Row_Status_Id = dbPage.Row_Status_Id,
            //    RowStatus = Enum.GetName(typeof(RowStatus), dbPage.Row_Status_Id),
            //    Created_Date = dbPage.Created_Date,
            //    ParentName = dbPage.Page2 != null ? dbPage.Page2.Page_Name_En : ""  ,


            //}).ToList();


            var collection = _uow.PageRepository.GetActivePages(excludePage, isAdmin);

            List<PageModel> modelPages = new List<PageModel>();

            foreach (var item in collection)
            {
                PageModel modelPage = new PageModel();

                modelPage.Page_Id = item.Page_Id;
                modelPage.Page_Name_En = item.Page_Name_En;
                modelPage.Page_Name_Ar = item.Page_Name_Ar;
                modelPage.Page_Content_En = item.Page_Content_En;
                modelPage.Page_Content_Ar = item.Page_Content_Ar;
                modelPage.Meta_Title_En = item.Meta_Title_En;
                modelPage.Meta_Title_Ar = item.Meta_Title_Ar;
                modelPage.Meta_Keywords_En = item.Meta_Keywords_En;
                modelPage.Meta_Keywords_Ar = item.Meta_Title_Ar;
                modelPage.Meta_Description_Ar = item.Meta_Description_Ar;
                modelPage.Meta_Description_En = item.Meta_Description_En;
                modelPage.Parent_Id = item.Parent_Id;
                modelPage.IsStandalone = item.IsStandalone;
                modelPage.Row_Status_Id = item.Row_Status_Id;
                modelPage.RowStatus = Enum.GetName(typeof(RowStatus), item.Row_Status_Id);
                modelPage.Created_Date = item.Created_Date;
                modelPage.ParentName = item.Page2 != null ? item.Page2.Page_Name_En : "";
                modelPage.Url = item.Url;


                modelPage.SubMenus = new List<PageModel>();

                foreach (var subPage in item.Page1)
                {
                    PageModel subModelPage = new PageModel();

                    subModelPage.Page_Id = subPage.Page_Id;
                    subModelPage.Page_Name_En = subPage.Page_Name_En;
                    subModelPage.Page_Name_Ar = subPage.Page_Name_Ar;
                    subModelPage.Url = subPage.Url;

                    modelPage.SubMenus.Add(subModelPage);
                }


                modelPages.Add(modelPage);
            }

            return modelPages;
        }

        /// <summary>
        /// Add new Page obj into db.
        /// </summary>
        /// <param name="news">Page object to add</param>
        /// <returns></returns>
        public PageModel Add(PageModel pageModel)
        {
            try
            {
                Page dbPage = new Page()
                {
                    Page_Id = pageModel.Page_Id,
                    Page_Name_En = pageModel.Page_Name_En,
                    Page_Name_Ar = pageModel.Page_Name_Ar,
                    Page_Content_En = pageModel.Page_Content_En,
                    Page_Content_Ar = pageModel.Page_Content_Ar,
                    Meta_Title_En = pageModel.Meta_Title_En,
                    Meta_Title_Ar = pageModel.Meta_Title_Ar,
                    Meta_Keywords_En = pageModel.Meta_Keywords_En,
                    Meta_Keywords_Ar = pageModel.Meta_Title_Ar,
                    Meta_Description_Ar = pageModel.Meta_Description_Ar,
                    Meta_Description_En = pageModel.Meta_Description_En,
                    Parent_Id = pageModel.Parent_Id,
                    IsStandalone = pageModel.IsStandalone,
                    Created_Date = DateTime.Now,
                    Row_Status_Id = (long?)RowStatus.Active,
                    Url = pageModel.Url


                };

                if (pageModel.PageMenus != null)
                {
                    if (pageModel.PageMenus.Count > 0)
                    {
                        foreach (var item in pageModel.PageMenus)
                        {
                            //Menu menu = _uow.MenuRepository.GetByID(item);

                            Page_Menu pageMenu = new Page_Menu();
                            // pageMenu.Menu = menu;
                            pageMenu.Menu_Id = item;

                            dbPage.Page_Menu.Add(pageMenu);
                        }
                    }

                }

                _uow.PageRepository.Add(dbPage);
                _uow.Save();

                return pageModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Delete Page object from data base . It should be soft delete i.e just update row status to delete
        /// </summary>
        /// <param name="menu">Page object to delete</param>
        /// <returns></returns>
        public int Delete(int pageID)
        {
            Page dbPage = _uow.PageRepository.GetByID(pageID);
            dbPage.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Delete;

            return _uow.Save();
        }

        /// <summary>
        /// Update page status .
        /// </summary>
        /// <param name="menus"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int UpdateRowStatus(IEnumerable<long> pageIDList, RowStatus status)
        {
            foreach (var id in pageIDList)
            {
                Page dbPage = _uow.PageRepository.GetByID(id);
                dbPage.Row_Status_Id = (long?)status;
            }

            return _uow.Save();
        }

        /// <summary>
        /// Update page in data base
        /// </summary>
        /// <param name="news">Page object to update</param>
        /// <returns>Number of rows effected</returns>
        public int Update(PageModel pageModel)
        {
            try
            {
                Page dbPage = _uow.PageRepository.GetByID(pageModel.Page_Id);

                dbPage.Page_Name_En = pageModel.Page_Name_En;
                dbPage.Page_Name_Ar = pageModel.Page_Name_Ar;
                dbPage.Page_Content_En = pageModel.Page_Content_En;
                dbPage.Page_Content_Ar = pageModel.Page_Content_Ar;
                dbPage.Meta_Title_En = pageModel.Meta_Title_En;
                dbPage.Meta_Title_Ar = pageModel.Meta_Title_Ar;
                dbPage.Meta_Keywords_En = pageModel.Meta_Keywords_En;
                dbPage.Meta_Keywords_Ar = pageModel.Meta_Title_Ar;
                dbPage.Meta_Description_Ar = pageModel.Meta_Description_Ar;
                dbPage.Meta_Description_En = pageModel.Meta_Description_En;
                dbPage.Parent_Id = pageModel.Parent_Id;
                dbPage.IsStandalone = pageModel.IsStandalone;
                dbPage.Url = pageModel.Url;
                dbPage.Updated_Date = System.DateTime.Now;
                dbPage.Updated_By = pageModel.Updated_By;


                // 1 - If in db there is menu Id and it do not exist in source menu list then delete relation between menu and page  from db

                List<Page_Menu> menusToDelete = new List<Page_Menu>();

                if (pageModel.PageMenus == null)
                {

                    menusToDelete.AddRange(dbPage.Page_Menu);
                }
                else
                {
                    foreach (var item in dbPage.Page_Menu)
                    {
                        if (!pageModel.PageMenus.Exists(x => x == item.Menu.Menu_Id))
                        {
                            // Remove menu from page collection and user have uncheck this menu...
                            menusToDelete.Add(item);
                        }
                    }
                }


                _uow.PageRepository.DeletePageMenus(menusToDelete);

                // 2 - If menu is in page list and it do not exist in db then add new one .

                if (pageModel.PageMenus != null)
                {
                    foreach (var item in pageModel.PageMenus)
                    {
                        if (dbPage.Page_Menu.Where(x => x.Menu_Id == (long?)item).ToList().Count == 0)
                        {
                            // Add new menu for Page : TO-DO

                            Page_Menu objPageMenu = new Page_Menu();
                            objPageMenu.Menu_Id = item;
                            dbPage.Page_Menu.Add(objPageMenu);
                        }
                    }

                }


                return _uow.Save();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Get next row id to be insert
        /// </summary>
        /// <returns></returns>
        public long NextID()
        {
            return _uow.PageRepository.NextSequenceID();
        }

        public List<PageModel> GetMenu(string location)
        {
            List<Page> menus = _uow.PageRepository.GetMenus(location);


            List<PageModel> modelMenus = new List<PageModel>();

            foreach (var item in menus)
            {
                PageModel modelMenu = new PageModel();

                modelMenu.Page_Id = item.Page_Id;
                modelMenu.Page_Name_Ar = item.Page_Name_Ar;
                modelMenu.Page_Name_En = item.Page_Name_En;
                modelMenu.Url = item.Url;
                modelMenu.IsStandalone = item.IsStandalone;
                if (item.Page1 != null)
                {
                    item.Page1 = item.Page1.OrderBy(x => x.Page_Id).ToList();

                    modelMenu.SubMenus = new List<PageModel>();

                    foreach (var childDbItem in item.Page1)
                    {
                        modelMenu.SubMenus.Add(new PageModel() { Page_Id = childDbItem.Page_Id, Page_Name_Ar = childDbItem.Page_Name_Ar, Page_Name_En = childDbItem.Page_Name_En, Url = childDbItem.Url });
                    }
                }

                modelMenus.Add(modelMenu);
            }

            ITJCMSEntities ctx = _uow.DataContext();

            ctx.Configuration.LazyLoadingEnabled = true;

            return modelMenus;

        }

        public List<PageModel> GetAllMenu()
        {
            List<Page> menus = _uow.PageRepository.GetAllMenus();



            List<PageModel> modelMenus = new List<PageModel>();

            foreach (var item in menus)
            {
                PageModel modelMenu = new PageModel();

                modelMenu.Page_Name_Ar = item.Page_Name_Ar;
                modelMenu.Page_Name_En = item.Page_Name_En;
                modelMenu.Url = item.Url;
                modelMenu.IsStandalone = item.IsStandalone;
                modelMenu.Parent_Id = item.Parent_Id == null ? 0 : item.Parent_Id;
                if (item.Page1 != null)
                {
                    modelMenu.SubMenus = new List<PageModel>();

                    foreach (var childDbItem in item.Page1)
                    {
                        modelMenu.SubMenus.Add(new PageModel() { Page_Name_Ar = childDbItem.Page_Name_Ar, Page_Name_En = childDbItem.Page_Name_En, Url = childDbItem.Url });
                    }
                }

                modelMenus.Add(modelMenu);
            }

            ITJCMSEntities ctx = _uow.DataContext();

            ctx.Configuration.LazyLoadingEnabled = true;

            return modelMenus;

        }
    }
}
