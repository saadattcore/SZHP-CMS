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
    public class ProjectController : BaseController
    {
        private readonly ProjectBH _projectBL;
        public ProjectController()
        {
            _projectBL = new ProjectBH(new UnitOfWork());
        }

        #region Project Action Methods
        //
        // GET: /Project/Index
        public ActionResult Index()
        {
            var modelProjects = _projectBL.GetActiveProjects();

            ViewBag.DropDownMenus = Utilities.Utility.GetActionOptions();

            List<ProjectViewModel> vmProjects = Translator.TranslateList<ProjectModel, ProjectViewModel>(modelProjects);

            return View(vmProjects);
        }


        //GET : /Project/CreateUpdate

        [HttpGet]
        
        public ActionResult CreateUpdate(long itemID = 0, string operation = "Create")
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

                    var model = _projectBL.GetByID(itemID);
                    ProjectViewModel viewModel = Translator.TranslateObject<ProjectModel, ProjectViewModel>(model);
                    viewModel.DocumentName = model.Document != null ? model.Document.FileName : string.Empty;
                    viewToReturn = View(viewModel);

                    break;

                default:
                    viewToReturn = View();
                    break;
            }

            return viewToReturn;

        }


        //POST : /Project/CreateUpdate

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult CreateUpdate(ProjectViewModel viewModel, long itemID = 0, string operation = "Create")
        {
            ActionResult viewToReturn = null;

            HttpPostedFileBase file = Request.Files[0];

            ViewBag.ItemID = itemID;
            ViewBag.Operation = operation;
            try
            {
                if (ModelState.IsValid)
                {
                    ProjectModel model = Translator.TranslateObject<ProjectViewModel, ProjectModel>(viewModel);


                    if (file.ContentLength > 0 && !string.IsNullOrEmpty(file.FileName))
                    {
                        // Utilities.Utility.DeleteFile(Path.Combine(Server.MapPath("~"), "Images", "Project", itemID.ToString(), viewModel.ImageName));

                        model.Document = new DocumentModel();

                        model.Document.FileName = file.FileName;
                        model.Document.Extenstion = file.ContentType;
                    }


                    int result = 0;

                    switch (operation)
                    {
                        case "Create":
                            model.CreatedBy = UserID;
                            model = _projectBL.Add(model);

                            break;
                        case "Update":
                            model.ProjectId = itemID;
                            model.UpdatedBy = UserID;
                            result = _projectBL.Update(model);

                            break;
                        default:
                            break;
                    }

                    if (file.ContentLength > 0 && !string.IsNullOrEmpty(file.FileName) && (model.ProjectId > 0 || result > 0))
                    {
                        Utilities.Utility.UploadFile(file, Path.Combine(Utility.DocumentUploadFolder, "Project"), model.ProjectId);
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

        //POST : Delete research study object using ajax operation

        [HttpPost]
        public JsonResult Delete(string id)
        {
            if (!Request.IsAjaxRequest() || string.IsNullOrEmpty(id))
                throw new HttpException(400, "Method cannot be invoked");

            JsonResult result = null;

            try
            {
                if (_projectBL.Delete(int.Parse(id)) > 0)
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
        public ActionResult PerformAction(FormCollection formCollection)
        {
            long ddlSelectedValue = Convert.ToInt64(formCollection["ddlActions"]);

            RowStatus action = (SZHPCMS.Common.RowStatus)ddlSelectedValue;

            string[] chkBoxItems = formCollection["chkBoxItem"].Split(',');

            IEnumerable<long> idArray = chkBoxItems.Select(long.Parse);

            _projectBL.UpdateRowStatus(idArray, action);

            TempData[Constants.MESSAGE] = SZHPCMS.Common.Constants.ACTION_APPLIED_MESSAGE;

            return RedirectToAction("Index");

        } 
        #endregion

        #region Open Data Action Methods
        //
        // GET: /OpenData/Index
        public ActionResult IndexOpenData()
        {
            var modelOpenData = _projectBL.GetActiveOpenData();

            ViewBag.DropDownMenus = Utilities.Utility.GetActionOptions();

            List<OpenDataViewModel> vmProjects = Translator.TranslateList<OpenDataModel, OpenDataViewModel>(modelOpenData);

            return View(vmProjects);
        }


        //GET : /OpenData/CreateUpdate

        [HttpGet]
        public ActionResult CreateUpdateOpenData(long itemID = 0, string operation = "Create")
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

                    var model = _projectBL.GetOpenDataById(itemID);

                    OpenDataViewModel viewModel = Translator.TranslateObject<OpenDataModel, OpenDataViewModel>(model);
                    viewModel.PublicationDate = viewModel.PublicationDate;

                    viewModel.ExcelDocName = model.ExcelDoc != null ? model.ExcelDoc.FileName : string.Empty;
                    viewModel.PDFDocName = model.PDFDoc != null ? model.PDFDoc.FileName : string.Empty;

                    viewToReturn = View(viewModel);

                    break;

                default:
                    viewToReturn = View();
                    break;
            }

            return viewToReturn;

        }


        //POST : /OpenData/CreateUpdate

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUpdateOpenData(OpenDataViewModel viewModel, long itemID = 0, string operation = "Create")
        {
            ActionResult viewToReturn = null;

            //HttpPostedFileBase file = Request.Files[0];

            ViewBag.ItemID = itemID;
            ViewBag.Operation = operation;
            try
            {
                if (ModelState.IsValid)
                {
                    OpenDataModel model = Translator.TranslateObject<OpenDataViewModel, OpenDataModel>(viewModel);

                    if (viewModel.ExcelDoc != null)
                    {
                        if (viewModel.ExcelDoc.ContentLength > 0 && !string.IsNullOrEmpty(viewModel.ExcelDoc.FileName))
                        {
                            model.ExcelDoc = new DocumentModel();

                            model.ExcelDoc.FileName = viewModel.ExcelDoc.FileName;
                            model.ExcelDoc.Extenstion = ".xls/xlsx";
                        }

                    }


                    if (viewModel.PDFDoc != null)
                    {
                        if (viewModel.PDFDoc.ContentLength > 0 && !string.IsNullOrEmpty(viewModel.PDFDoc.FileName))
                        {
                            model.PDFDoc = new DocumentModel();

                            model.PDFDoc.FileName = viewModel.PDFDoc.FileName;
                            model.PDFDoc.Extenstion = viewModel.PDFDoc.ContentType;
                        }

                    }

                    int result = 0;

                    switch (operation)
                    {
                        case "Create":
                            model.CreatedBy = UserID;
                            model = _projectBL.AddOpenData(model);

                            break;
                        case "Update":
                            model.OpenDataId = itemID;
                            model.UpdatedBy = UserID;
                            result = _projectBL.UpdateOpenData(model);

                            break;
                        default:
                            break;
                    }

                    if ((model.OpenDataId > 0 || result > 0))
                    {
                        if (viewModel.PDFDoc != null)
                        {
                            if (viewModel.PDFDoc.ContentLength > 0 && !string.IsNullOrEmpty(viewModel.PDFDoc.FileName))
                            {
                                Utilities.Utility.UploadFile(viewModel.PDFDoc, Path.Combine(Utility.DocumentUploadFolder, "OpenData"), model.OpenDataId);
                            }
                        }

                        if (viewModel.ExcelDoc != null)
                        {
                            if (viewModel.ExcelDoc.ContentLength > 0 && !string.IsNullOrEmpty(viewModel.ExcelDoc.FileName))
                            {
                                Utilities.Utility.UploadFile(viewModel.ExcelDoc, Path.Combine(Utility.DocumentUploadFolder, "OpenData"), model.OpenDataId);
                            }
                        }
                    }


                    TempData[Constants.MESSAGE] = operation == "Update" ? Constants.RECORD_UPDATED_MESSAGE : Constants.RECORD_ADDED_MESSAGE;

                    viewToReturn = RedirectToAction("IndexOpenData");
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
        public JsonResult DeleteOpenData(string id)
        {
            if (!Request.IsAjaxRequest() || string.IsNullOrEmpty(id))
                throw new HttpException(400, "Method cannot be invoked");

            JsonResult result = null;

            try
            {
                if (_projectBL.DeleteOpenData(int.Parse(id)) > 0)
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

        // POST : /OpenData/PerformAction

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PerformActionOpenData(FormCollection formCollection)
        {
            long ddlSelectedValue = Convert.ToInt64(formCollection["ddlActions"]);

            RowStatus action = (SZHPCMS.Common.RowStatus)ddlSelectedValue;

            string[] chkBoxItems = formCollection["chkBoxItem"].Split(',');

            IEnumerable<long> idArray = chkBoxItems.Select(long.Parse);

            _projectBL.UpdateRowStatusOpenData(idArray, action);

            TempData[Constants.MESSAGE] = SZHPCMS.Common.Constants.ACTION_APPLIED_MESSAGE;

            return RedirectToAction("IndexOpenData");

        } 

        #endregion
    }
}