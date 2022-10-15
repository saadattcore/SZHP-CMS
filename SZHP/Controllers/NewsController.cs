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
    public class NewsController : BaseController
    {
        private readonly NewsBH _newsBH;
        private readonly DocumentBH _docBL;
        public NewsController()
        {
            UnitOfWork uow = new UnitOfWork();

            _newsBH = new NewsBH(uow);
            _docBL = new DocumentBH(uow);
        }
        //
        // GET: /News/
        public ActionResult Index()
        {
            var result = _newsBH.GetActiveNews();

            ViewBag.DropDownMenus = Utilities.Utility.GetActionOptions();

            return View(Translator.TranslateList<NewsModel, NewsViewModel>(result));
        }

        //
        // GET: /News/Create
        public ActionResult CreateUpdate(long itemID = 0, string operation = "Create")
        {
            ActionResult viewToReturn = null;
            var ddlCategories = GetNewsCategories();

            switch (operation)
            {
                case "Create":

                    NewsViewModel newsVM = new NewsViewModel();
                    newsVM.Categories = ddlCategories;
                    viewToReturn = View(newsVM);
                    break;

                case "Update":

                    var returnNews = _newsBH.GetByID(itemID);

                    var vmToReturn = Translator.TranslateObject<NewsModel, NewsViewModel>(returnNews);

                    if (returnNews.Documents != null)
                    {
                        if (returnNews.Documents.Count > 0)
                        {
                            vmToReturn.Documents = Translator.TranslateList<DocumentModel, DocumentViewModel>(returnNews.Documents);
                        }
                    }

                    vmToReturn.Categories = ddlCategories;
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
        // POST: /News/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult CreateUpdate(NewsViewModel viewModel, long itemID = 0, string operation = "Create")
        {
            ActionResult viewToReturn = null;

            try
            {
                if (ModelState.IsValid)
                {
                    NewsModel modelNews = Translator.TranslateObject<NewsViewModel, NewsModel>(viewModel);

                    HttpFileCollectionBase postedFiles = Request.Files;

                    if (postedFiles.Count > 0)
                    {
                        modelNews.Documents = new List<DocumentModel>();

                        for (int i = 0; i < postedFiles.Count; i++)
                        {
                            if (!string.IsNullOrEmpty(postedFiles[i].FileName) && postedFiles[i].ContentLength > 0)
                            {
                                modelNews.Documents.Add(new DocumentModel() { FileName = postedFiles[i].FileName, Extenstion = postedFiles[i].ContentType });
                            }
                        }
                    }


                    int rowsEffected = 0;
                    switch (operation)
                    {
                        case "Create":

                            modelNews.CreatedBy = UserID;
                            modelNews = _newsBH.Add(modelNews);
                            break;
                        case "Update":

                            modelNews.UpdatedBy = UserID;
                            modelNews.Id = itemID;
                            rowsEffected = _newsBH.Update(modelNews);

                            break;

                        default:
                            break;
                    }

                    if ((rowsEffected > 0 || modelNews.Id > 0) && (postedFiles.Count > 0))
                    {
                        for (int i = 0; i < postedFiles.Count; i++)
                        {
                            if (!string.IsNullOrEmpty(postedFiles[i].FileName) && postedFiles[i].ContentLength > 0)
                                Utilities.Utility.UploadFile(postedFiles[i], Path.Combine(Utility.DocumentUploadFolder, "News"), modelNews.Id);
                        }
                    }

                    TempData[Constants.MESSAGE] = operation == "Update" ? Constants.RECORD_UPDATED_MESSAGE : Constants.RECORD_ADDED_MESSAGE;

                    viewToReturn = RedirectToAction("Index");
                }
                else
                {
                    viewModel.Categories = GetNewsCategories();
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

        // POST: News/Delete/5

        [HttpPost]
        public JsonResult Delete(string id)
        {
            if (!Request.IsAjaxRequest() || string.IsNullOrEmpty(id))
                throw new HttpException(400, "Method cannot be invoked");

            JsonResult result = null;

            try
            {
                if (_newsBH.Delete(int.Parse(id)) > 0)
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

            _newsBH.UpdateRowStatus(idArray, action);

            TempData[Constants.MESSAGE] = SZHPCMS.Common.Constants.ACTION_APPLIED_MESSAGE;

            return RedirectToAction("Index");

        }

        // POST : Delete image using ajax call.
        [HttpPost]
        public ActionResult RemoveDocument(string docID)
        {
            if (!Request.IsAjaxRequest() || string.IsNullOrEmpty(docID))
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            ActionResult resultView = null;

            try
            {
                int result = _docBL.Delete(long.Parse(docID));

                if (result > 0)
                    resultView = Json(new { Deleted = true }, JsonRequestBehavior.AllowGet);
                else
                    resultView = Json(new { Deleted = false }, JsonRequestBehavior.AllowGet);

                return resultView;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        [NonAction]
        private List<SelectListItem> GetNewsCategories()
        {

            var newsCategories = _newsBH.GetNewsCategories();
            List<SelectListItem> ddlCategories = new List<SelectListItem>();

            foreach (var item in newsCategories)
            {
                ddlCategories.Add(new SelectListItem() { Text = item.NameEnglish, Value = item.CategoryID.ToString() });
            }
            return ddlCategories;
        }



    }
}
