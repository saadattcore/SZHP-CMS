using BusinessLogic.BusinessHandler;
using SZHPCMS.Common;
using DataContract.Implementation;
using DataContract.Interfaces;
using SZHPCMS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SZHPCMS.Controllers
{
    public class SettingController : BaseController
    {
        private readonly SettingBH _settingBL;

        public SettingController()
        {
            _settingBL = new SettingBH();
        }
        //
        // GET: /Setting/
        public ActionResult Index()
        {
            //var listData = iCommon.GetAll();

            return View();
        }

        [HttpGet]
        public ActionResult CreateUpdate()
        {

            var objSiteSettingModel = _settingBL.GetSiteSetting();

            var siteSetting = AutoMapperUtil.Get<SettingModel, SettingViewModel>(objSiteSettingModel);

            return View(siteSetting);
        }

        // POST : To update setting . Setting can never add.
        [HttpPost]
        public ActionResult CreateUpdate(SettingViewModel settingViewModel)
        {
            ActionResult viewToReturn = null;

            if (ModelState.IsValid)
            {
                var objSettingModel = AutoMapperUtil.Get<SettingViewModel, SettingModel>(settingViewModel);

                HttpPostedFileBase logo = Request.Files[0];
                if (logo != null)
                {
                    if (logo.ContentLength > 0)
                    {
                        objSettingModel.Document = new DocumentModel();
                        objSettingModel.Document.FileName = logo.FileName;
                        objSettingModel.Document.Extenstion = logo.ContentType;

                        settingViewModel.DocumentName = logo.FileName;
                    }
                }

                int isUpdated = _settingBL.Update(ref objSettingModel);

                if (isUpdated > 0)
                {
                    HttpContext.Application[SZHPCMS.Common.Constants.SESSION_CMS_TITLE] = settingViewModel.CMSTitleEnglish;

                    if (logo != null && logo.ContentLength > 0)
                    {
                        Utilities.Utility.UploadFile(logo, Path.Combine(Utilities.Utility.DocumentUploadFolder, SZHPCMS.Common.Constants.IMAGES_SETTINGS), objSettingModel.Id);

                    }
                }

                ViewBag.Updated = true;
                settingViewModel.Id = objSettingModel.Id;
                viewToReturn = View(settingViewModel);

            }
            else
            {
                viewToReturn = View(settingViewModel);
            }

            return viewToReturn;
        }
    }
}