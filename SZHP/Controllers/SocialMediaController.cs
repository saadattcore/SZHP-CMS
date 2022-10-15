using BusinessLogic.BusinessHandler;
using DataAccess.CommonRespository;
using DataContract.Implementation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SZHPCMS.Common;
using SZHPCMS.Models;
using SZHPCMS.Utilities;

namespace SZHPCMS.Controllers
{
    public class SocialMediaController : BaseController
    {
        private readonly SocialMediaBH _socialMediaBL;
        private readonly DocumentBH _docBL;
        public SocialMediaController()
        {
            UnitOfWork uow = new UnitOfWork();

            _socialMediaBL = new SocialMediaBH(uow);
            _docBL = new DocumentBH(uow);

        }

        //
        // GET: /Social Media/Index
        public ActionResult Index()
        {

            var modelSocialMediaList = _socialMediaBL.GetActiveSocialMedia();

            ViewBag.DropDownMenus = Utilities.Utility.GetActionOptions();

            return View(Translator.TranslateList<SocialMediaModel, SocialMediaViewModel>(modelSocialMediaList));
        }

        //
        // GET: /SocialMedia/Create
        public ActionResult CreateUpdate(long itemID = 0, string operation = "Create")
        {
            ActionResult viewToReturn = null;

            switch (operation)
            {
                case "Create":

                    SocialMediaViewModel socialMediaViewModel = new SocialMediaViewModel();

                    viewToReturn = View(socialMediaViewModel);

                    break;

                case "Update":

                    var modelSocialMedia = _socialMediaBL.GetSocialMediaByID(itemID);

                    var vmToReturn = Translator.TranslateObject<SocialMediaModel, SocialMediaViewModel>(modelSocialMedia);

                    if (modelSocialMedia.Document != null)
                        vmToReturn.Document = Translator.TranslateObject<DocumentModel, DocumentViewModel>(modelSocialMedia.Document);

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
        // POST: /FAQ/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult CreateUpdate(SocialMediaViewModel viewModel, long itemID = 0, string operation = "Create")
        {
            ActionResult viewToReturn = null;

            try
            {
                if (ModelState.IsValid)
                {
                    SocialMediaModel modelSocialMedia = Translator.TranslateObject<SocialMediaViewModel, SocialMediaModel>(viewModel);

                    if (operation == "Create" || operation == "")
                    {
                        if (viewModel.ImageFile == null)
                        {
                            ModelState.AddModelError("ImageFile", "This field is required");

                            ViewBag.Operation = "Create";
                            return View(viewModel);
                        }
                        else
                        {
                            modelSocialMedia.Document = new DocumentModel() { FileName = viewModel.ImageFile.FileName, Extenstion = viewModel.ImageFile.ContentType };
                        }
                    }
                    else
                    {
                        if (viewModel.ImageFile != null)
                        {
                            modelSocialMedia.Document = new DocumentModel() { FileName = viewModel.ImageFile.FileName, Extenstion = viewModel.ImageFile.ContentType };
                        }
                    
                    }

                    

                    int rowsEffected = 0;
                    switch (operation)
                    {
                        case "Create":
                            modelSocialMedia.Created_By = UserID;

                            modelSocialMedia = _socialMediaBL.AddSocialMedia(modelSocialMedia);

                            break;
                        case "Update":

                            modelSocialMedia.Social_Media_Id = itemID;

                            _socialMediaBL.UpdateSocialMedia(modelSocialMedia);

                            break;

                        default:
                            break;
                    }

                    if (viewModel.ImageFile != null)
                    {
                        if ((rowsEffected > 0 || modelSocialMedia.Social_Media_Id > 0) && (viewModel.ImageFile.ContentLength > 0) && !string.IsNullOrEmpty(viewModel.ImageFile.FileName))
                        {
                            Utilities.Utility.UploadFile(viewModel.ImageFile, Path.Combine(Utility.DocumentUploadFolder, "SocialMedia"), modelSocialMedia.Social_Media_Id);

                        }
                    }
                                  

                    TempData[SZHPCMS.Common.Constants.MESSAGE] = operation == "Update" ? SZHPCMS.Common.Constants.RECORD_UPDATED_MESSAGE : SZHPCMS.Common.Constants.RECORD_ADDED_MESSAGE;

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


        // POST: Social Media/Delete/5

        [HttpPost]
        public JsonResult Delete(string id)
        {
            if (!Request.IsAjaxRequest() || string.IsNullOrEmpty(id))
                throw new HttpException(400, "Method cannot be invoked");

            JsonResult result = null;

            try
            {
                if (_socialMediaBL.Delete(int.Parse(id)) > 0)
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

        // POST : Delete image using ajax call.
        [HttpPost]
        public ActionResult RemoveDocument(string docID, string docName, string itemID)
        {
            if (!Request.IsAjaxRequest() || string.IsNullOrEmpty(docID))
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            ActionResult resultView = null;

            try
            {
                int result = _docBL.Delete(long.Parse(docID));

                if (result > 0)
                {
                    string path = Path.Combine(Utility.DocumentUploadFolder, "SocialMedia");

                    string filePath = Path.Combine(path, itemID + "_" + docName);

                    Utility.DeleteFile(filePath);

                    resultView = Json(new { Deleted = true }, JsonRequestBehavior.AllowGet);
                }
                else
                    resultView = Json(new { Deleted = false }, JsonRequestBehavior.AllowGet);

                return resultView;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PerformAction(FormCollection formCollection)
        {
            long ddlSelectedValue = Convert.ToInt64(formCollection["ddlActions"]);

            RowStatus action = (SZHPCMS.Common.RowStatus)ddlSelectedValue;

            string[] chkBoxItems = formCollection["chkBoxItem"].Split(',');

            IEnumerable<long> idArray = chkBoxItems.Select(long.Parse);

            _socialMediaBL.UpdateRowStatus(idArray, action);

            TempData[Constants.MESSAGE] = SZHPCMS.Common.Constants.ACTION_APPLIED_MESSAGE;

            return RedirectToAction("Index");

        }


    }
}