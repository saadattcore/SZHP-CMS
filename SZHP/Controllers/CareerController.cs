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
    public class CareerController : BaseController
    {
        private readonly CareerBH _careerBL;
        private readonly DocumentBH _docBL;
        public CareerController()
        {
            UnitOfWork uow = new UnitOfWork();

            _careerBL = new CareerBH(uow);
            _docBL = new DocumentBH(uow);
        }

        //
        // GET: /Career/
        public ActionResult Index()
        {
            var viewModels = AutoMapperUtil.GetList<CareerJobModel, CareerJobViewModel>(_careerBL.GetActiveJobs());

            ViewBag.DropDownMenus = Utilities.Utility.GetActionOptions();

            return View(viewModels);
        }


        // GET : /Career/CreateUpdate

        [HttpGet]
        public ActionResult CreateUpdate(long itemID = 0, string operation = "Create")
        {
            ActionResult viewToReturn = null;

            switch (operation)
            {
                case "Create":
                    var vmCareer = new CareerJobViewModel();
                    vmCareer.CareerTypeDDL = this.GetCareerTypeDropDown();
                    vmCareer.CareerIndustryDDL = this.GetCareerIndustriesDropDown();

                    viewToReturn = View(vmCareer);
                    break;

                case "Update":
                    var model = _careerBL.GetByID(itemID);
                    var vmToReturn = Translator.TranslateObject<CareerJobModel, CareerJobViewModel>(model);
                    vmToReturn.CareerIndustryDDL = GetCareerIndustriesDropDown();
                    vmToReturn.CareerTypeDDL = GetCareerTypeDropDown();

                    if (model.Documents != null)
                    {
                        vmToReturn.Documents = Translator.TranslateList<DocumentModel, DocumentViewModel>(model.Documents);
                    }                    

                    viewToReturn = View(vmToReturn);
                    break;

                default:
                    var viewModel = new CareerJobViewModel();
                    viewModel.CareerTypeDDL = this.GetCareerTypeDropDown();
                    viewModel.CareerIndustryDDL = this.GetCareerIndustriesDropDown();

                    viewToReturn = View(viewModel);
                    break;
            }

            ViewBag.ItemID = itemID;
            ViewBag.Operation = operation;

            return viewToReturn;
        }

        //
        // POST: /Career/CreateUpdate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUpdate(CareerJobViewModel viewModel, long itemID = 0, string operation = "Create")
        {
            ActionResult viewToReturn = null;

            ViewBag.ItemID = itemID;
            ViewBag.Operation = operation;

            HttpFileCollectionBase filesToUplaod = Request.Files;

            try
            {
                if (ModelState.IsValid)
                {
                    CareerJobModel modelJob = AutoMapperUtil.Get<CareerJobViewModel, CareerJobModel>(viewModel);

                    if (filesToUplaod.Count > 0)
                    {
                        modelJob.Documents = new List<DocumentModel>();

                        for (int i = 0; i < filesToUplaod.Count; i++)
                        {
                            if (!string.IsNullOrEmpty(filesToUplaod[i].FileName) && filesToUplaod[i].ContentLength > 0)
                                modelJob.Documents.Add(new DocumentModel() { FileName = filesToUplaod[i].FileName, Extenstion = filesToUplaod[i].ContentType });
                        }
                    }

                    int result = 0;

                    switch (operation)
                    {
                        case "Create":
                            modelJob = _careerBL.Add(modelJob);
                            break;

                        case "Update":
                            modelJob.CareerJobId = itemID;
                            result = _careerBL.Update(modelJob);
                            break;

                        default:
                            break;
                    }

                    if ((result > 0 || modelJob.CareerJobId > 0) && (filesToUplaod.Count > 0 && modelJob.Documents.Count > 0))
                    {
                        for (int i = 0; i < filesToUplaod.Count; i++)
                        {
                            Utilities.Utility.UploadFile(filesToUplaod[i], Path.Combine(Utilities.Utility.DocumentUploadFolder, "Career"), modelJob.CareerJobId);
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
        // POST: /Career/Delete/5
        [HttpPost]
        public JsonResult Delete(string id)
        {
            if (!Request.IsAjaxRequest())
                throw new HttpException(400, "Method cannot be invoked");

            JsonResult result = null;

            try
            {
                if (_careerBL.Delete(int.Parse(id)) > 0)
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

        //
        // POST: /Career/DeleteApplicant/5
        [HttpPost]
        public ActionResult DeleteApplicant(string id)
        {
            if (!Request.IsAjaxRequest() || string.IsNullOrEmpty(id))
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest, "Wrong id. Value of id =" + id);
            try
            {
                return _careerBL.DeleteApplicant(long.Parse(id)) > 0 ? (ActionResult)Json(AutoMapperUtil.GetList<JobApplicantModel, JobApplicantViewModel>(_careerBL.GetJobApplicants()), JsonRequestBehavior.AllowGet) : new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound, "Unable to object with id = " + id + " . Please contact administrator");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest, ex.Message);
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

            _careerBL.UpdateRowStatus(idArray, action);

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


        // GET : /Career/JobsApplication
        [HttpGet]
        public ActionResult JobsApplicants()
        {
            List<JobApplicantViewModel> modelApplicants = AutoMapperUtil.GetList<JobApplicantModel, JobApplicantViewModel>(_careerBL.GetJobApplicants());

            //ViewBag.DropDownMenus = Utilities.Utility.GetActionOptions();

            return View(modelApplicants);
        }

        // GET : /Career/ApplicantDetails
        public ActionResult ApplicantDetails(long itemID = 0, string operation = "Update")
        {
            JobApplicantModel modelApplicant = _careerBL.GetApplicantByID(itemID);

            var vmToReturn = AutoMapperUtil.Get<JobApplicantModel, JobApplicantViewModel>(modelApplicant);

            vmToReturn.Resume = modelApplicant.Resume != null ? AutoMapperUtil.Get<DocumentModel, DocumentViewModel>(modelApplicant.Resume) : null;

            return View(vmToReturn);

        }

        #region Private Methods

        [NonAction]
        private List<SelectListItem> GetCareerTypeDropDown()
        {
            return _careerBL.GetCareerTypes().Select(x => new SelectListItem() { Text = x.NamEn, Value = x.CareerTypeId.ToString() }).ToList();
        }

        [NonAction]
        private List<SelectListItem> GetCareerIndustriesDropDown()
        {
            return _careerBL.GetCareerIndustries().Select(x => new SelectListItem() { Text = x.NameEn, Value = x.CareerIndustryId.ToString() }).ToList();
        }

        #endregion


        #region Career Industry Region

        //
        // GET: /CareerIndustry/Index
        public ActionResult IndexCareerIndustry()
        {
            var result = _careerBL.GetCareerIndustries();

            ViewBag.DropDownMenus = Utilities.Utility.GetActionOptions();

            return View(Translator.TranslateList<CareerIndustryModel, CareerIndustryViewModel>(result));
        }

        //
        // GET: /CareerIndustry/Create
        public ActionResult CreateUpdateIndustry(long itemID = 0, string operation = "Create")
        {
            ActionResult viewToReturn = null;

            switch (operation)
            {
                case "Create":
                    viewToReturn = View();

                    break;

                case "Update":

                    var model = _careerBL.GetIndustryByID(itemID);

                    var viewModels = Translator.TranslateObject<CareerIndustryModel, CareerIndustryViewModel>(model);

                    viewToReturn = View(viewModels);
                    break;

                default:
                    break;
            }

            ViewBag.ItemID = itemID;
            ViewBag.Operation = operation;

            return viewToReturn;
        }

        //
        // POST: /CareerIndustry/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUpdateIndustry(CareerIndustryViewModel viewModel, long itemID = 0, string operation = "Create")
        {
            ActionResult viewToReturn = null;

            try
            {
                if (ModelState.IsValid)
                {
                    viewModel.CareerIndustryId = itemID;

                    CareerIndustryModel model = AutoMapperUtil.Get<CareerIndustryViewModel, CareerIndustryModel>(viewModel);


                    int result = 0;

                    switch (operation)
                    {
                        case "Create":
                            model.CreatedBy = UserID;
                            model = _careerBL.AddIndutry(model);

                            break;
                        case "Update":

                            model.UpdatedBy = UserID;
                            result = _careerBL.UpdateIndustry(model);

                            break;

                        default:
                            break;
                    }

                    TempData[Constants.MESSAGE] = operation == "Update" ? Constants.RECORD_UPDATED_MESSAGE : Constants.RECORD_ADDED_MESSAGE;

                    viewToReturn = RedirectToAction("IndexCareerIndustry");
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

        // POST: CareerIndustry/Delete/5

        [HttpPost]
        public JsonResult DeleteCareerIndustry(string id)
        {
            if (!Request.IsAjaxRequest() || string.IsNullOrEmpty(id))
                throw new HttpException(400, "Method cannot be invoked");

            JsonResult result = null;

            try
            {
                if (_careerBL.DeleteCareerIndustry(int.Parse(id)) > 0)
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
        public ActionResult PerformActionCareerIndustry(FormCollection formCollection)
        {
            long ddlSelectedValue = Convert.ToInt64(formCollection["ddlActions"]);

            RowStatus action = (SZHPCMS.Common.RowStatus)ddlSelectedValue;

            string[] chkBoxItems = formCollection["chkBoxItem"].Split(',');

            IEnumerable<long> idArray = chkBoxItems.Select(long.Parse);

            _careerBL.UpdateRowStatusIndustry(idArray, action);

            TempData[Constants.MESSAGE] = SZHPCMS.Common.Constants.ACTION_APPLIED_MESSAGE;

            return RedirectToAction("IndexCareerIndustry");

        }

        #endregion

    }
}
