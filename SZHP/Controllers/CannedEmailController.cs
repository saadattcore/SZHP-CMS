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
    public class CannedEmailController : BaseController
    {
        private readonly CannedEmailBH _emailBL;
        private readonly DocumentBH _docBL;
        public CannedEmailController()
        {
            UnitOfWork uow = new UnitOfWork();

            _emailBL = new CannedEmailBH(uow);
            _docBL = new DocumentBH(uow);
        }

        //
        // GET: /Link/Index
        public ActionResult Index()
        {
            var activeEmailTemplates = _emailBL.GetActiveTemplates();

            ViewBag.DropDownMenus = Utilities.Utility.GetActionOptions();

            return View(Translator.TranslateList<CannedEmailModel, CannedEmailViewModel>(activeEmailTemplates));
        }


        // GET : /Link/CreateUpdate

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
                    var modelTemplates = _emailBL.GetByID(itemID);
                    var vmToReturn = Translator.TranslateObject<CannedEmailModel, CannedEmailViewModel>(modelTemplates);

                    if (modelTemplates.Documents != null)
                    {
                        vmToReturn.Documents = AutoMapperUtil.GetList<DocumentModel, DocumentViewModel>(modelTemplates.Documents);
                    }

                    //vmToReturn.Documents = modelTemplates.Documents.Count > 0 ? AutoMapperUtil.GetList<DocumentModel, DocumentViewModel>(modelTemplates.Documents) : null;

                    viewToReturn = View(vmToReturn);
                    break;

                default:
                    viewToReturn = View();
                    break;
            }

            ViewBag.ItemID = itemID;
            ViewBag.Operation = operation;

            return viewToReturn;
        }

        //
        // POST: /Link/CreateUpdate
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult CreateUpdate(CannedEmailViewModel viewModel, long itemID = 0, string operation = "Create")
        {
            ActionResult viewToReturn = null;

            ViewBag.ItemID = itemID;
            ViewBag.Operation = operation;

            HttpFileCollectionBase filesToUplaod = Request.Files;

            try
            {
                if (ModelState.IsValid)
                {
                    CannedEmailModel modelEmil = Translator.TranslateObject<CannedEmailViewModel, CannedEmailModel>(viewModel);

                    if (filesToUplaod.Count > 0)
                    {
                        modelEmil.Documents = new List<DocumentModel>();

                        for (int i = 0; i < filesToUplaod.Count; i++)
                        {
                            if (!string.IsNullOrEmpty(filesToUplaod[i].FileName) && filesToUplaod[i].ContentLength > 0)
                                modelEmil.Documents.Add(new DocumentModel() { FileName = filesToUplaod[i].FileName, Extenstion = filesToUplaod[i].ContentType });
                        }
                    }

                    int result = 0;

                    switch (operation)
                    {
                        case "Create":
                            modelEmil.CreatedBy = UserID;
                            modelEmil = _emailBL.Add(modelEmil);
                            break;

                        case "Update":
                            modelEmil.UpdatedBy = UserID;
                            modelEmil.CannedEmailId = itemID;
                            result = _emailBL.Update(modelEmil);
                            break;

                        default:
                            break;
                    }

                    if ((result > 0 || modelEmil.CannedEmailId > 0) && (filesToUplaod.Count > 0 && modelEmil.Documents.Count > 0))
                    {
                        for (int i = 0; i < filesToUplaod.Count; i++)
                        {
                            Utilities.Utility.UploadFile(filesToUplaod[i], Path.Combine(Utility.DocumentUploadFolder, "CannedEmails"), modelEmil.CannedEmailId);
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
                throw ex;
            }
        }

        //
        // POST: /Link/Delete/5
        [HttpPost]
        public JsonResult Delete(string id)
        {
            if (!Request.IsAjaxRequest())
                throw new HttpException(400, "Method cannot be invoked");

            JsonResult result = null;

            try
            {
                if (_emailBL.Delete(int.Parse(id)) > 0)
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


        // POST : /CannedEmail/PerformAction

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PerformAction(FormCollection formCollection)
        {
            long ddlSelectedValue = Convert.ToInt64(formCollection["ddlActions"]);

            RowStatus action = (SZHPCMS.Common.RowStatus)ddlSelectedValue;

            string[] chkBoxItems = formCollection["chkBoxItem"].Split(',');

            IEnumerable<long> idArray = chkBoxItems.Select(long.Parse);

            _emailBL.UpdateRowStatus(idArray, action);

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
                int result = _docBL.Delete(long.Parse(docID));

                return result > 0 ? (ActionResult)Json(new { Deleted = true }, JsonRequestBehavior.AllowGet) : new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound, "Unable to delete document object with id = " + docID + " . Please contact administrator");

            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest, ex.Message);
            }
        }

    }
}