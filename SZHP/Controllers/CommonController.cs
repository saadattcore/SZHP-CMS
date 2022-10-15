using BusinessLogic.BusinessHandler;
using SZHPCMS.Common;
using DataAccess.CommonRespository;
using DataContract.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SZHPCMS.Models;
using SZHPCMS.Utilities;

namespace SZHPCMS.Controllers
{
    public class CommonController : BaseController
    {
        private readonly CommonBH _commonBL;

        public CommonController()
        {
            _commonBL = new CommonBH(new UnitOfWork());
        }

        #region Polling Region
        //
        // GET: /Common/
        public ActionResult IndexPolls()
        {
            var modelPolls = _commonBL.GetActiveCallCenterPolls();

            ViewBag.DropDownMenus = Utilities.Utility.GetActionOptions();

            return View(Translator.TranslateList<CallCenterPollModel, CallCenterPollViewModel>(modelPolls));
        }

        // POST: Ajax delete poll.

        [HttpPost]
        public JsonResult Delete(string id)
        {
            if (!Request.IsAjaxRequest())
                throw new HttpException(400, "Method cannot be invoked");

            JsonResult result = null;

            try
            {
                result = _commonBL.Delete(int.Parse(id)) > 0 ? Json(new { status = true, message = "item has been deleted" }, JsonRequestBehavior.AllowGet) : Json(new { status = false, message = "object not found" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                result = Json(new { status = true, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

            return result;
        }


        // POST : /Common/PerformAction
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PerformAction(FormCollection formCollection)
        {
            long ddlSelectedValue = Convert.ToInt64(formCollection["ddlActions"]);

            RowStatus action = (SZHPCMS.Common.RowStatus)ddlSelectedValue;

            string[] chkBoxItems = formCollection["chkBoxItem"].Split(',');

            IEnumerable<long> idArray = chkBoxItems.Select(long.Parse);

            _commonBL.UpdateRowStatus(idArray, action);

            TempData[Constants.MESSAGE] = SZHPCMS.Common.Constants.ACTION_APPLIED_MESSAGE;

            return RedirectToAction("IndexPolls");

        }
        #endregion

        //
        // GET: /Common/
        #region FeedBack
        public ActionResult IndexFeedBack()
        {
            var modelFeedBack = _commonBL.GetFeedBacks();

            ViewBag.DropDownMenus = Utilities.Utility.GetActionOptions();

            return View(Translator.TranslateList<FeedBackModel, FeedBackViewModel>(modelFeedBack));
        }

        // POST: Ajax delete poll.

        [HttpPost]
        public JsonResult DeleteFeedBack(string id)
        {
            if (!Request.IsAjaxRequest())
                throw new HttpException(400, "Method cannot be invoked");

            JsonResult result = null;

            try
            {
                result = _commonBL.DeleteFeedBack(int.Parse(id)) > 0 ? Json(new { status = true, message = "item has been deleted" }, JsonRequestBehavior.AllowGet) : Json(new { status = false, message = "object not found" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                result = Json(new { status = true, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

            return result;
        }


        // POST : /Common/PerformAction
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PerformActionFeedBack(FormCollection formCollection)
        {
            long ddlSelectedValue = Convert.ToInt64(formCollection["ddlActions"]);

            RowStatus action = (SZHPCMS.Common.RowStatus)ddlSelectedValue;

            string[] chkBoxItems = formCollection["chkBoxItem"].Split(',');

            IEnumerable<long> idArray = chkBoxItems.Select(long.Parse);

            _commonBL.UpdateRowStatusFeedBack(idArray, action);

            TempData[Constants.MESSAGE] = SZHPCMS.Common.Constants.ACTION_APPLIED_MESSAGE;

            return RedirectToAction("IndexFeedBack");

        }
        #endregion


        //
        // GET: /Link/Index

        #region Poll Region
        public ActionResult IndexPolling()
        {
            var activePolls = _commonBL.GetActivePolls();

            ViewBag.DropDownMenus = Utilities.Utility.GetActionOptions();

            return View(Utilities.Translator.TranslateList<PollModel, PollViewModel>(activePolls));
        }


        // GET : /Poll/CreateUpdate

        [HttpGet]
        public ActionResult CreateUpdatePoll(long itemID = 0, string operation = "Create")
        {
            ActionResult viewToReturn = null;

            switch (operation)
            {
                case "Create":
                    viewToReturn = View();
                    break;

                case "Update":
                    var modelLinks = _commonBL.GetPollByID(itemID);
                    var vmToReturn = Utilities.Translator.TranslateObject<PollModel, PollViewModel>(modelLinks);

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
        public ActionResult CreateUpdatePoll(PollViewModel viewModel, FormCollection formValues, long itemID = 0, string operation = "Create")
        {
            ActionResult viewToReturn = null;

            ViewBag.ItemID = itemID;
            ViewBag.Operation = operation;

            HttpFileCollectionBase filesToUplaod = Request.Files;

            try
            {
                if (ModelState.IsValid)
                {
                    PollModel modelPoll = Utilities.Translator.TranslateObject<PollViewModel, PollModel>(viewModel);

                    List<PollOptionViewModel> pollOptionVM = new List<PollOptionViewModel>();
                    string[] polls_En = formValues["PollOpt_En"].Split(',');
                    string[] polls_Ar = formValues["PollOpt_Ar"].Split(',');
                    for (var item = 0; item <= polls_En.Count()-1; item++)
                    {
                        var pollOpt = new PollOptionViewModel();
                        pollOpt.Option_Ar = polls_Ar[item];
                        pollOpt.Option_En = polls_En[item];
                        pollOptionVM.Add(pollOpt);
                    }

                    PollOptionModel modelPollopt = Utilities.Translator.TranslateObject<List<PollOptionViewModel>, PollOptionModel>(pollOptionVM);


                    int result = 0;

                    switch (operation)
                    {
                        case "Create":
                            modelPoll.Created_By = UserID;
                            modelPoll = _commonBL.AddPoll(modelPoll);

                            foreach (var item in pollOptionVM)
                            {
                                var pollModeOptions = new PollOptionModel();
                                pollModeOptions.Created_By = UserID;
                                pollModeOptions.Option_Ar = item.Option_Ar;
                                pollModeOptions.Option_En = item.Option_En;
                                pollModeOptions.Poll_Id = modelPoll.Poll_Id;
                                modelPollopt = _commonBL.AddPollOptions(pollModeOptions);
                            } 
                            
                            break;

                        case "Update":
                            modelPoll.Updated_By = UserID;
                            modelPoll.Poll_Id = itemID;
                            result = _commonBL.UpdatePoll(modelPoll);
                            break;

                        default:
                            break;
                    }
                   

                    TempData[Constants.MESSAGE] = operation == "Update" ? Constants.RECORD_UPDATED_MESSAGE : Constants.RECORD_ADDED_MESSAGE;

                    viewToReturn = RedirectToAction("IndexPolling");
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
        public JsonResult DeletePoll(string id)
        {
            if (!Request.IsAjaxRequest() || string.IsNullOrEmpty(id))
                throw new HttpException(400, "Method cannot be invoked");

            JsonResult result = null;

            try
            {
                if (_commonBL.DeletePoll(int.Parse(id)) > 0)
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
        public ActionResult PerformActionPoll(FormCollection formCollection)
        {
            long ddlSelectedValue = Convert.ToInt64(formCollection["ddlActions"]);

            RowStatus action = (SZHPCMS.Common.RowStatus)ddlSelectedValue;

            string[] chkBoxItems = formCollection["chkBoxItem"].Split(',');

            IEnumerable<long> idArray = chkBoxItems.Select(long.Parse);

            _commonBL.UpdateRowStatusPoll(idArray, action);

            TempData[Constants.MESSAGE] = SZHPCMS.Common.Constants.ACTION_APPLIED_MESSAGE;

            return RedirectToAction("IndexPolling");

        }

        #endregion

        #region Polls Option Region
        public ActionResult IndexPollOption(long itemID = 0)
        {
            var activePolls = _commonBL.GetPollsByPoll(itemID);

            ViewBag.DropDownMenus = Utilities.Utility.GetActionOptions();

            var url = HttpContext.Request.RawUrl;
            ViewBag.Url = url;
            return View(Utilities.Translator.TranslateList<PollOptionModel, PollOptionViewModel>(activePolls));
        }


        // GET : /Poll/CreateUpdate

        [HttpGet]
        public ActionResult CreateUpdatePollOption(long itemID = 0, string operation = "Create")
        {
            ActionResult viewToReturn = null;

           
            var polls = _commonBL.GetActivePolls();
            List<SelectListItem> ddlCategories = new List<SelectListItem>();

            foreach (var item in polls)
            {
                ddlCategories.Add(new SelectListItem() { Text = item.Poll_En, Value = item.Poll_Id.ToString() });
            }


            switch (operation)
            {
                case "Create":
                    var pollOptionVM = new PollOptionViewModel();
                    pollOptionVM.Polls = ddlCategories;

                    viewToReturn = View(pollOptionVM);
                    break;

                case "Update":
                    var modelLinks = _commonBL.GetPollOptionByID(itemID);
                    var vmToReturn = AutoMapperUtil.Get<PollOptionModel, PollOptionViewModel>(modelLinks);
                    vmToReturn.Polls = ddlCategories;

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
        public ActionResult CreateUpdatePollOption(PollOptionViewModel viewModel, long itemID = 0, string operation = "Create")
        {
            ActionResult viewToReturn = null;

            ViewBag.ItemID = itemID;
            ViewBag.Operation = operation;

            HttpFileCollectionBase filesToUplaod = Request.Files;

            try
            {
                if (ModelState.IsValid)
                {
                    viewModel.Poll_Option_Id = itemID;
                    PollOptionModel modelPoll = Utilities.Translator.TranslateObject<PollOptionViewModel, PollOptionModel>(viewModel);


                    int result = 0;

                    switch (operation)
                    {
                        case "Create":
                            modelPoll.Created_By = UserID;
                            modelPoll = _commonBL.AddPollOptions(modelPoll);
                            break;

                        case "Update":
                            modelPoll.Updated_By = UserID;
                            modelPoll.Poll_Id = itemID;
                            result = _commonBL.UpdatePollOption(modelPoll);
                            break;

                        default:
                            break;
                    }


                    TempData[Constants.MESSAGE] = operation == "Update" ? Constants.RECORD_UPDATED_MESSAGE : Constants.RECORD_ADDED_MESSAGE;

                    viewToReturn = RedirectToAction("IndexPolling");
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
        public JsonResult DeletePollOption(string id)
        {
            if (!Request.IsAjaxRequest() || string.IsNullOrEmpty(id))
                throw new HttpException(400, "Method cannot be invoked");

            JsonResult result = null;

            try
            {
                if (_commonBL.DeletePollOption(int.Parse(id)) > 0)
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
        public ActionResult PerformActionPollOption(FormCollection formCollection)
        {

            var url = formCollection["Url"];

            long ddlSelectedValue = Convert.ToInt64(formCollection["ddlActions"]);

            RowStatus action = (SZHPCMS.Common.RowStatus)ddlSelectedValue;

            string[] chkBoxItems = formCollection["chkBoxItem"].Split(',');

            IEnumerable<long> idArray = chkBoxItems.Select(long.Parse);

            _commonBL.UpdateRowStatusPollOption(idArray, action);

            TempData[Constants.MESSAGE] = SZHPCMS.Common.Constants.ACTION_APPLIED_MESSAGE;
            ViewBag.ReturnUrl = url;

            //return RedirectToAction("IndexPollOption");
            return Redirect("~" + url);
        }
        #endregion


        [HttpGet]
        public ActionResult PollingStats(long itemID = 0, string operation = "Create")
        {
            var result = _commonBL.GetPollingResults(itemID);
            var translatedResult = Utilities.Translator.TranslateList<VoteStatsModel, VoteStatsViewModel>(result);

            return View(translatedResult);

        }
        public ActionResult HappinessMeterStats()
        {
            var stats = _commonBL.GetHappinessStats();

            return View(Utilities.Translator.TranslateList<HappinessStatsModel, HappinessStatsViewModel>(stats));
        }

    }
}