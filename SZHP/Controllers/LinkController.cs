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
    public class LinkController : BaseController
    {
        private readonly LinkBH _linkBL;
        private readonly DocumentBH _docBL;
        public LinkController()
        {
            UnitOfWork uow = new UnitOfWork();

            _linkBL = new LinkBH(uow);
            _docBL = new DocumentBH(uow);
        }

        //
        // GET: /Link/Index
        public ActionResult Index()
        {
            var activeEvents = _linkBL.GetActiveLinks();

            ViewBag.DropDownMenus = Utilities.Utility.GetActionOptions();

            return View(AutoMapperUtil.GetList<LinkModel, LinkViewModel>(activeEvents));
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
                    var modelLinks = _linkBL.GetByID(itemID);
                    var vmToReturn = AutoMapperUtil.Get<LinkModel, LinkViewModel>(modelLinks);
                    vmToReturn.Documents = modelLinks.Documents.Count > 0 ? AutoMapperUtil.GetList<DocumentModel, DocumentViewModel>(modelLinks.Documents) : null;

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
        public ActionResult CreateUpdate(LinkViewModel viewModel, long itemID = 0, string operation = "Create")
        {
            ActionResult viewToReturn = null;

            ViewBag.ItemID = itemID;
            ViewBag.Operation = operation;

            HttpFileCollectionBase filesToUplaod = Request.Files;

            try
            {
                if (ModelState.IsValid)
                {
                    LinkModel modelLink = AutoMapperUtil.Get<LinkViewModel, LinkModel>(viewModel);

                    if (filesToUplaod.Count > 0)
                    {
                        modelLink.Documents = new List<DocumentModel>();

                        for (int i = 0; i < filesToUplaod.Count; i++)
                        {
                            if (!string.IsNullOrEmpty(filesToUplaod[i].FileName) && filesToUplaod[i].ContentLength > 0)
                                modelLink.Documents.Add(new DocumentModel() { FileName = filesToUplaod[i].FileName, Extenstion = filesToUplaod[i].ContentType });
                        }
                    }

                    int result = 0;

                    switch (operation)
                    {
                        case "Create":
                            modelLink.CreatedBy = UserID;
                            modelLink = _linkBL.Add(modelLink);
                            break;

                        case "Update":
                            modelLink.UpdatedBy = UserID;
                            modelLink.LinkID = itemID;
                            result = _linkBL.Update(modelLink);
                            break;

                        default:
                            break;
                    }

                    if ((result > 0 || modelLink.LinkID > 0) && (filesToUplaod.Count > 0 && modelLink.Documents.Count > 0))
                    {
                        for (int i = 0; i < filesToUplaod.Count; i++)
                        {
                            Utilities.Utility.UploadFile(filesToUplaod[i], Path.Combine(Utilities.Utility.DocumentUploadFolder, "Links"),modelLink.LinkID);
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
            if (!Request.IsAjaxRequest() || string.IsNullOrEmpty(id))
                throw new HttpException(400, "Method cannot be invoked");

            JsonResult result = null;

            try
            {
                if (_linkBL.Delete(int.Parse(id)) > 0)
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

            _linkBL.UpdateRowStatus(idArray, action);

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

                return result > 0 ? Json(new { Deleted = true }, JsonRequestBehavior.AllowGet) : Json(new { Deleted = false }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest, ex.Message);
            }
        }

    }
}
