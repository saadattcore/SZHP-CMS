using BusinessLogic.BusinessHandler;
using SZHPCMS.Common;
using DataAccess.CommonRespository;
using DataContract.Implementation;
using SZHPCMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SZHPCMS.Controllers
{
    public class TrainingPlanController : BaseController
    {
        private readonly TrainingProgramBH _trainingBH;
        public TrainingPlanController()
        {
            _trainingBH = new TrainingProgramBH(new UnitOfWork());
        }

        //
        // GET: /TrainingPlan/
        public ActionResult Index()
        {
            var trainingPlansModel = _trainingBH.GetActiveTrainingPlans();

            ViewBag.DropDownMenus = Utilities.Utility.GetActionOptions();

            var trainingPlansVM = AutoMapperUtil.GetList<TrainingPlanModel, TrainingPlanViewModel>(trainingPlansModel);

            return View(trainingPlansVM);
        }

        // GET: /TrainingPlan/Create
        [HttpGet]
        public ActionResult CreateUpdate(long itemID = 0, string operation = "Create")
        {
            ActionResult viewToReturn = null;

            IEnumerable<SelectListItem> ddlTrainingPrograms = GetTraningProgramDDL();

            switch (operation)
            {
                case "Create":
                    TrainingPlanViewModel traningPlanViewModel = new TrainingPlanViewModel();
                    traningPlanViewModel.TrainingPrograms = ddlTrainingPrograms;
                    viewToReturn = View(traningPlanViewModel);

                    break;
                case "Update":
                    var returnedPlans = _trainingBH.GetByID(itemID);
                    var vmToReturn = AutoMapperUtil.Get<TrainingPlanModel, TrainingPlanViewModel>(returnedPlans);
                    vmToReturn.TrainingPrograms = ddlTrainingPrograms;
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
        // POST: /TrainingPlan/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUpdate(TrainingPlanViewModel viewModel, long itemID = 0, string operation = "Create")
        {
            ActionResult viewToReturn = null;

            ViewBag.ItemID = itemID;
            ViewBag.Operation = operation;

            try
            {
                if (ModelState.IsValid)
                {
                    TrainingPlanModel trainingPlanModel = AutoMapperUtil.Get<TrainingPlanViewModel, TrainingPlanModel>(viewModel);


                    switch (operation)
                    {
                        case "Create":
                            trainingPlanModel.CreatedBy = UserID;
                            trainingPlanModel = _trainingBH.Add(trainingPlanModel);
                            break;
                        case "Update":
                            trainingPlanModel.UpdatedBy = UserID;
                            _trainingBH.Update(trainingPlanModel);
                            break;
                        default:
                            break;
                    }

                    TempData[Constants.MESSAGE] = operation == "Update" ? Constants.RECORD_UPDATED_MESSAGE : Constants.RECORD_ADDED_MESSAGE;
                    viewToReturn = RedirectToAction("Index");
                }
                else
                {
                    viewModel.TrainingPrograms = this.GetTraningProgramDDL();

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
        // POST: /TrainingPlan/Delete/5
        [HttpPost]
        public JsonResult Delete(string id)
        {
            if (!Request.IsAjaxRequest() || string.IsNullOrEmpty(id))
                throw new HttpException(400, "Method cannot be invoked");

            JsonResult result = null;

            try
            {
                if (_trainingBH.Delete(int.Parse(id)) > 0)
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

            _trainingBH.UpdateRowStatus(idArray, action);

            TempData[Constants.MESSAGE] = SZHPCMS.Common.Constants.ACTION_APPLIED_MESSAGE;

            return RedirectToAction("Index");

        }



        [NonAction]
        private IEnumerable<SelectListItem> GetTraningProgramDDL()
        {
            List<TraningProgramModel> traningPrograms = _trainingBH.GetTraningPrograms();

            List<SelectListItem> dropDownItems = new List<SelectListItem>();

            foreach (var item in traningPrograms)
            {
                dropDownItems.Add(new SelectListItem() { Text = item.NameEnglish, Value = item.TrainingProgramId.ToString() });
            }

            return dropDownItems;
        }
    }
}
