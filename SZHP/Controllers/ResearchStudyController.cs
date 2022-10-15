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
    public class ResearchStudyController : BaseController
    {
        private readonly ResearchStudyBH _researchStudyBL;
        public ResearchStudyController()
        {
            _researchStudyBL = new ResearchStudyBH(new UnitOfWork());
        }


        //
        // GET: /ResearchStudy/
        public ActionResult Index() 
        {
            var modelResearchStudies = _researchStudyBL.GetActiveResearchStudies();

            ViewBag.DropDownMenus = Utilities.Utility.GetActionOptions();

            return View(AutoMapperUtil.GetList<ResearchStudyModel, ResearchStudyViewModel>(modelResearchStudies));
        }

        //GET : /ResearchStudy/CreateUpdate

        [HttpGet]
        public ActionResult CreateUpdate(long itemID = 0, string operation = "Create")
        {
            ActionResult viewToReturn = null;

            List<SelectListItem> ddlCategories = this.GetCategoryDropDown();

            switch (operation)
            {
                case "Create":

                    ResearchStudyViewModel viewModel = new ResearchStudyViewModel();

                    viewModel.Categories = ddlCategories;

                    viewToReturn = View(viewModel);

                    break;

                case "Update":

                    var dbResearchStudies = _researchStudyBL.GetByID(itemID);

                    var vmToReturn = AutoMapperUtil.Get<ResearchStudyModel, ResearchStudyViewModel>(dbResearchStudies);

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


        //POST : /ResearchStudy/CreateUpdate

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult CreateUpdate(ResearchStudyViewModel viewModel, string operation = "Create")
        {
            ActionResult viewToReturn = null;

            try
            {
                if (ModelState.IsValid)
                {
                    ResearchStudyModel model = AutoMapperUtil.Get<ResearchStudyViewModel, ResearchStudyModel>(viewModel);

                    switch (operation)
                    {
                        case "Create":
                            model.CreatedBy = UserID;
                            model = _researchStudyBL.Add(model);
                            break;
                        case "Update":
                            model.UpdatedBy = UserID;
                            _researchStudyBL.Update(model);
                            break;
                        default:
                            break;
                    }

                    TempData[Constants.MESSAGE] = operation == "Update" ? Constants.RECORD_UPDATED_MESSAGE : Constants.RECORD_ADDED_MESSAGE;

                    viewToReturn = RedirectToAction("Index");
                }
                else
                {
                    viewModel.Categories = this.GetCategoryDropDown();

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
                if (_researchStudyBL.Delete(int.Parse(id)) > 0)
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

            _researchStudyBL.UpdateRowStatus(idArray, action);

            TempData[Constants.MESSAGE] = SZHPCMS.Common.Constants.ACTION_APPLIED_MESSAGE;

            return RedirectToAction("Index");

        }

        [NonAction]
        private List<SelectListItem> GetCategoryDropDown()
        {
            var researchStudyCategories = _researchStudyBL.GetCategories();

            List<SelectListItem> ddlCategories = new List<SelectListItem>();

            foreach (var item in researchStudyCategories)
            {
                ddlCategories.Add(new SelectListItem() { Text = item.NameEn, Value = item.ResearchStudyCategoryId.ToString() });
            }

            return ddlCategories;
        }
    }
}