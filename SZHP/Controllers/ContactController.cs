using SZHPCMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogic.BusinessHandler;
using DataAccess.CommonRespository;
using SZHPCMS.Utilities;
using DataContract.Implementation;
using SZHPCMS.Common;

namespace SZHPCMS.Controllers
{
    public class ContactController : BaseController
    {
        private readonly ContactBH _contactBL;
        public ContactController()
        {
            _contactBL = new ContactBH(new UnitOfWork());
        }

        #region Contacts To Chairman Region
        //
        // GET: /Contact Chairman/Index
        public ActionResult IndexChairmanContact()
        {
            var modelChairmanContact = _contactBL.GetActiveContactChairman();

            ViewBag.DropDownMenus = Utilities.Utility.GetActionOptions();

            return View(Translator.TranslateList<ContactChairmanModel, ContactChairmanViewModel>(modelChairmanContact));
        }


        //GET : /Contact Chairman/CreateUpdate

        [HttpGet]
        public ActionResult CreateUpdateContactChairman(long itemID = 0, string operation = "Create")
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
                    var model = _contactBL.GetContactChairmanById(itemID);
                    var viewModel = Translator.TranslateObject<ContactChairmanModel, ContactChairmanViewModel>(model);
                    viewToReturn = View(viewModel);
                    break;

                default:
                    viewToReturn = View();
                    break;
            }

            return viewToReturn;

        }
        #endregion

        #region Technical Support Request Region
        //
        // GET: /IndexSupportRequest/Index
        public ActionResult IndexSupportRequest()
        {
            var modelSupport = _contactBL.GetActiveSupportRequests();

            ViewBag.DropDownMenus = Utilities.Utility.GetActionOptions();

            return View(Translator.TranslateList<TechnicalSupportModel, TechnicalSupportViewModel>(modelSupport));
        }


        //GET : /IndexSupportRequest/CreateUpdate

        [HttpGet]
        public ActionResult CreateUpdateSupportRequest(long itemID = 0, string operation = "Create")
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
                    var model = _contactBL.GetSupportRequest(itemID);
                    var viewModel = Translator.TranslateObject<TechnicalSupportModel, TechnicalSupportViewModel>(model);
                    viewToReturn = View(viewModel);
                    break;

                default:
                    viewToReturn = View();
                    break;
            }

            return viewToReturn;

        }
        #endregion

        #region Contact Region
        //
        // GET: /Contact/IndexContact
        public ActionResult IndexContact()
        {
            var modelSupport = _contactBL.GetActiveContacts();

            ViewBag.DropDownMenus = Utilities.Utility.GetActionOptions();

            return View(Translator.TranslateList<ContactModel, ContactViewModel>(modelSupport));
        }


        //GET : /Contact/CreateUpdateContact

        [HttpGet]
        public ActionResult CreateUpdateContact(long itemID = 0, string operation = "Create")
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
                    var model = _contactBL.GetContactById(itemID);
                    var viewModel = Translator.TranslateObject<ContactModel, ContactViewModel>(model);
                    viewToReturn = View(viewModel);
                    break;

                default:
                    viewToReturn = View();
                    break;
            }

            return viewToReturn;

        }

        //
        // POST: /Contact/CreateUpdateContact
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUpdateContact(ContactViewModel viewModel, long itemID = 0, string operation = "Create")
        {
            ActionResult viewToReturn = null;

            try
            {
                if (ModelState.IsValid)
                {
                    ContactModel model = Translator.TranslateObject<ContactViewModel, ContactModel>(viewModel);

                    switch (operation)
                    {
                        case "Create":
                            model = _contactBL.AddContact(model);

                            break;
                        case "Update":
                            model.ContactId = itemID;
                            _contactBL.UpdateContact(model);

                            break;

                        default:
                            break;
                    }

                    TempData[Constants.MESSAGE] = operation == "Update" ? Constants.RECORD_UPDATED_MESSAGE : Constants.RECORD_ADDED_MESSAGE;

                    viewToReturn = RedirectToAction("IndexContact");
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


        // POST: Contact/DeleteContact/5

        [HttpPost]
        public JsonResult DeleteContact(string id)
        {
            if (!Request.IsAjaxRequest() || string.IsNullOrEmpty(id))
                throw new HttpException(400, "Method cannot be invoked");

            JsonResult result = null;

            try
            {
                if (_contactBL.DeleteContact(int.Parse(id)) > 0)
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
        public ActionResult PerformActionContact(FormCollection formCollection)
        {
            long ddlSelectedValue = Convert.ToInt64(formCollection["ddlActions"]);

            RowStatus action = (SZHPCMS.Common.RowStatus)ddlSelectedValue;

            string[] chkBoxItems = formCollection["chkBoxItem"].Split(',');

            IEnumerable<long> idArray = chkBoxItems.Select(long.Parse);

            _contactBL.UpdateRowStatusContact(idArray, action);

            TempData[Constants.MESSAGE] = SZHPCMS.Common.Constants.ACTION_APPLIED_MESSAGE;

            return RedirectToAction("IndexContact");

        } 
        #endregion

        #region Contacts Us Region
        //
        // GET: /Contact/IndexContact
        public ActionResult IndexContactUs()
        {
            var modelContactUs = _contactBL.GetActiveContactUs();

            ViewBag.DropDownMenus = Utilities.Utility.GetActionOptions();

            return View(Translator.TranslateList<ContactUsModel, ContactUsViewModel>(modelContactUs));
        }


        //GET : /Contact Chairman/CreateUpdate

        [HttpGet]
        public ActionResult CreateUpdateContactUs(long itemID = 0, string operation = "Create")
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
                    var model = _contactBL.GetContactUsById(itemID);
                    var viewModel = Translator.TranslateObject<ContactUsModel, ContactUsViewModel>(model);
                    viewToReturn = View(viewModel);
                    break;

                default:
                    viewToReturn = View();
                    break;
            }

            return viewToReturn;

        }

        #endregion
    }
}
