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
    public class EventController : BaseController
    {
        private readonly EventBH _eventBL;
        private readonly DocumentBH _docBL;
        public EventController()
        {
            UnitOfWork uow = new UnitOfWork();

            _eventBL = new EventBH(uow);
            _docBL = new DocumentBH(uow);
        }

        //
        // GET: /Event/
        public ActionResult Index()
        {

            var activeEvents = _eventBL.GetActiveEvents();
            ViewBag.DropDownMenus = Utilities.Utility.GetActionOptions();

            return View(Translator.TranslateList<EventModel, EventViewModel>(activeEvents));
        }


        [HttpGet]
        public ActionResult CreateUpdate(long itemID = 0, string operation = "Create")
        {
            ActionResult viewToReturn = null;
            IEnumerable<SelectListItem> dropDownEventTypes = GetEventTypeDropDown();

            switch (operation)
            {
                case "Create":

                    EventViewModel eventViewModel = new EventViewModel();
                    eventViewModel.EventTypes = dropDownEventTypes;
                    eventViewModel.StartDate = DateTime.Now;
                    eventViewModel.EndDate = DateTime.Now;
                    viewToReturn = View(eventViewModel);

                    break;
                case "Update":

                    var returnedEvents = _eventBL.GetByID(itemID);
                    var vmToReturn = Translator.TranslateObject<EventModel, EventViewModel>(returnedEvents);

                    if (returnedEvents.Documents != null)
                        vmToReturn.Documents = Translator.TranslateList<DocumentModel, DocumentViewModel>(returnedEvents.Documents);

                    vmToReturn.EventTypes = dropDownEventTypes;
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
        // POST: /Event/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUpdate(EventViewModel viewModel, long itemID = 0, string operation = "Create")
        {
            ActionResult viewToReturn = null;

            ViewBag.ItemID = itemID;
            ViewBag.Operation = operation;

            if (DateTime.Compare(viewModel.EndDate, viewModel.StartDate) < 0)
            {
                ViewBag.ErrorMessage = "End date cannote be earlier then start date";

                viewModel.EventTypes = GetEventTypeDropDown();

                return View(viewModel);
            }

            HttpFileCollectionBase filesToUplaod = Request.Files;

            try
            {
                if (ModelState.IsValid)
                {
                    EventModel eventModel = Translator.TranslateObject<EventViewModel, EventModel>(viewModel);

                    if (filesToUplaod.Count > 0)
                    {
                        eventModel.Documents = new List<DocumentModel>();

                        for (int i = 0; i < filesToUplaod.Count; i++)
                        {
                            if (!(string.IsNullOrEmpty(filesToUplaod[i].FileName) && filesToUplaod[i].ContentLength == 0))
                                eventModel.Documents.Add(new DocumentModel() { FileName = filesToUplaod[i].FileName, Extenstion = filesToUplaod[i].ContentType });
                        }
                    }

                    int result = 0;

                    switch (operation)
                    {
                        case "Create":
                            eventModel.CreatedBy = UserID;
                            eventModel = _eventBL.Add(eventModel);

                            break;

                        case "Update":
                            eventModel.UpdatedBy = UserID;
                            eventModel.Id = itemID;
                            result = _eventBL.Update(eventModel);

                            break;
                        default:
                            break;
                    }

                    if ((result == 0 || eventModel.Id > 0) && filesToUplaod.Count > 0 && eventModel.Documents.Count > 0)
                    {
                        for (int i = 0; i < filesToUplaod.Count; i++)
                        {
                            Utilities.Utility.UploadFile(filesToUplaod[i], Path.Combine(Utility.DocumentUploadFolder, "Events"), eventModel.Id);
                        }

                    }

                    TempData[Constants.MESSAGE] = operation == "Update" ? Constants.RECORD_UPDATED_MESSAGE : Constants.RECORD_ADDED_MESSAGE;

                    viewToReturn = RedirectToAction("Index");
                }
                else
                {

                    viewModel.EventTypes = GetEventTypeDropDown();

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
        // GET: /Event/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Event/Edit/5
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

        //
        // POST: /Event/Delete/5
        [HttpPost]
        public JsonResult Delete(string id)
        {
            if (!Request.IsAjaxRequest() || string.IsNullOrEmpty(id))
                throw new HttpException(400, "Method cannot be invoked");

            JsonResult result = null;

            try
            {
                if (_eventBL.Delete(int.Parse(id)) > 0)
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

            _eventBL.UpdateRowStatus(idArray, action);

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



        #region Controller Private Methods
        private IEnumerable<SelectListItem> GetEventTypeDropDown()
        {
            List<EventTypeViewModel> eventTypes = AutoMapperUtil.GetList<EventTypeModel, EventTypeViewModel>(_eventBL.GetActiveEventTypes());
            List<SelectListItem> ddlSelect = new List<SelectListItem>();

            foreach (var item in eventTypes)
            {
                ddlSelect.Add(new SelectListItem() { Text = item.NameEn, Value = item.EventTypeID.ToString() });
            }

            return ddlSelect;
        }

        #endregion
    }
}
