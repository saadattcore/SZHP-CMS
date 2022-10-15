using BusinessLogic.BusinessHandler;
using SZHPCMS.Common;
using DataAccess.CommonRespository;
using DataContract.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SZHPCMS.Models;

namespace SZHPCMS.Controllers
{
    public class SiteMapController : BaseController
    {

        private readonly SiteMapBH _siteMapBL;
        public SiteMapController()
        {
            _siteMapBL = new SiteMapBH(new UnitOfWork());
        }

        #region Site Map Region

        //
        // GET: /SiteMap/
        public ActionResult Index()
        {
            //var vmSiteMaps = _siteMapBL.GetActiveSiteMaps();

            ViewBag.DropDownMenus = Utilities.Utility.GetActionOptions();

            //return View(AutoMapperUtil.GetList<SiteMapModel, SiteMapViewModel>(vmSiteMaps));
            return View();
        }

        [HttpGet]
        public JsonResult LoadData()
        {
            try
            {
                string filter = Request.QueryString["search[value]"];
                int draw = Convert.ToInt32(Request.QueryString["draw"]);
                int start = Convert.ToInt32(Request.QueryString["start"]);
                int length = Convert.ToInt32(Request.QueryString["length"]);


                int totalRecords = 0;

                var viewModels = _siteMapBL.GetActiveSiteMaps(out totalRecords, 0, start, length);

                var result = Json(new { draw = draw, recordsFiltered = totalRecords, recordsTotal = totalRecords, data = viewModels }, JsonRequestBehavior.AllowGet);

                return result;


            }
            catch (Exception)
            {

                throw;
            }
        }

        //
        // GET: /SiteMap/Create
        public ActionResult CreateUpdate(long itemID = 0, string operation = "Create")
        {
            ActionResult viewToReturn = null;

            switch (operation)
            {
                case "Create":

                    SiteMapViewModel vmSiteMap = new SiteMapViewModel();
                    vmSiteMap.SiteMaps = DropDownSiteMap();
                    viewToReturn = View(vmSiteMap);

                    break;

                case "Update":

                    var modelSiteMap = _siteMapBL.GetSiteMapByID(itemID);
                    var vmToReturn = AutoMapperUtil.Get<SiteMapModel, SiteMapViewModel>(modelSiteMap);
                    vmToReturn.SiteMaps = DropDownSiteMap(itemID);
                    viewToReturn = View(vmToReturn);

                    break;

                default:
                    break;
            }

            ViewBag.ItemID = itemID;
            ViewBag.Operation = operation;

            return viewToReturn;
        }

        //
        // POST: /SiteMap/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUpdate(SiteMapViewModel viewModel, long itemID = 0, string operation = "Create")
        {
            ActionResult viewToReturn = null;

            try
            {
                if (ModelState.IsValid)
                {
                    SiteMapModel vmSiteMap = AutoMapperUtil.Get<SiteMapViewModel, SiteMapModel>(viewModel);

                    switch (operation)
                    {
                        case "Create":
                            vmSiteMap.CreatedBy = UserID;
                            vmSiteMap = _siteMapBL.Add(vmSiteMap);
                            break;

                        case "Update":

                            vmSiteMap.UpdatedBy = UserID;
                            vmSiteMap.SiteMapId = itemID;
                            _siteMapBL.Update(vmSiteMap);

                            break;

                        default:
                            break;
                    }

                    TempData[Constants.MESSAGE] = operation == "Update" ? Constants.RECORD_UPDATED_MESSAGE : Constants.RECORD_ADDED_MESSAGE;

                    viewToReturn = RedirectToAction("Index");
                }
                else
                {
                    viewModel.SiteMaps = DropDownSiteMap();

                    viewToReturn = View(viewModel);
                }

                return viewToReturn;

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return viewToReturn;
        }


        // POST: News/Delete/5
        [HttpPost]
        public JsonResult Delete(string id)
        {
            if (!Request.IsAjaxRequest() || string.IsNullOrEmpty(id))
                throw new HttpException(400, "Method cannot be invoked");

            JsonResult result = null;

            try
            {
                if (_siteMapBL.Delete(int.Parse(id)) > 0)
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

            _siteMapBL.UpdateRowStatus(idArray, action);

            TempData[Constants.MESSAGE] = SZHPCMS.Common.Constants.ACTION_APPLIED_MESSAGE;

            return RedirectToAction("Index");

        }

        [NonAction]
        private List<SelectListItem> DropDownSiteMap(long objectToExclude = 0)
        {
            int totalRecords = 0;

            var modelSiteMaps = _siteMapBL.GetActiveSiteMaps(out totalRecords, objectToExclude);

            List<SelectListItem> dropDown = new List<SelectListItem>();

            modelSiteMaps.ForEach((x) =>
            {

                dropDown.Add(new SelectListItem() { Text = x.NameEn, Value = x.SiteMapId.ToString() });

            });

            return dropDown;
        }
        #endregion

        #region Help Region
        //
        // GET: /SiteMap/
        public ActionResult HelpIndex()
        {
            var vmHelp = _siteMapBL.GetActiveHelp();

            ViewBag.DropDownMenus = Utilities.Utility.GetActionOptions();

            return View(AutoMapperUtil.GetList<HelpModel, HelpViewModel>(vmHelp));
        }

        //
        // GET: /SiteMap/Create
        public ActionResult HelpCreateUpdate(long itemID = 0, string operation = "Create")
        {
            ActionResult viewToReturn = null;

            switch (operation)
            {
                case "Create":
                    viewToReturn = View();

                    break;

                case "Update":

                    var modelHelp = _siteMapBL.GetHelpByID(itemID);
                    var vmToReturn = AutoMapperUtil.Get<HelpModel, HelpViewModel>(modelHelp);
                    viewToReturn = View(vmToReturn);

                    break;

                default:
                    break;
            }

            ViewBag.ItemID = itemID;
            ViewBag.Operation = operation;

            return viewToReturn;
        }

        //
        // POST: /SiteMap/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult HelpCreateUpdate(HelpViewModel viewModel, long itemID = 0, string operation = "Create")
        {
            ActionResult viewToReturn = null;

            try
            {
                if (ModelState.IsValid)
                {
                    HelpModel modelHelp = AutoMapperUtil.Get<HelpViewModel, HelpModel>(viewModel);

                    switch (operation)
                    {
                        case "Create":
                            modelHelp = _siteMapBL.AddHelp(modelHelp);
                            break;

                        case "Update":
                            modelHelp.HelpId = itemID;
                            _siteMapBL.UpdateHelp(modelHelp);

                            break;

                        default:
                            break;
                    }

                    viewToReturn = RedirectToAction("HelpIndex");
                }
                else
                {
                    viewToReturn = View(viewModel);
                }

                TempData[Constants.MESSAGE] = operation == "Update" ? Constants.RECORD_UPDATED_MESSAGE : Constants.RECORD_ADDED_MESSAGE;

                return viewToReturn;

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return viewToReturn;
        }


        // POST: Site Map/Delete/5
        [HttpPost]
        public JsonResult HelpDelete(string id)
        {
            if (!Request.IsAjaxRequest())
                throw new HttpException(400, "Method cannot be invoked");

            JsonResult result = null;

            try
            {
                if (_siteMapBL.DeleteHelp(int.Parse(id)) > 0)
                {
                    result = Json(new { status = true, message = "item has been deleted" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                result = Json(new { status = true, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HelpPerformAction(FormCollection formCollection)
        {
            long ddlSelectedValue = Convert.ToInt64(formCollection["ddlActions"]);

            RowStatus action = (SZHPCMS.Common.RowStatus)ddlSelectedValue;

            string[] chkBoxItems = formCollection["chkBoxItem"].Split(',');

            IEnumerable<long> idArray = chkBoxItems.Select(long.Parse);

            _siteMapBL.UpdateRowStatusHelp(idArray, action);

            TempData[Constants.MESSAGE] = SZHPCMS.Common.Constants.ACTION_APPLIED_MESSAGE;

            return RedirectToAction("HelpIndex");

        }
        #endregion

    }
}