using BusinessLogic.BusinessHandler;
using SZHPCMS.Common;
using DataAccess.CommonRespository;
using DataContract.Implementation;
using DataContract.Interfaces;
using SZHPCMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SZHPCMS.Controllers
{
    public class MenuController : BaseController
    {
        private readonly MenuBH _menuBL;
        private readonly GenericBH _genericBL;

        public MenuController()
        {
            _menuBL = new MenuBH(new UnitOfWork());
            _genericBL = new GenericBH();
        }

        //
        // GET: /Menu/
        public ActionResult Index()
        {
            var menuList = _menuBL.GetActiveMenus();
            ViewBag.DropDownMenus = Utilities.Utility.GetActionOptions();

            return View(AutoMapperUtil.GetList<MenuModel, MenuViewModel>(menuList));
        }

        //
        // GET: /Menu/Create
        public ActionResult CreateUpdate(long itemID = 0, string operation = "Create")
        {
            ActionResult viewToReturn = null;
            ViewBag.Operation = operation;

            switch (operation)
            {
                case "Create":
                    viewToReturn = View();
                    break;
                case "Update":
                    var returnMenu = _menuBL.GetByID(itemID);
                    viewToReturn = View(AutoMapperUtil.Get<MenuModel, MenuViewModel>(returnMenu));
                    break;
                default:
                    break;
            }

            ViewBag.MenuIDToUpdate = itemID;
            ViewBag.Operation = operation;

            return viewToReturn;
        }

        //
        // POST: /Menu/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUpdate(MenuViewModel model, long itemID = 0, string operation = "Create")
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var menuModel = AutoMapperUtil.Get<MenuViewModel, MenuModel>(model);

                    switch (operation)
                    {
                        case "Create":
                            menuModel.CreatedBy = UserID;
                            menuModel = _menuBL.AddMenu(menuModel);
                            break;
                        case "Update":
                           
                            menuModel.Id = itemID;
                            _menuBL.UpdateMenu(menuModel);
                            break;

                        default:
                            break;
                    }

                    TempData[Constants.MESSAGE] = operation == "Update" ? Constants.RECORD_UPDATED_MESSAGE : Constants.RECORD_ADDED_MESSAGE;

                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }

            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Menu/Delete/5
        [HttpPost]
        public JsonResult Delete(string id)
        {
            if (!Request.IsAjaxRequest() || string.IsNullOrEmpty(id))
                throw new HttpException(400, "Method cannot be invoked");

            JsonResult result = null;

            try
            {
                if (_menuBL.Delete(int.Parse(id)) > 0)
                {
                    result = Json(new { status = true, message = "item has been deleted" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                result = Json(new { status = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

            return result;

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PerformAction(FormCollection formCollection)
        {
            long ddlSelectedValue = Convert.ToInt64(formCollection["ddlActions"]);

            RowStatus action = (SZHPCMS.Common.RowStatus)ddlSelectedValue;

            string[] chkBoxItems = formCollection["chkBoxItem"].Split(',');

            IEnumerable<long> idArray = chkBoxItems.Select(long.Parse);

            _menuBL.UpdateRowStatus(idArray, action);

            TempData[Constants.MESSAGE] = SZHPCMS.Common.Constants.ACTION_APPLIED_MESSAGE;

            return RedirectToAction("Index");

        }

        ////
        //// POST: /Menu/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
