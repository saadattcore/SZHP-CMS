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

namespace SZHPCMS.Controllers
{
    public class GalleryController : BaseController
    {
        private readonly GalleryBH _galleryBL;
        public GalleryController()
        {
            _galleryBL = new GalleryBH(new UnitOfWork());
        }

        //
        // GET: /Gallery/Index
        public ActionResult Index()
        {
            var modelGallries = _galleryBL.GetActiveGalleries();

            ViewBag.DropDownMenus = Utilities.Utility.GetActionOptions();

            return View(AutoMapperUtil.GetList<GalleryModel, GalleryViewModel>(modelGallries));
        }


        //GET : /Gallery/CreateUpdate

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
                    var model = _galleryBL.GetByID(itemID);
                    var viewModel = AutoMapperUtil.Get<GalleryModel, GalleryViewModel>(model);
                    viewModel.ImageName = model.Document != null ? model.Document.FileName : string.Empty;
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
        public ActionResult CreateUpdate(GalleryViewModel viewModel, long itemID = 0, string operation = "Create")
        {
            ActionResult viewToReturn = null;

            HttpPostedFileBase file = Request.Files[0];

            ViewBag.ItemID = itemID;
            ViewBag.Operation = operation;
            try
            {
                if (ModelState.IsValid)
                {
                    GalleryModel model = AutoMapperUtil.Get<GalleryViewModel, GalleryModel>(viewModel);


                    if (file.ContentLength > 0 && !string.IsNullOrEmpty(file.FileName))
                    {
                      //  Utilities.Utility.DeleteFile(Path.Combine(Server.MapPath("~"), "Images", "Gallery", itemID.ToString(), viewModel.ImageName));

                        model.Document = new DocumentModel();

                        model.Document.FileName = file.FileName;
                        model.Document.Extenstion = file.ContentType;
                    }


                    int result = 0;

                    switch (operation)
                    {
                        case "Create":
                            model.CreatedBy = UserID;
                            model = _galleryBL.Add(model);

                            break;
                        case "Update":

                            model.GalleryId = itemID;
                            model.UpdatedBy = UserID;
                            result = _galleryBL.Update(model);

                            break;
                        default:
                            break;
                    }

                    if (file.ContentLength > 0 && !string.IsNullOrEmpty(file.FileName) && (model.GalleryId > 0 || result > 0))
                    {
                        Utilities.Utility.UploadFile(file, Path.Combine(Utilities.Utility.DocumentUploadFolder, "Gallery"),model.GalleryId);
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
                if (_galleryBL.Delete(int.Parse(id)) > 0)
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

            _galleryBL.UpdateRowStatus(idArray, action);

            TempData[Constants.MESSAGE] = SZHPCMS.Common.Constants.ACTION_APPLIED_MESSAGE;

            return RedirectToAction("Index");

        }

    }
}
