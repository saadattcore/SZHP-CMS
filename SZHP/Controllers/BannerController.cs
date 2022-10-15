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
using SZHPCMS.Utilities;

namespace SZHPCMS.Controllers
{
    
    public class BannerController : BaseController
    {
        private readonly BannerBH _bannerBL;
        private readonly DocumentBH _documentBL;
        public BannerController()
        {
            UnitOfWork uow = new UnitOfWork();

            _bannerBL = new BannerBH(uow);
            _documentBL = new DocumentBH(uow);
        }


        //
        // GET: /Banner/
        public ActionResult Index()
        {

            var modelBanners = _bannerBL.GetActiveBanners();

            ViewBag.DropDownMenus = Utilities.Utility.GetActionOptions();

            return View(Translator.TranslateList<BannerModel, BannerViewModel>(modelBanners));
            
        }

        // GET : /Banner/CreateUpdate
        [HttpGet]
        public ActionResult CreateUpdate(long itemID = 0, string operation = "Create")
        {
            ActionResult viewToReturn = null;

            switch (operation)
            {
                case "Create":
                    viewToReturn = View();

                    break;
                case "Update":
                    BannerModel modelBanner = _bannerBL.GetByID(itemID);

                    BannerViewModel viewModelBanner = Translator.TranslateObject<BannerModel, BannerViewModel>(modelBanner);

                    if (modelBanner.Documents != null)
                    {
                        if (modelBanner.Documents.Count > 0)
                        {
                            viewModelBanner.Documents = Translator.TranslateList<DocumentModel, DocumentViewModel>(modelBanner.Documents);
                        }
                    }

                    ViewBag.Operation = operation;

                    viewToReturn = View(viewModelBanner);

                    break;
                default:
                    break;
            }

            ViewBag.Operation = operation;
            ViewBag.ItemID = itemID;

            return viewToReturn;
        }

        //
        // POST: /Banner/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUpdate(BannerViewModel viewModel, long itemID = 0, string operation = "Create")
        {
            ActionResult viewToReturn = null;
            viewModel.BannerId = itemID;

            try
            {
                if (ModelState.IsValid)
                {
                    BannerModel modelBanner = AutoMapperUtil.Get<BannerViewModel, BannerModel>(viewModel);

                    HttpFileCollectionBase imagesToUpload = Request.Files;

                    if (imagesToUpload.Count > 0)
                    {
                        modelBanner.Documents = new List<DocumentModel>();

                        for (int i = 0; i < imagesToUpload.Count; i++)
                        {
                            if (!string.IsNullOrEmpty(imagesToUpload[i].FileName) && imagesToUpload[i].ContentLength > 0)
                                modelBanner.Documents.Add(new DocumentModel() { FileName = imagesToUpload[i].FileName, Extenstion = imagesToUpload[i].ContentType });
                        }
                    }

                    int result = 0;

                    switch (operation)
                    {
                        case "Create":
                            modelBanner.CreatedBy = UserID;
                            modelBanner = _bannerBL.Add(modelBanner);

                            break;
                        case "Update":
                            modelBanner.UpdatedBy = UserID;
                            result = _bannerBL.Update(modelBanner);

                            break;

                        default:
                            break;
                    }

                    if ((modelBanner.BannerId > 0 || result > 0) && (modelBanner.Documents.Count > 0 && imagesToUpload.Count > 0))
                    {
                        for (int i = 0; i < imagesToUpload.Count; i++)
                        {
                            Utilities.Utility.UploadFile(imagesToUpload[i], System.IO.Path.Combine(Utility.DocumentUploadFolder, "Banners"), modelBanner.BannerId);
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
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(viewModel);
            }
        }

        // POST: Ajax delete banner.

        [HttpPost]
        public JsonResult Delete(string id)
        {
            if (!Request.IsAjaxRequest())
                throw new HttpException(400, "Method cannot be invoked");

            JsonResult result = null;

            try
            {
                if (_bannerBL.Delete(int.Parse(id)) > 0)
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


        // POST : /BannerController/PerformAction
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PerformAction(FormCollection formCollection)
        {
            long ddlSelectedValue = Convert.ToInt64(formCollection["ddlActions"]);

            RowStatus action = (SZHPCMS.Common.RowStatus)ddlSelectedValue;

            string[] chkBoxItems = formCollection["chkBoxItem"].Split(',');

            IEnumerable<long> idArray = chkBoxItems.Select(long.Parse);

            _bannerBL.UpdateRowStatus(idArray, action);

            TempData[Constants.MESSAGE] = SZHPCMS.Common.Constants.ACTION_APPLIED_MESSAGE;

            return RedirectToAction("Index");

        }


        // POST : Delete image using ajax call.
        [HttpPost]
        public ActionResult RemoveDocument(string docID)
        {
            if (!Request.IsAjaxRequest() || string.IsNullOrEmpty(docID))
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest, "Wrong doc id . Value of id = " + docID);

            try
            {
                return _documentBL.Delete(long.Parse(docID)) > 0 ? (ActionResult)Json(new { Deleted = true }, JsonRequestBehavior.AllowGet) : new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound, "Server error while deleting image . Contact admin");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound, ex.Message); ;
            }

        }
    }
}
