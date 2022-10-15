using BusinessLogic.BusinessHandler;
using SZHPCMS.Common;
using DataAccess.CommonRespository;
using DataContract.Implementation;
using SZHPCMS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SZHPCMS.Utilities;

namespace SZHPCMS.Controllers
{
    public class EServicesController : BaseController
    {
        private readonly EServiceBH _eServiceBL;
        public EServicesController()
        {
            _eServiceBL = new EServiceBH(new UnitOfWork());
        }

        #region E-Services Region

        //
        // GET: /E-Service/
        public ActionResult Index()
        {
            var result = _eServiceBL.GetActiveEServices();

            ViewBag.DropDownMenus = Utilities.Utility.GetActionOptions();

            return View(AutoMapperUtil.GetList<EServiceModel, EServiceViewModel>(result));
        }

        //
        // GET: /E-Service/Create
        public ActionResult CreateUpdate(long itemID = 0, string operation = "Create")
        {
            ActionResult viewToReturn = null;

            switch (operation)
            {
                case "Create":

                    EServiceViewModel eServiceViewModel = new EServiceViewModel();

                    eServiceViewModel.CategoryDropDown = new List<SelectListItem>();

                    _eServiceBL.GetActiveServiceCategories().ForEach((serviceCat) =>
                    {

                        eServiceViewModel.CategoryDropDown.Add(new SelectListItem() { Text = serviceCat.CategoryNameEn, Value = serviceCat.EServiceCategoryId.ToString() });

                    });

                    viewToReturn = View(eServiceViewModel);
                    break;

                case "Update":

                    var modelService = _eServiceBL.GetByID(itemID);

                    var vmEService = AutoMapperUtil.Get<EServiceModel, EServiceViewModel>(modelService);

                    vmEService.DocumentName = modelService.Document != null ? modelService.Document.FileName : string.Empty;

                    vmEService.CategoryID = vmEService.CategoryID;

                    vmEService.CategoryDropDown = new List<SelectListItem>();

                    _eServiceBL.GetActiveServiceCategories().ForEach((serviceCat) =>
                    {

                        vmEService.CategoryDropDown.Add(new SelectListItem() { Text = serviceCat.CategoryNameEn, Value = serviceCat.EServiceCategoryId.ToString() });

                    });

                    viewToReturn = View(vmEService);
                    break;

                default:
                    break;
            }

            ViewBag.ItemID = itemID;
            ViewBag.Operation = operation;

            return viewToReturn;
        }

        //
        // POST: /E-Service/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult CreateUpdate(EServiceViewModel viewModel, long itemID = 0, string operation = "Create")
        {
            ActionResult viewToReturn = null;

            try
            {
                if (ModelState.IsValid)
                {
                    EServiceModel modelNews = AutoMapperUtil.Get<EServiceViewModel, EServiceModel>(viewModel);


                    if (viewModel.Image != null)
                    {
                        if (viewModel.Image.ContentLength > 0)
                        {
                            modelNews.Document = new DocumentModel();
                            modelNews.Document.FileName = viewModel.Image.FileName;
                            modelNews.Document.Extenstion = viewModel.Image.ContentType;
                        }
                    }

                    int result = 0;

                    switch (operation)
                    {
                        case "Create":
                            modelNews.CreatedBy = UserID;
                            modelNews = _eServiceBL.Add(modelNews);

                            break;
                        case "Update":
                            modelNews.UpdatedBy = UserID;
                            result = _eServiceBL.Update(modelNews);

                            break;

                        default:
                            break;
                    }

                    if ((modelNews.EServiceID > 0 || result > 0) && viewModel.Image != null)
                    {
                        if (viewModel.Image.ContentLength > 0)
                        {
                            Utilities.Utility.UploadFile(viewModel.Image, Path.Combine(Utility.DocumentUploadFolder, "E-Services"),modelNews.EServiceID);
                        }
                    }

                    TempData[Constants.MESSAGE] = operation == "Update" ? Constants.RECORD_UPDATED_MESSAGE : Constants.RECORD_ADDED_MESSAGE;

                    viewToReturn = RedirectToAction("Index");
                }
                else
                {
                    viewToReturn = View(viewModel);
                }

                return viewToReturn;

            }
            catch
            {
                return View();
            }
        }

        // POST: E-Service/Delete/5

        [HttpPost]
        public JsonResult Delete(string id)
        {
            if (!Request.IsAjaxRequest() || string.IsNullOrEmpty(id))
                throw new HttpException(400, "Method cannot be invoked");

            JsonResult result = null;

            try
            {
                if (_eServiceBL.Delete(int.Parse(id)) > 0)
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

            _eServiceBL.UpdateRowStatus(idArray, action);

            TempData[Constants.MESSAGE] = SZHPCMS.Common.Constants.ACTION_APPLIED_MESSAGE;

            return RedirectToAction("Index");

        }

        #endregion

        #region E-Participation Region
        //
        // GET: /E-Participation/
        public ActionResult IndexParticipation()
        {
            var result = _eServiceBL.GetActiveEParticipation();

            ViewBag.DropDownMenus = Utilities.Utility.GetActionOptions();

            return View(Translator.TranslateList<EParticipationModel, EParticipationViewModel>(result));
        }

        //
        // GET: /E-Participation/Create
        public ActionResult CreateUpdateParticipation(long itemID = 0, string operation = "Create")
        {
            ActionResult viewToReturn = null;

            switch (operation)
            {
                case "Create":
                    viewToReturn = View();
                    break;

                case "Update":

                    var modelParticipation = _eServiceBL.GetParticipationByID(itemID);
                    var vmToReturn = Translator.TranslateObject<EParticipationModel, EParticipationViewModel>(modelParticipation);
                    vmToReturn.DocumentName = modelParticipation.Document != null ? modelParticipation.Document.FileName : string.Empty;
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
        // POST: /E-Participation/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult CreateUpdateParticipation(EParticipationViewModel viewModel, long itemID = 0, string operation = "Create")
        {
            ActionResult viewToReturn = null;

            try
            {
                if (ModelState.IsValid)
                {
                    EParticipationModel modelParticipation = AutoMapperUtil.Get<EParticipationViewModel, EParticipationModel>(viewModel);


                    if (viewModel.Document != null)
                    {
                        if (viewModel.Document.ContentLength > 0 && !string.IsNullOrEmpty(viewModel.Document.FileName))
                        {
                            modelParticipation.Document = new DocumentModel();
                            modelParticipation.Document.FileName = viewModel.Document.FileName;
                            modelParticipation.Document.Extenstion = viewModel.Document.ContentType;
                        }
                    }

                    int result = 0;

                    switch (operation)
                    {
                        case "Create":
                            modelParticipation.CreatedBy = UserID;
                            modelParticipation = _eServiceBL.AddParticipation(modelParticipation);

                            break;
                        case "Update":

                            modelParticipation.UpdatedBy = UserID;
                            modelParticipation.EParticipationId = itemID;
                            result = _eServiceBL.UpdateParticipation(modelParticipation);

                            break;

                        default:
                            break;
                    }

                    if ((modelParticipation.EParticipationId > 0 || result > 0) && viewModel.Document != null)
                    {
                        if (viewModel.Document.ContentLength > 0 && !string.IsNullOrEmpty(viewModel.Document.FileName))
                        {
                            Utilities.Utility.UploadFile(viewModel.Document, Path.Combine(Utility.DocumentUploadFolder,"EParticipation"),modelParticipation.EParticipationId);
                        }
                    }

                    TempData[Constants.MESSAGE] = operation == "Update" ? Constants.RECORD_UPDATED_MESSAGE : Constants.RECORD_ADDED_MESSAGE;

                    viewToReturn = RedirectToAction("IndexParticipation");
                }
                else
                {
                    viewToReturn = View(viewModel);
                }

                return viewToReturn;

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(viewModel);
            }
        }


        // POST: E-Participation/Delete/5

        [HttpPost]
        public JsonResult DeleteParticipation(string id)
        {
            if (!Request.IsAjaxRequest() || string.IsNullOrEmpty(id))
                throw new HttpException(400, "Method cannot be invoked");

            JsonResult result = null;

            try
            {
                if (_eServiceBL.DeleteParticipation(int.Parse(id)) > 0)
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
        public ActionResult PerformActionParticipation(FormCollection formCollection)
        {
            long ddlSelectedValue = Convert.ToInt64(formCollection["ddlActions"]);

            RowStatus action = (SZHPCMS.Common.RowStatus)ddlSelectedValue;

            string[] chkBoxItems = formCollection["chkBoxItem"].Split(',');

            IEnumerable<long> idArray = chkBoxItems.Select(long.Parse);

            _eServiceBL.UpdateRowStatusParticipation(idArray, action);

            TempData[Constants.MESSAGE] = SZHPCMS.Common.Constants.ACTION_APPLIED_MESSAGE;

            return RedirectToAction("IndexParticipation");

        }
        #endregion

        #region E-Services-Category Region

        //
        // GET: /E-Service/
        public ActionResult IndexServiceCategory()
        {
            var result = _eServiceBL.GetActiveServiceCategories();

            ViewBag.DropDownMenus = Utilities.Utility.GetActionOptions();

            return View(Translator.TranslateList<EServiceCategoryModel,EServiceCategoryViewModel>(result));
        }

        //
        // GET: /E-Service/Create
        public ActionResult CreateUpdateServiceCategory(long itemID = 0, string operation = "Create")
        {
            ActionResult viewToReturn = null;

            switch (operation)
            {
                case "Create":               
                    viewToReturn = View();

                    break;

                case "Update":

                    var modelService = _eServiceBL.GetServiceCatById(itemID);

                    var vmCategories = Translator.TranslateObject<EServiceCategoryModel, EServiceCategoryViewModel>(modelService);

                    viewToReturn = View(vmCategories);
                    break;

                default:
                    break;
            }

            ViewBag.ItemID = itemID;
            ViewBag.Operation = operation;

            return viewToReturn;
        }

        //
        // POST: /E-Service/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
      
        public ActionResult CreateUpdateServiceCategory(EServiceCategoryViewModel viewModel, long itemID = 0, string operation = "Create")
        {
            ActionResult viewToReturn = null;

            try
            {
                if (ModelState.IsValid)
                {
                    viewModel.EServiceCategoryId = itemID;

                    EServiceCategoryModel modelNews = AutoMapperUtil.Get<EServiceCategoryViewModel, EServiceCategoryModel>(viewModel);
             

                    int result = 0;

                    switch (operation)
                    {
                        case "Create":
                            modelNews.CreatedBy = UserID;
                            modelNews = _eServiceBL.AddServiceCat(modelNews);

                            break;
                        case "Update":

                            modelNews.UpdatedBy = UserID;
                            result = _eServiceBL.UpdateServiceCat(modelNews);

                            break;

                        default:
                            break;
                    }

                    TempData[Constants.MESSAGE] = operation == "Update" ? Constants.RECORD_UPDATED_MESSAGE : Constants.RECORD_ADDED_MESSAGE;

                    viewToReturn = RedirectToAction("IndexServiceCategory");
                }
                else
                {
                    viewToReturn = View(viewModel);
                }

                return viewToReturn;

            }
            catch
            {
                return View();
            }
        }

        // POST: E-Service/Delete/5

        [HttpPost]
        public JsonResult DeleteServiceCategory(string id)
        {
            if (!Request.IsAjaxRequest() || string.IsNullOrEmpty(id))
                throw new HttpException(400, "Method cannot be invoked");

            JsonResult result = null;

            try
            {
                if (_eServiceBL.DeleteServiceCategory(int.Parse(id)) > 0)
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
        public ActionResult PerformActionServiceCategory(FormCollection formCollection)
        {
            long ddlSelectedValue = Convert.ToInt64(formCollection["ddlActions"]);

            RowStatus action = (SZHPCMS.Common.RowStatus)ddlSelectedValue;

            string[] chkBoxItems = formCollection["chkBoxItem"].Split(',');

            IEnumerable<long> idArray = chkBoxItems.Select(long.Parse);

            _eServiceBL.UpdateRowStatusServiceCategory(idArray, action);

            TempData[Constants.MESSAGE] = SZHPCMS.Common.Constants.ACTION_APPLIED_MESSAGE;

            return RedirectToAction("IndexServiceCategory");

        }

        #endregion

    }
}