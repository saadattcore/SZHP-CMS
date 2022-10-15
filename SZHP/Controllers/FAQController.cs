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

    [Authorize]
    public class FAQController : BaseController
    {
        private readonly FAQBH _faqBH;
        public FAQController()
        {
            _faqBH = new FAQBH(new UnitOfWork());
        }

        //
        // GET: /FAQ/
        public ActionResult Index()
        {
           
            //throw new Exception("Object null");

            var faqList = _faqBH.GetActiveFAQ();

            ViewBag.DropDownMenus = Utilities.Utility.GetActionOptions();

            return View(AutoMapperUtil.GetList<FAQModel, FAQViewModel>(faqList));
        }

        //
        // GET: /FAQ/Create
        public ActionResult CreateUpdate(long itemID = 0, string operation = "Create")
        {
            ActionResult viewToReturn = null;

            var faqCategories = _faqBH.GetFAQCategories();
            List<SelectListItem> ddlCategories = new List<SelectListItem>();

            foreach (var item in faqCategories)
            {
                ddlCategories.Add(new SelectListItem() { Text = item.NameEnglish, Value = item.CategoryID.ToString() });
            }

            switch (operation)
            {
                case "Create":
                    FAQViewModel faqVM = new FAQViewModel();
                    faqVM.Categories = ddlCategories;
                    viewToReturn = View(faqVM);

                    break;
                case "Update":
                    var returnNews = _faqBH.GetByID(itemID);
                    var vmToReturn = AutoMapperUtil.Get<FAQModel, FAQViewModel>(returnNews);
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
        // POST: /FAQ/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult CreateUpdate(FAQViewModel model, long itemID = 0, string operation = "Create")
        {
            ActionResult viewToReturn = null;

            try
            {
                if (ModelState.IsValid)
                {
                    FAQModel faqModel = AutoMapperUtil.Get<FAQViewModel, FAQModel>(model);

                    switch (operation)
                    {
                        case "Create":
                            faqModel.CreatedBy = UserID;
                            faqModel = _faqBH.Add(faqModel);
                            break;
                        case "Update":
                            
                            faqModel.Id = itemID;
                            _faqBH.Update(faqModel);

                            break;

                        default:
                            break;
                    }

                    TempData[Constants.MESSAGE] = operation == "Update" ? Constants.RECORD_UPDATED_MESSAGE : Constants.RECORD_ADDED_MESSAGE;

                    viewToReturn = RedirectToAction("Index");
                }
                else
                {
                    var faqCategories = _faqBH.GetFAQCategories();
                    List<SelectListItem> ddlCategories = new List<SelectListItem>();

                    foreach (var item in faqCategories)
                    {
                        ddlCategories.Add(new SelectListItem() { Text = item.NameEnglish, Value = item.CategoryID.ToString() });
                    }
                    model.Categories = ddlCategories;

                    viewToReturn = View(model);
                }
                return viewToReturn;

            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /FAQ/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /FAQ/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
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
                if (_faqBH.Delete(int.Parse(id)) > 0)
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

            _faqBH.UpdateRowStatus(idArray, action);

            TempData[Constants.MESSAGE] = SZHPCMS.Common.Constants.ACTION_APPLIED_MESSAGE;

            return RedirectToAction("Index");

        }
    }
}
