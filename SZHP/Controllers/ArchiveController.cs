using BusinessLogic.BusinessHandler;
using SZHPCMS.Common;
using DataAccess.CommonRespository;
using DataContract.Implementation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SZHPCMS.Models;
using SZHPCMS.Utilities;

namespace SZHPCMS.Controllers
{
    public class ArchiveController : BaseController
    {
        private readonly ArchiveBH _archiveBL;
        private readonly DocumentBH _documentBL;
        public ArchiveController()
        {
            UnitOfWork uow = new UnitOfWork();

            _archiveBL = new ArchiveBH(uow);
            _documentBL = new DocumentBH(uow);
        }

        #region Form Region
        //
        // GET: /Banner/
        public ActionResult FormIndex()
        {
            var modelBanners = _archiveBL.GetActiveForms();

            ViewBag.DropDownMenus = Utilities.Utility.GetActionOptions();

            return View(Translator.TranslateList<FormModel,FormViewModel>(modelBanners));
        }

        // GET : /Banner/CreateUpdate
        [HttpGet]
        public ActionResult FormCreateUpdate(long itemID = 0, string operation = "Create")
        {
            ActionResult viewToReturn = null;

            var docs = Session[SZHPCMS.Common.Constants.SESSION_FORM_DOCS] as List<FormDocumentViewModel>;

            switch (operation)
            {
                case "Create":

                    FormViewModel vmForm = new FormViewModel();

                    if (docs != null)
                        vmForm.Documents = docs;

                    viewToReturn = View(vmForm);

                    break;
                case "Update":

                    FormModel modelForm = _archiveBL.GetFormByID(itemID);

                    FormViewModel viewModelForm = Translator.TranslateObject<FormModel, FormViewModel>(modelForm);

                    if (modelForm.Documents != null)
                    {
                        if (modelForm.Documents.Count > 0)
                        {
                            viewModelForm.Documents = Translator.TranslateList<FormDocumentModel, FormDocumentViewModel>(modelForm.Documents);
                        }
                    }

                    if (docs != null)
                        viewModelForm.Documents.AddRange(docs);

                    ViewBag.Operation = operation;

                    viewToReturn = View(viewModelForm);

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
        public ActionResult FormCreateUpdate(FormViewModel viewModel, long itemID = 0, string operation = "Create")
        {
            ActionResult viewToReturn = null;
            viewModel.FormId = itemID;

            try
            {
                if (ModelState.IsValid)
                {
                    FormModel modelForm = AutoMapperUtil.Get<FormViewModel, FormModel>(viewModel);

                    List<FormDocumentViewModel> formDocuments = Session[SZHPCMS.Common.Constants.SESSION_FORM_DOCS] as List<FormDocumentViewModel>;

                    if (formDocuments != null)
                    {
                        if (formDocuments.Count > 0)
                        {
                            foreach (var item in formDocuments)
                            {
                                if (item.File != null)
                                {
                                    FormDocumentModel modelFormDoc = new FormDocumentModel();
                                    modelFormDoc.NameAr = item.NameAr;
                                    modelFormDoc.NameEn = item.NameEn;

                                    modelFormDoc.Document = new DocumentModel();
                                    modelFormDoc.Document.FileName = item.File.FileName;
                                    modelFormDoc.Document.Extenstion = item.File.ContentType.Contains("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet") ? "xls/xlsx" : item.File.ContentType;

                                    modelForm.Documents.Add(modelFormDoc);
                                }
                            }
                        }
                    }

                    int result = 0;

                    switch (operation)
                    {
                        case "Create":
                            modelForm.CreatedBy = UserID;
                            modelForm = _archiveBL.AddForm(modelForm);

                            break;
                        case "Update":
                            modelForm.UpdatedBy = UserID;
                            result = _archiveBL.UpdateForm(modelForm);

                            break;

                        default:
                            break;
                    }



                    if ((modelForm.FormId > 0 || result > 0) && (modelForm.Documents.Count > 0))
                    {
                        for (int i = 0; i < formDocuments.Count; i++)
                        {
                            Utilities.Utility.UploadFile(formDocuments[i].File, System.IO.Path.Combine(Utility.DocumentUploadFolder, "Forms"), modelForm.FormId);
                        }
                    }

                    Session.Remove(Constants.SESSION_FORM_DOCS);

                    TempData[Constants.MESSAGE] = operation == "Update" ? Constants.RECORD_UPDATED_MESSAGE : Constants.RECORD_ADDED_MESSAGE;

                    viewToReturn = RedirectToAction("FormIndex");
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
                Session.Remove(Constants.SESSION_FORM_DOCS);
                return View(viewModel);
            }
        }

        // POST: Ajax delete form.

        [HttpPost]
        public JsonResult FormDelete(string id)
        {
            if (!Request.IsAjaxRequest())
                throw new HttpException(400, "Method cannot be invoked");

            JsonResult result = null;

            try
            {
                result = _archiveBL.DeleteForm(int.Parse(id)) > 0 ? Json(new { status = true, message = "item has been deleted" }, JsonRequestBehavior.AllowGet) : Json(new { status = false, message = "object not found" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                result = Json(new { status = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

            return result;
        }


        // POST : /BannerController/PerformAction
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FormPerformAction(FormCollection formCollection)
        {
            long ddlSelectedValue = Convert.ToInt64(formCollection["ddlActions"]);

            RowStatus action = (SZHPCMS.Common.RowStatus)ddlSelectedValue;

            string[] chkBoxItems = formCollection["chkBoxItem"].Split(',');

            IEnumerable<long> idArray = chkBoxItems.Select(long.Parse);

            TempData[Constants.MESSAGE] = SZHPCMS.Common.Constants.ACTION_APPLIED_MESSAGE;

            _archiveBL.UpdateRowStatus(idArray, action, SZHPCMS.Common.Archive.Form);

            return RedirectToAction("FormIndex");

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

        [HttpPost]
        public ActionResult AddFormDocument(FormDocumentViewModel viewModel, string operation = "Create", long itemID = 0, string operationDocForm = "Create")
        {

            switch (operation)
            {

                case "Create":

                    #region Add Form Document

                    if (Session[SZHPCMS.Common.Constants.SESSION_FORM_DOCS] == null)
                    {
                        List<FormDocumentViewModel> docs = new List<FormDocumentViewModel>();

                        docs.Add(viewModel);

                        Session.Add(SZHPCMS.Common.Constants.SESSION_FORM_DOCS, docs);
                    }
                    else
                    {
                        var docs = Session[SZHPCMS.Common.Constants.SESSION_FORM_DOCS] as List<FormDocumentViewModel>;

                        if (docs != null)
                            docs.Add(viewModel);
                    }

                    #endregion

                    break;
                case "Update":

                    #region Update Form Document

                    if (ModelState.IsValid)
                    {
                        var model = Translator.TranslateObject<FormDocumentViewModel, FormDocumentModel>(viewModel);
                        model.FormID = itemID;

                        if (viewModel.File != null)
                        {
                            if (viewModel.File.ContentLength > 0 && !string.IsNullOrEmpty(viewModel.File.FileName))
                            {
                                model.Document = new DocumentModel();

                                model.Document.FileName = viewModel.File.FileName;
                                model.Document.Extenstion = viewModel.File.ContentType;                               

                            }
                        }

                        if (operationDocForm == "Update")
                            model.UpdatedBy = UserID;

                        else
                            model.CreatedBy = UserID;


                        int rowsEffected = _archiveBL.UpdateFormDoc(model, operationDocForm);

                        if (rowsEffected > 0 && viewModel.File != null)
                            if (viewModel.File.ContentLength > 0 && !string.IsNullOrEmpty(viewModel.File.FileName))

                                Utilities.Utility.UploadFile(viewModel.File, System.IO.Path.Combine(Utility.DocumentUploadFolder, "Forms"), itemID);
                    }
                    

                    #endregion

                    break;

                default:
                    break;
            }

            return RedirectToAction("FormCreateUpdate", new { operation = operation, ItemID = itemID });

        }

        /// <summary>
        /// Generate partial view result in edit mode for form document .
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpPost]
        public PartialViewResult GetFormDocPartial(string id, string operation, string formID)
        {
            PartialViewResult partialResult = null;

            if (string.IsNullOrEmpty(id))
            {
                partialResult = PartialView("~/Views/Archive/_ucFormDocument.cshtml", new FormDocumentViewModel());

                partialResult.ViewData.Add("OperationFormDoc", "Create");
            }
            else
            {
                long docID = long.Parse(id);

                var model = _archiveBL.GetFormDoc(docID);

                FormDocumentViewModel viewModel = Translator.TranslateObject<FormDocumentModel, FormDocumentViewModel>(model);

                partialResult = PartialView("~/Views/Archive/_ucFormDocument.cshtml", viewModel);

                partialResult.ViewData.Add("OperationFormDoc", "Update");

            }

            partialResult.ViewData.Add("ItemID", formID);

            partialResult.ViewData.Add("Operation", operation);


            //long docID = long.Parse(id);

            //var model = _archiveBL.GetFormDoc(docID);

            //FormDocumentViewModel viewModel = Translator.TranslateObject<FormDocumentModel, FormDocumentViewModel>(model);

            //var partialView = PartialView("~/Views/Archive/_ucFormDocument.cshtml", viewModel);



            //partialResult.ViewData.Add("OperationFormDoc", "Update");

            return partialResult;


        }



        #endregion

        #region Rule Region
        //
        // GET: /Gallery/Index
        public ActionResult IndexRules()
        {
            var modelRules = _archiveBL.GetActiveRules();

            ViewBag.DropDownMenus = Utilities.Utility.GetActionOptions();

            return View(Translator.TranslateList<RulesAndRegulationModel, RulesAndRegulationViewModel>(modelRules));
        }


        //GET : /Rules/CreateUpdate

        [HttpGet]
        public ActionResult CreateUpdateRule(long itemID = 0, string operation = "Create")
        {
            ViewBag.ItemID = itemID;
            ViewBag.Operation = operation;

            ActionResult viewToReturn = null;

            switch (operation)
            {
                case "Create":
                    viewToReturn = View();

                    break;
                case "Update":
                    var model = _archiveBL.GetRuleById(itemID);
                    var viewModel = Translator.TranslateObject<RulesAndRegulationModel, RulesAndRegulationViewModel>(model);
                    viewModel.DocumentName = model.Document != null ? model.Document.FileName : string.Empty;
                    viewToReturn = View(viewModel);

                    break;

                default:
                    viewToReturn = View();
                    break;
            }

            return viewToReturn;

        }


        //POST : /Rules/CreateUpdate

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUpdateRule(RulesAndRegulationViewModel viewModel, long itemID = 0, string operation = "Create")
        {
            ActionResult viewToReturn = null;

            HttpPostedFileBase file = Request.Files[0];

            ViewBag.ItemID = itemID;
            ViewBag.Operation = operation;
            try
            {
                if (ModelState.IsValid)
                {
                    RulesAndRegulationModel model = Translator.TranslateObject<RulesAndRegulationViewModel, RulesAndRegulationModel>(viewModel);


                    if (file.ContentLength > 0 && !string.IsNullOrEmpty(file.FileName))
                    {
                        //  Utilities.Utility.DeleteFile(Path.Combine(Server.MapPath("~"), "Images", "Rules&Regulation", itemID.ToString()));

                        model.Document = new DocumentModel();

                        model.Document.FileName = file.FileName;
                        model.Document.Extenstion = file.ContentType;
                    }


                    int result = 0;

                    switch (operation)
                    {
                        case "Create":
                            model.CreatedBy = UserID;
                            model = _archiveBL.AddRule(model);

                            break;
                        case "Update":
                            model.RuleId = itemID;
                            model.UpdatedBy = (int?)UserID;
                            result = _archiveBL.UpdateRule(model);

                            break;
                        default:
                            break;
                    }

                    if (model.Document != null && (model.RuleId > 0 || result > 0))
                    {
                        Utilities.Utility.UploadFile(file, Path.Combine(Utility.DocumentUploadFolder, "RulesAndRegulation"), model.RuleId);
                    }

                    TempData[Constants.MESSAGE] = operation == "Update" ? Constants.RECORD_UPDATED_MESSAGE : Constants.RECORD_ADDED_MESSAGE;

                    viewToReturn = RedirectToAction("IndexRules");
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

        //POST : Delete rule object using ajax operation

        [HttpPost]
        public JsonResult DeleteRule(string id)
        {
            if (!Request.IsAjaxRequest() || string.IsNullOrEmpty(id))
                throw new HttpException(400, "Method cannot be invoked");

            JsonResult result = null;

            try
            {
                if (_archiveBL.DeleteRule(int.Parse(id)) > 0)
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

        // POST : /ResearchStudy/PerformAction

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PerformActionRule(FormCollection formCollection)
        {
            long ddlSelectedValue = Convert.ToInt64(formCollection["ddlActions"]);

            RowStatus action = (SZHPCMS.Common.RowStatus)ddlSelectedValue;

            string[] chkBoxItems = formCollection["chkBoxItem"].Split(',');

            IEnumerable<long> idArray = chkBoxItems.Select(long.Parse);

            _archiveBL.UpdateRowStatusRule(idArray, action);

            TempData[Constants.MESSAGE] = SZHPCMS.Common.Constants.ACTION_APPLIED_MESSAGE;

            return RedirectToAction("IndexRules");

        }
        #endregion

        #region Magzine Region
        //
        // GET: /Gallery/Index
        public ActionResult IndexMagzine()
        {
            var modelRules = _archiveBL.GetActiveMagzine();

            ViewBag.DropDownMenus = Utilities.Utility.GetActionOptions();

            return View(Translator.TranslateList<MagzineModel, MagzineViewModel>(modelRules));
        }


        //GET : /Gallery/CreateUpdate

        [HttpGet]
        public ActionResult CreateUpdateMagzine(long itemID = 0, string operation = "Create")
        {
            ViewBag.ItemID = itemID;
            ViewBag.Operation = operation;

            ActionResult viewToReturn = null;

            switch (operation)
            {
                case "Create":
                    viewToReturn = View();

                    break;
                case "Update":
                    var model = _archiveBL.GetMagzineById(itemID);
                    var viewModel = Translator.TranslateObject<MagzineModel, MagzineViewModel>(model);
                    viewModel.FileDocName = model.FileDoc != null ? model.FileDoc.FileName : string.Empty;
                    viewModel.CoverName = model.CoverDoc != null ? model.CoverDoc.FileName : string.Empty;
                    viewToReturn = View(viewModel);

                    break;

                default:
                    viewToReturn = View();
                    break;
            }

            return viewToReturn;

        }


        //POST : /Gallery/CreateUpdate

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUpdateMagzine(MagzineViewModel viewModel, long itemID = 0, string operation = "Create")
        {
            ActionResult viewToReturn = null;

            // HttpPostedFileBase file = Request.Files[0];

            ViewBag.ItemID = itemID;
            ViewBag.Operation = operation;
            try
            {
                if (ModelState.IsValid)
                {
                    MagzineModel model = Translator.TranslateObject<MagzineViewModel, MagzineModel>(viewModel);

                    if (viewModel.FileDoc != null)
                    {
                        if (viewModel.FileDoc.ContentLength > 0 && !string.IsNullOrEmpty(viewModel.FileDoc.FileName))
                        {
                            //  Utilities.Utility.DeleteFile(Path.Combine(Server.MapPath("~"), "Images", "Rules&Regulation", itemID.ToString()));

                            model.FileDoc = new DocumentModel();

                            //   model.FileDoc.FileName = Utilities.Utility.GetFileName(viewModel.FileDoc.FileName);

                            model.FileDoc.FileName = viewModel.FileDoc.FileName;
                            model.FileDoc.Extenstion = viewModel.FileDoc.ContentType;
                        }
                    }


                    if (viewModel.CoverDoc != null)
                    {
                        if (viewModel.CoverDoc.ContentLength > 0 && !string.IsNullOrEmpty(viewModel.CoverDoc.FileName))
                        {
                            //  Utilities.Utility.DeleteFile(Path.Combine(Server.MapPath("~"), "Images", "Rules&Regulation", itemID.ToString()));

                            model.CoverDoc = new DocumentModel();

                            model.CoverDoc.FileName = viewModel.CoverDoc.FileName;
                            model.CoverDoc.Extenstion = viewModel.CoverDoc.ContentType;
                        }
                    }

                    int result = 0;

                    switch (operation)
                    {
                        case "Create":
                            model.CreatedBy = UserID;
                            model = _archiveBL.AddMagzine(model);

                            break;
                        case "Update":

                            model.UpdatedBy = UserID;
                            model.MagzineId = itemID;
                            result = _archiveBL.UpdateMagzine(model);

                            break;
                        default:
                            break;
                    }

                    if ((model.MagzineId > 0 || result > 0))
                    {
                        if (viewModel.FileDoc != null)
                        {
                            if (viewModel.FileDoc.ContentLength > 0 && !string.IsNullOrEmpty(viewModel.FileDoc.FileName))
                                Utilities.Utility.UploadFile(viewModel.FileDoc, Path.Combine(Utility.DocumentUploadFolder, "Magzines"), model.MagzineId);
                        }

                        if (viewModel.CoverDoc != null)
                        {
                            if (viewModel.CoverDoc.ContentLength > 0 && !string.IsNullOrEmpty(viewModel.CoverDoc.FileName))
                                Utilities.Utility.UploadFile(viewModel.CoverDoc, Path.Combine(Utility.DocumentUploadFolder, "Magzines"), model.MagzineId);
                        }


                    }

                    TempData[Constants.MESSAGE] = operation == "Update" ? Constants.RECORD_UPDATED_MESSAGE : Constants.RECORD_ADDED_MESSAGE;

                    viewToReturn = RedirectToAction("IndexMagzine");
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

        //POST : Delete research study object using ajax operation

        [HttpPost]
        public JsonResult DeleteMagzine(string id)
        {
            if (!Request.IsAjaxRequest() || string.IsNullOrEmpty(id))
                throw new HttpException(400, "Method cannot be invoked");

            JsonResult result = null;

            try
            {
                if (_archiveBL.DeleteMagzine(int.Parse(id)) > 0)
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

        // POST : /ResearchStudy/PerformAction

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PerformActionMagzine(FormCollection formCollection)
        {
            long ddlSelectedValue = Convert.ToInt64(formCollection["ddlActions"]);

            RowStatus action = (SZHPCMS.Common.RowStatus)ddlSelectedValue;

            string[] chkBoxItems = formCollection["chkBoxItem"].Split(',');

            IEnumerable<long> idArray = chkBoxItems.Select(long.Parse);

            _archiveBL.UpdateRowStatusMagzine(idArray, action);

            TempData[Constants.MESSAGE] = SZHPCMS.Common.Constants.ACTION_APPLIED_MESSAGE;

            return RedirectToAction("IndexMagzine");

        }
        #endregion

        #region Condition & Requirements Region
        //
        // GET: /Conditions/Indexs
        public ActionResult IndexConditions()
        {
            var mdConditions = _archiveBL.GetConditions();

            ViewBag.DropDownMenus = Utilities.Utility.GetActionOptions();

            return View(Translator.TranslateList<ConditionModel, ConditionViewModel>(mdConditions));
        }


        //GET : /Conditions/CreateUpdateCondition

        [HttpGet]
        public ActionResult CreateUpdateCondition(long itemID = 0, string operation = "Create")
        {
            ViewBag.ItemID = itemID;
            ViewBag.Operation = operation;

            ActionResult viewToReturn = null;

            switch (operation)
            {
                case "Create":
                    viewToReturn = View();

                    break;
                case "Update":

                    var model = _archiveBL.GetCondition(itemID);
                    var viewModel = Translator.TranslateObject<ConditionModel, ConditionViewModel>(model);
                    viewToReturn = View(viewModel);

                    break;

                default:
                    viewToReturn = View();
                    break;
            }

            return viewToReturn;

        }


        //POST : /Conditions/CreateUpdateCondition

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult CreateUpdateCondition(ConditionViewModel viewModel, long itemID = 0, string operation = "Create")
        {
            ActionResult viewToReturn = null;

            ViewBag.ItemID = itemID;
            ViewBag.Operation = operation;

            try
            {
                if (ModelState.IsValid)
                {
                    ConditionModel model = Translator.TranslateObject<ConditionViewModel, ConditionModel>(viewModel);

                    int result = 0;

                    switch (operation)
                    {
                        case "Create":

                            model.CreatedBy = UserID;
                            model = _archiveBL.AddCondition(model);

                            break;
                        case "Update":

                            model.UpdatedBy = UserID;
                            model.ConditionId = itemID;
                            result = _archiveBL.UpdateCondition(model);

                            break;
                        default:
                            break;
                    }

                    TempData[Constants.MESSAGE] = operation == "Update" ? Constants.RECORD_UPDATED_MESSAGE : Constants.RECORD_ADDED_MESSAGE;

                    viewToReturn = RedirectToAction("IndexConditions");
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

        //POST : Delete condition and requirement object using ajax operation

        [HttpPost]
        public JsonResult DeleteCondition(string id)
        {
            if (!Request.IsAjaxRequest() || string.IsNullOrEmpty(id))
                throw new HttpException(400, "Method cannot be invoked");

            JsonResult result = null;

            try
            {
                if (_archiveBL.DeleteCondition(int.Parse(id)) > 0)
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

        // POST : /ResearchStudy/PerformAction

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PerformActionCondition(FormCollection formCollection)
        {
            long ddlSelectedValue = Convert.ToInt64(formCollection["ddlActions"]);

            RowStatus action = (SZHPCMS.Common.RowStatus)ddlSelectedValue;

            string[] chkBoxItems = formCollection["chkBoxItem"].Split(',');

            IEnumerable<long> idArray = chkBoxItems.Select(long.Parse);

            _archiveBL.UpdateRowStatusCondition(idArray, action);

            TempData[Constants.MESSAGE] = SZHPCMS.Common.Constants.ACTION_APPLIED_MESSAGE;

            return RedirectToAction("IndexConditions");

        }
        #endregion
    }
}