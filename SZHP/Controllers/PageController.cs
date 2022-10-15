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
using System.IO;
using System.Net;

namespace SZHPCMS.Controllers
{
    public class PageController : BaseController
    {
        private readonly PageBH _pageBH;
        private readonly MenuBH _menuBH;
        public PageController()
        {
            UnitOfWork uow = new UnitOfWork();
            _pageBH = new PageBH(uow);
            _menuBH = new MenuBH(uow);
        }

        //
        // GET: /Page/
        [HttpGet]
        public ActionResult Index()
        {
            List<PageViewModel> pages = new List<PageViewModel>();

            ViewBag.DropDownMenus = Utilities.Utility.GetActionOptions();

            pages = AutoMapperUtil.GetList<PageModel, PageViewModel>(_pageBH.GetActivePages());
            return View(pages);
        }

        //

        //
        // GET: /Page/Create
        [HttpGet]
        public ActionResult CreateUpdate(long itemID = 0, string operation = "Create")
        {
            ActionResult viewToReturn = null;

            ViewBag.Menus = _menuBH.GetActiveMenus();
            var ddlActivePages = this.GetActivePagesDDL(itemID);

            switch (operation)
            {
                case "Create":
                    PageViewModel newsVM = new PageViewModel();
                    newsVM.DropDownPages = ddlActivePages;
                    viewToReturn = View(newsVM);

                    break;
                case "Update":
                    var returnNews = _pageBH.GetByID(itemID);
                    var vmToReturn = AutoMapperUtil.Get<PageModel, PageViewModel>(returnNews);

                    vmToReturn.DropDownPages = ddlActivePages;
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
        // POST: /Page/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult CreateUpdate(PageViewModel viewModel, FormCollection formValues, long itemID = 0, string operation = "Create")
        {
            //checkBoxMenu

            ActionResult viewToReturn = null;

            List<long> pageMenuIDs = null;

            if (!string.IsNullOrEmpty(formValues["checkBoxMenu"]))
            {
                string[] menuIds = formValues["checkBoxMenu"].Split(',');
                pageMenuIDs = menuIds.Select(long.Parse).ToList();
            }

            try
            {
                if (ModelState.IsValid)
                {
                    PageModel modelPage = AutoMapperUtil.Get<PageViewModel, PageModel>(viewModel);
                    modelPage.PageMenus = pageMenuIDs;


                    int result = 0;
                    switch (operation)
                    {
                        case "Create":
                            modelPage.Created_By = UserID;
                            modelPage = _pageBH.Add(modelPage);
                            break;
                        case "Update":

                            modelPage.Page_Id = itemID;
                            modelPage.Updated_By = UserID;
                            result = _pageBH.Update(modelPage);

                            break;
                        default:
                            break;
                    }

                    viewToReturn = RedirectToAction("Index");

                }
                else
                {
                    viewModel.DropDownPages = this.GetActivePagesDDL(itemID);
                    ViewBag.Menus = _menuBH.GetActiveMenus();
                    viewToReturn = View(viewModel);
                }

                TempData[Constants.MESSAGE] = operation == "Update" ? Constants.RECORD_UPDATED_MESSAGE : Constants.RECORD_ADDED_MESSAGE;


                return viewToReturn;
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public JsonResult Delete(string id)
        {
            if (!Request.IsAjaxRequest() || string.IsNullOrEmpty(id))
                throw new HttpException(400, "Method cannot be invoked");

            JsonResult result = null;

            try
            {
                if (_pageBH.Delete(int.Parse(id)) > 0)
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

            _pageBH.UpdateRowStatus(idArray, action);

            TempData[Constants.MESSAGE] = SZHPCMS.Common.Constants.ACTION_APPLIED_MESSAGE;

            return RedirectToAction("Index");
        }

        [NonAction]
        private List<SelectListItem> GetActivePagesDDL(long pageToExclude)
        {
            var activePages = _pageBH.GetActivePages(pageToExclude);
            List<SelectListItem> ddlActivePages = new List<SelectListItem>();

            foreach (var item in activePages)
            {
                ddlActivePages.Add(new SelectListItem() { Text = item.Page_Name_En, Value = item.Page_Id.ToString() });
            }
            return ddlActivePages;
        }

        public void uploadnow(HttpPostedFileWrapper upload, FormCollection keyValues)
        {
            if (upload != null)
            {

                Utilities.Utility.UploadFile(upload, Path.Combine(Utilities.Utility.DocumentUploadFolder, "Page"));

                               

                //using (WebClient client = new WebClient())
                //{

                //    try
                //    {
                //        client.Credentials = new NetworkCredential("szhp_web_user", "szhp1q2w3e");

                //        byte[] imageData = null;

                //        using (BinaryReader br = new BinaryReader(upload.InputStream))
                //        {
                //            imageData = br.ReadBytes((int)upload.InputStream.Length);
                //        }

                //        client.UploadData("ftp://134.213.218.153/admin/Rpt/" + upload.FileName, imageData);
                //    }
                //    catch (Exception)
                //    {
                        
                //        throw;
                //    }                 
                //}

            }
        }
        public ActionResult uploadPartial()
        {
            try
            {
                var appData = Path.Combine(Utilities.Utility.DocumentUploadFolder, "Page");

                IEnumerable<string> files = Directory.GetFiles(appData).Select(x => "/Documents/Page/" + Path.GetFileName(x));


            //FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create("ftp://134.213.218.153/admin/Rpt/");
            //request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            //request.Credentials = new NetworkCredential("szhp_web_user", "szhp1q2w3e");
            //request.UsePassive = true;
            //request.UseBinary = true;
            //request.KeepAlive = false;

            //List<string> returnValue = new List<string>();
            //string[] list = null;

            //using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            //using (StreamReader reader = new StreamReader(response.GetResponseStream())) 
            
            //{
            //    list = reader.ReadToEnd().Split(new string[] { "\r\n"},StringSplitOptions.RemoveEmptyEntries);
            //}

            //string str; 

            //for (int i = 0; i < list.Length; i++)
            //{
            //    str = list[i];
            //}


            //    return null;

            return View("~/Views/Page/_uploadPartial.cshtml", files);

          
            }
            catch (Exception)
            {
                return View("~/Views/Page/_uploadPartial.cshtml", new List<string>());
            }


        }
    }
}
