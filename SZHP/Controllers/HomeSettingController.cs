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

namespace SZHPCMS.Controllers
{
    public class HomeSettingController : BaseController
    {
        private readonly HomeSettingBH _homeSettingBL;
        private readonly DocumentBH _docBL;

        #region CTOR
        public HomeSettingController()
        {
            var uow = new UnitOfWork();

            _homeSettingBL = new HomeSettingBH(uow);
            _docBL = new DocumentBH(uow);
        }
        #endregion

        #region Home Settings Region
        //
        // GET: /HomeSetting/
        public ActionResult Index()
        {
            return View();
        }

        //GET : /HomeSetting/CreateUpdate
        [HttpGet]
        public ActionResult CreateUpdate()
        {
            var objModel = _homeSettingBL.GetHomeSetting();

            var objViewModel = AutoMapperUtil.Get<HomeSettingModel, HomeSettingViewModel>(objModel);

            if (objModel.BannerDocument != null)
                objViewModel.BannerDocumentName = objModel.BannerDocument.FileName;


            if (objModel.ExcelDocument != null)
                objViewModel.ExcelDocumentName = objModel.ExcelDocument.FileName;

            if (objModel.WordDocument != null)
                objViewModel.WordDocumentName = objModel.WordDocument.FileName;

            if (objModel.PDFDocument != null)
                objViewModel.PDFDocumentName = objModel.PDFDocument.FileName;

            return View(objViewModel);
        }

        // POST : /HomeSetting/CreateUpdate

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUpdate(HomeSettingViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                HomeSettingModel model = AutoMapperUtil.Get<HomeSettingViewModel, HomeSettingModel>(viewModel);


                if (viewModel.BannerDocument != null)
                {
                    if (viewModel.BannerDocument.ContentLength > 0)
                    {
                        model.BannerDocument = new DocumentModel();
                        model.BannerDocument.FileName = viewModel.BannerDocument.FileName;
                        model.BannerDocument.Extenstion = viewModel.BannerDocument.ContentType;
                    }

                }

                if (viewModel.WordDocument != null)
                {
                    if (viewModel.WordDocument.ContentLength > 0)
                    {
                        model.WordDocument = new DocumentModel();
                        model.WordDocument.FileName = viewModel.WordDocument.FileName;
                        model.WordDocument.Extenstion = viewModel.WordDocument.ContentType;
                    }

                }

                if (viewModel.ExcelDocument != null)
                {
                    if (viewModel.ExcelDocument.ContentLength > 0)
                    {
                        model.ExcelDocument = new DocumentModel();
                        model.ExcelDocument.FileName = viewModel.ExcelDocument.FileName;
                        model.ExcelDocument.Extenstion = viewModel.ExcelDocument.ContentType;
                    }

                }

                if (viewModel.PDFDocument != null)
                {
                    if (viewModel.PDFDocument.ContentLength > 0)
                    {
                        model.PDFDocument = new DocumentModel();
                        model.PDFDocument.FileName = viewModel.PDFDocument.FileName;
                        model.PDFDocument.Extenstion = viewModel.PDFDocument.ContentType;
                    }

                }


                if (viewModel.HomeSettingId > 0)
                    _homeSettingBL.Update(model);
                else
                    _homeSettingBL.Add(model);

                if (viewModel.BannerDocument != null)
                {
                    if (viewModel.BannerDocument.ContentLength > 0)
                    {
                        Utilities.Utility.UploadFile(viewModel.BannerDocument, Path.Combine(Utilities.Utility.DocumentUploadFolder, "HomeSettings"), viewModel.HomeSettingId);

                        viewModel.BannerDocumentName = viewModel.BannerDocument.FileName;
                    }

                }

                if (viewModel.WordDocument != null)
                {
                    if (viewModel.WordDocument.ContentLength > 0)
                    {
                        Utilities.Utility.UploadFile(viewModel.WordDocument, Path.Combine(Utilities.Utility.DocumentUploadFolder, "HomeSettings"), viewModel.HomeSettingId);

                        viewModel.WordDocumentName = viewModel.WordDocument.FileName;
                    }

                }

                if (viewModel.ExcelDocument != null)
                {
                    if (viewModel.ExcelDocument.ContentLength > 0)
                    {
                        Utilities.Utility.UploadFile(viewModel.ExcelDocument, Path.Combine(Utilities.Utility.DocumentUploadFolder, "HomeSettings"), viewModel.HomeSettingId);

                        viewModel.ExcelDocumentName = viewModel.ExcelDocument.FileName;
                    }

                }

                if (viewModel.PDFDocument != null)
                {
                    if (viewModel.PDFDocument.ContentLength > 0)
                    {
                        Utilities.Utility.UploadFile(viewModel.PDFDocument, Path.Combine(Utilities.Utility.DocumentUploadFolder, "HomeSettings"), viewModel.HomeSettingId);

                        viewModel.PDFDocumentName = viewModel.PDFDocument.FileName;
                    }

                }
                

                TempData[SZHPCMS.Common.Constants.MESSAGE] = "Home settings updated sucessfully";

            }

            return RedirectToAction("CreateUpdate");

        }
        #endregion

        #region Mobile Application Region

        [HttpGet]
        public ActionResult MobileApplication()
        {
            var modelApplications = _homeSettingBL.GetMobileApps();

            List<MobileAppViewModel> viewModelsMobileApps = AutoMapperUtil.GetList<MobileAppModel, MobileAppViewModel>(modelApplications);

            ViewBag.DropDownMenus = Utilities.Utility.GetActionOptions();
            return View(viewModelsMobileApps);
        }

        //GET :  /HomeSetting/CreateUpdateApp
        [HttpGet]
        public ActionResult CreateUpdateApp(long itemID = 0, string operation = "Create")
        {
            ViewResult viewToReturn = null;

            switch (operation)
            {
                case "Create":

                    MobileAppViewModel emptyViewModel = new MobileAppViewModel();

                    viewToReturn = View(emptyViewModel);
                    break;

                case "Update":
                    MobileAppModel model = _homeSettingBL.GetAppById(itemID);

                    MobileAppViewModel viewModel = AutoMapperUtil.Get<MobileAppModel, MobileAppViewModel>(model);

                    if (model.Document != null)
                        viewModel.DocumentName = model.Document.FileName;

                    if (model.IPhoneQRCodeDoc != null)
                        viewModel.IPhoneQRCodeDocName = model.IPhoneQRCodeDoc.FileName;

                    if (model.AndroidQRCodeDoc != null)
                        viewModel.AndroidQRCodeDocName = model.AndroidQRCodeDoc.FileName;

                    if (model.BlackBerryQRCodDoc != null)
                        viewModel.BlackBerryQRCodDocName = model.BlackBerryQRCodDoc.FileName;

                    if (model.WinQRCodeDoc != null)
                        viewModel.WinQRCodeDocName = model.WinQRCodeDoc.FileName;

                    viewToReturn = View(viewModel);

                    break;

                default:
                    break;
            }

            ViewBag.Operation = operation;
            ViewBag.ItemID = itemID;

            return viewToReturn;

        }

        // POST : /HomeSetting/CreateUpdateApp
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUpdateApp(MobileAppViewModel viewModel, long itemID = 0, string operation = "Create")
        {
            ActionResult viewToReturn = null;

            MobileAppModel model = AutoMapperUtil.Get<MobileAppViewModel, MobileAppModel>(viewModel);

            try
            {
                if (ModelState.IsValid)
                {
                    #region Model Populating Region

                    if (viewModel.Document != null)
                        model.Document = new DocumentModel() { FileName = viewModel.Document.FileName, Extenstion = viewModel.Document.ContentType };

                    if (viewModel.IPhoneQRCodeDoc != null)
                        model.IPhoneQRCodeDoc = new DocumentModel() { FileName = viewModel.IPhoneQRCodeDoc.FileName, Extenstion = viewModel.IPhoneQRCodeDoc.ContentType };

                    if (viewModel.AndroidQRCodeDoc != null)
                        model.AndroidQRCodeDoc = new DocumentModel() { FileName = viewModel.AndroidQRCodeDoc.FileName, Extenstion = viewModel.AndroidQRCodeDoc.ContentType };

                    if (viewModel.BlackBerryQRCodDoc != null)
                        model.BlackBerryQRCodDoc = new DocumentModel() { FileName = viewModel.BlackBerryQRCodDoc.FileName, Extenstion = viewModel.BlackBerryQRCodDoc.ContentType };

                    if (viewModel.WinQRCodeDoc != null)
                        model.WinQRCodeDoc = new DocumentModel() { FileName = viewModel.WinQRCodeDoc.FileName, Extenstion = viewModel.WinQRCodeDoc.ContentType };

                    #endregion

                    int rowsEffected = 0;

                    #region Switch Region

                    switch (operation)
                    {
                        case "Create":
                            model.CreatedBy = UserID;
                            model = _homeSettingBL.AddMobileApp(model);
                            break;

                        case "Update":
                            model.UpdatedBy = UserID;
                            rowsEffected = _homeSettingBL.UpdateMobileApp(model);
                            break;
                        default:
                            break;
                    }
                    #endregion

                    #region Document Upload Region

                    if (model.MobileApplicationId > 0 || rowsEffected > 0)
                    {


                        if (viewModel.Document != null)
                        {
                            if (viewModel.Document.ContentLength > 0 && !string.IsNullOrEmpty(viewModel.Document.FileName))
                            {
                                Utilities.Utility.UploadFile(viewModel.Document, Path.Combine(Utilities.Utility.DocumentUploadFolder, "MobileApp"), model.MobileApplicationId);
                            }
                        }



                        if (viewModel.IPhoneQRCodeDoc != null)
                        {
                            if (viewModel.IPhoneQRCodeDoc.ContentLength > 0 && !string.IsNullOrEmpty(viewModel.IPhoneQRCodeDoc.FileName))
                            {
                                Utilities.Utility.UploadFile(viewModel.IPhoneQRCodeDoc, Path.Combine(Utilities.Utility.DocumentUploadFolder, "MobileApp"), model.MobileApplicationId);
                            }
                        }



                        if (viewModel.AndroidQRCodeDoc != null)
                        {
                            if (viewModel.AndroidQRCodeDoc.ContentLength > 0 && !string.IsNullOrEmpty(viewModel.AndroidQRCodeDoc.FileName))
                            {
                                Utilities.Utility.UploadFile(viewModel.AndroidQRCodeDoc, Path.Combine(Utilities.Utility.DocumentUploadFolder, "MobileApp"), model.MobileApplicationId);
                            }
                        }

                        if (viewModel.BlackBerryQRCodDoc != null)
                        {
                            if (viewModel.BlackBerryQRCodDoc.ContentLength > 0 && !string.IsNullOrEmpty(viewModel.BlackBerryQRCodDoc.FileName))
                            {
                                Utilities.Utility.UploadFile(viewModel.BlackBerryQRCodDoc, Path.Combine(Utilities.Utility.DocumentUploadFolder, "MobileApp"), model.MobileApplicationId);
                            }
                        }

                        if (viewModel.WinQRCodeDoc != null)
                        {
                            if (viewModel.WinQRCodeDoc.ContentLength > 0 && !string.IsNullOrEmpty(viewModel.WinQRCodeDoc.FileName))
                            {
                                Utilities.Utility.UploadFile(viewModel.WinQRCodeDoc, Path.Combine(Utilities.Utility.DocumentUploadFolder, "MobileApp"), model.MobileApplicationId);
                            }

                        }

                    }
                    #endregion

                    TempData[Constants.MESSAGE] = operation == "Update" ? Constants.RECORD_UPDATED_MESSAGE : Constants.RECORD_ADDED_MESSAGE;

                    viewToReturn = RedirectToAction("MobileApplication");

                }
                else
                {
                    viewToReturn = View(viewModel);
                }

            }
            catch (Exception ex)
            {
                viewToReturn = View("", ex.Message);
            }

            return viewToReturn;

        }

        // POST: HomeSettings/Delete/5
        [HttpPost]
        public ActionResult Delete(string id)
        {
            if (!Request.IsAjaxRequest() || string.IsNullOrEmpty(id))
                throw new HttpException(400, "Method cannot be invoked");

            JsonResult result = null;

            try
            {
                if (_homeSettingBL.DeleteMobileApp(int.Parse(id)) > 0)
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

        //POST : /HomeSettings/PerformAction
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PerformAction(FormCollection formCollection)
        {
            long ddlSelectedValue = Convert.ToInt64(formCollection["ddlActions"]);

            RowStatus action = (SZHPCMS.Common.RowStatus)ddlSelectedValue;

            string[] chkBoxItems = formCollection["chkBoxItem"].Split(',');

            IEnumerable<long> idArray = chkBoxItems.Select(long.Parse);

            _homeSettingBL.UpdateRowStatus(idArray, action);

            TempData[Constants.MESSAGE] = SZHPCMS.Common.Constants.ACTION_APPLIED_MESSAGE;

            return RedirectToAction("MobileApplication");

        }

        #endregion

        #region Partner Service Region

        // GET : /HomeSettings/PartnerService
        public ActionResult PartnerService()
        {
            var modelPartServices = _homeSettingBL.GetPartnerServices();

            List<PartnerServiceViewModel> viewModelsMobileApps = AutoMapperUtil.GetList<PartnerServiceModel, PartnerServiceViewModel>(modelPartServices);

            ViewBag.DropDownMenus = Utilities.Utility.GetActionOptions();
            return View(viewModelsMobileApps);

        }

        //GET :  /HomeSetting/CreateUpdatePartnerService
        [HttpGet]
        public ActionResult CreateUpdatePartnerService(long itemID = 0, string operation = "Create")
        {
            ViewResult viewToReturn = null;

            switch (operation)
            {
                case "Create":

                    PartnerServiceViewModel emptyViewModel = new PartnerServiceViewModel();

                    viewToReturn = View(emptyViewModel);
                    break;

                case "Update":
                    PartnerServiceModel model = _homeSettingBL.GetPartnerServiceById(itemID);

                    PartnerServiceViewModel viewModel = AutoMapperUtil.Get<PartnerServiceModel, PartnerServiceViewModel>(model);

                    if (model.Document != null)
                        viewModel.DocumentName = model.Document.FileName;

                    viewToReturn = View(viewModel);

                    break;

                default:
                    break;
            }

            ViewBag.Operation = operation;
            ViewBag.ItemID = itemID;

            return viewToReturn;

        }

        // POST : /HomeSetting/CreateUpdatePartnerService
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUpdatePartnerService(PartnerServiceViewModel viewModel, long itemID = 0, string operation = "Create")
        {
            ActionResult viewToReturn = null;

            PartnerServiceModel model = AutoMapperUtil.Get<PartnerServiceViewModel, PartnerServiceModel>(viewModel);

            try
            {
                if (ModelState.IsValid)
                {

                    HttpPostedFileBase file = Request.Files[0];

                    if (file.ContentLength > 0 && !string.IsNullOrEmpty(file.FileName))
                    {
                        model.Document = new DocumentModel() { FileName = file.FileName, Extenstion = file.ContentType };
                    }


                    int rowsEffected = 0;

                    #region Switch Region

                    switch (operation)
                    {
                        case "Create":
                            model.CreatedBy = UserID;
                            model = _homeSettingBL.AddPartnerService(model);
                            break;

                        case "Update":
                            model.UpdatedBy = UserID;
                            model.PartnerServiceId = itemID;
                            rowsEffected = _homeSettingBL.UpdatePartnerService(model);
                            break;
                        default:
                            break;
                    }
                    #endregion

                    #region Document Upload Region

                    if (model.PartnerServiceId > 0 || rowsEffected > 0)
                    {
                        if (file.ContentLength > 0 && !string.IsNullOrEmpty(file.FileName))
                        {
                            Utilities.Utility.UploadFile(file, Path.Combine(Utilities.Utility.DocumentUploadFolder, "PartnerService"), model.PartnerServiceId);
                        }
                    }

                    #endregion

                    TempData[Constants.MESSAGE] = operation == "Update" ? Constants.RECORD_UPDATED_MESSAGE : Constants.RECORD_ADDED_MESSAGE;

                    viewToReturn = RedirectToAction("PartnerService");

                }
                else
                {
                    viewToReturn = View(viewModel);
                }

            }
            catch (Exception ex)
            {
                viewToReturn = View("", ex.Message);
            }

            return viewToReturn;

        }

        // POST: HomeSettings/DeletePartnerService/5
        [HttpPost]
        public ActionResult DeletePartnerService(string id)
        {
            if (!Request.IsAjaxRequest() || string.IsNullOrEmpty(id))
                throw new HttpException(400, "Method cannot be invoked");

            JsonResult result = null;

            try
            {
                if (_homeSettingBL.DeletePartnerService(int.Parse(id)) > 0)
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

        //POST : /HomeSettings/PerformActionPartnerService
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PerformActionPartnerService(FormCollection formCollection)
        {
            long ddlSelectedValue = Convert.ToInt64(formCollection["ddlActions"]);

            RowStatus action = (SZHPCMS.Common.RowStatus)ddlSelectedValue;

            string[] chkBoxItems = formCollection["chkBoxItem"].Split(',');

            IEnumerable<long> idArray = chkBoxItems.Select(long.Parse);

            _homeSettingBL.UpdatePartnerServiceRowStatus(idArray, action);

            TempData[Constants.MESSAGE] = SZHPCMS.Common.Constants.ACTION_APPLIED_MESSAGE;

            return RedirectToAction("PartnerService");

        }

        #endregion

    }
}