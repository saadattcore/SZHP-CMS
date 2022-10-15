using BusinessLogic.BusinessHandler;
using DataAccess.CommonRespository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SZHPCMS.Models;
using BusinessLogic.BusinessHandler;
using DataContract.Implementation;
using SZHPCMS.Utilities;

namespace SZHPCMS.Controllers
{
    public class ApplicantController : BaseController
    {
        private readonly ApplicantBH _applicantBL;

        public ApplicantController()
        {
            UnitOfWork uow = new UnitOfWork();

            _applicantBL = new ApplicantBH(uow);
        }

        [HttpGet]
        public ActionResult ApplicantCategoryIndex()
        {
            var modelAppCategories = _applicantBL.GetActiveApplicantCategory();

            var viewModelAppCategories = Translator.TranslateList<ApplicantCategoryModel, ApplicantCategoryViewModel>(modelAppCategories);

            ViewBag.DropDownMenus = Utilities.Utility.GetActionOptions();

            return View(viewModelAppCategories);

        }

        [HttpGet]
        public ActionResult CreateApplicant()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateApplicant(FormCollection keyValues)
        {
            string stCounter = keyValues["hfCounter"];
            int counter = 0;

            if (!string.IsNullOrEmpty(stCounter))
                int.TryParse(stCounter, out counter);


            ApplicantCategoryModel rootNode = new ApplicantCategoryModel();
            rootNode.Category_Name_Ar = keyValues["CategoryNameAr"];
            rootNode.Category_Name_En = keyValues["CategoryNameEn"];
            rootNode.Updated_By = UserID;

            rootNode.ApplicantCategoryModels = new List<ApplicantCategoryModel>();

            rootNode.ApplicantCategoryModels.Add(new ApplicantCategoryModel() { Category_Name_Ar = keyValues["SubCategoryNameAr_1"], Category_Name_En = keyValues["SubCategoryNameEn_1"], Header_Description_Ar = keyValues["SubCategoryHeaderAr_1"], Header_Description_En = keyValues["SubCategoryHeaderEn_1"] });

            for (int i = 2; i <= counter; i++)
            {
                ApplicantCategoryModel appCatVM = new ApplicantCategoryModel();

                appCatVM.Category_Name_Ar = keyValues["SubCategoryNameAr_" + i];
                appCatVM.Category_Name_En = keyValues["SubCategoryNameEn_" + i];

                appCatVM.Header_Description_Ar = keyValues["SubCategoryHeaderAr_" + i];
                appCatVM.Header_Description_En = keyValues["SubCategoryHeaderEn_" + i];

                appCatVM.Updated_By = UserID;

                rootNode.ApplicantCategoryModels.Add(appCatVM);

            }

            try
            {
                _applicantBL.AddApplicantCategory(rootNode);
            }
            catch (Exception)
            {
                throw;
            }

            return View();
        }

        [HttpGet]
        public ActionResult ContentCategoryIndex()
        {
            var modelContentCategories = _applicantBL.GetActiveApplicantContentCategory();

            var viewModelContentCategories = Translator.TranslateList<ApplicantContentCategoryModel, ApplicantContentCategoryViewModel>(modelContentCategories);

            ViewBag.DropDownMenus = Utilities.Utility.GetActionOptions();

            return View(viewModelContentCategories);
        }

        [HttpGet]
        public ActionResult CreateContentCategory()
        {
            ViewBag.DropDown = GetApplicantCategoryDropDown();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateContentCategory(FormCollection keyValues)
        {
            string stCounter = keyValues["hfCounter"];
            string stCategoryID = keyValues["ddlCategory"];

            int counter = 0;
            long appCategoryId = 0;

            if (!string.IsNullOrEmpty(stCounter))
                int.TryParse(stCounter, out counter);

            if (!string.IsNullOrEmpty(keyValues["ddlCategory"]))
                long.TryParse(stCategoryID, out appCategoryId);

            List<ApplicantContentCategoryModel> contentCategoryModels = new List<ApplicantContentCategoryModel>();

            contentCategoryModels.Add(new ApplicantContentCategoryModel() { Applicant_Category = appCategoryId, Title_Ar = keyValues["ContentCategoryNameAr_1"], Title_En = keyValues["ContentCategoryNameEn_1"], Description_Ar = keyValues["ContentCategoryDescAr_1"], Description_En = keyValues["ContentCategoryDescEn_1"], Created_By = UserID });


            for (int i = 2; i <= counter; i++)
            {
                ApplicantContentCategoryModel contentCategoryModel = new ApplicantContentCategoryModel();

                contentCategoryModel.Applicant_Category = appCategoryId;

                contentCategoryModel.Title_Ar = keyValues["ContentCategoryNameAr_" + i];
                contentCategoryModel.Title_En = keyValues["ContentCategoryNameEn_" + i];

                contentCategoryModel.Description_Ar = keyValues["ContentCategoryDescAr_" + i];
                contentCategoryModel.Description_En = keyValues["ContentCategoryDescEn_" + i];

                contentCategoryModel.Created_By = UserID;

                contentCategoryModels.Add(contentCategoryModel);

            }

            try
            {
                _applicantBL.AddContentCategoryRange(contentCategoryModels);
            }
            catch (Exception)
            {
                throw;
            }

            return View();

        }

        [HttpGet]
        public ActionResult ContentSubCategoryIndex()
        {
            var models = _applicantBL.GetActiveApplicantContentSubCategory();

            var viewModels = Translator.TranslateList<ApplicantContentSubCategoryModel, ApplicantContentSubCategoryViewModel>(models);

            ViewBag.DropDownMenus = Utilities.Utility.GetActionOptions();

            return View(viewModels);

        }


        [HttpGet]
        public ActionResult CreateContentSubCategory()
        {

            ViewBag.ddlCategory = GetContentCategoryDropDown();

            return View();
        }

        [HttpPost]
        public ActionResult CreateContentSubCategory(FormCollection keyValues)
        {

            string stCounter = keyValues["hfCounter"];
            string stCategoryID = keyValues["ddlCategory"];

            int counter = 0;
            long appCategoryId = 0;

            if (!string.IsNullOrEmpty(stCounter))
                int.TryParse(stCounter, out counter);

            if (!string.IsNullOrEmpty(keyValues["ddlCategory"]))
                long.TryParse(stCategoryID, out appCategoryId);

            List<ApplicantContentSubCategoryModel> contentSubCategoryModels = new List<ApplicantContentSubCategoryModel>();

            contentSubCategoryModels.Add(new ApplicantContentSubCategoryModel() { Applicant_Content_Id = appCategoryId, Title_Ar = keyValues["ContentSubCategoryNameAr_1"], Title_En = keyValues["ContentSubCategoryNameEn_1"], Description_Ar = keyValues["ContentSubCategoryDescAr_1"], Description_En = keyValues["ContentSubCategoryDescEn_1"], Created_By = UserID });


            for (int i = 2; i <= counter; i++)
            {
                ApplicantContentSubCategoryModel contentCategoryModel = new ApplicantContentSubCategoryModel();

                contentCategoryModel.Applicant_Content_Id = appCategoryId;

                contentCategoryModel.Title_Ar = keyValues["ContentSubCategoryNameAr_" + i];
                contentCategoryModel.Title_En = keyValues["ContentSubCategoryNameEn_" + i];

                contentCategoryModel.Description_Ar = keyValues["ContentSubCategoryDescAr_" + i];
                contentCategoryModel.Description_En = keyValues["ContentSubCategoryDescEn_" + i];

                contentCategoryModel.Created_By = UserID;

                contentSubCategoryModels.Add(contentCategoryModel);

            }

            try
            {
                _applicantBL.AddContentSubCategoryRange(contentSubCategoryModels);
            }
            catch (Exception)
            {
                throw;
            }

            return View();


        }

        [HttpGet]
        public ActionResult CreateUpdateCityDescription()
        {
            ViewBag.DropDown = GetApplicantCategoryDropDown();
            ViewBag.CityDropDown = GetCities();

            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult CreateUpdateCityDescription(FormCollection keyValues)
        {
            string stCity = keyValues["ddlCity"];
            string stCategoryID = keyValues["ddlCategory"];

            int cityID = 0;
            long appCategoryId = 0;

            if (!string.IsNullOrEmpty(stCity))
                int.TryParse(stCity, out cityID);

            if (!string.IsNullOrEmpty(keyValues["ddlCategory"]))
                long.TryParse(stCategoryID, out appCategoryId);

            CityDescriptionModel cityModel = new CityDescriptionModel();

            cityModel.City_Id = cityID;
            cityModel.Description_Ar = keyValues["descriptionAr"];
            cityModel.Description_En = keyValues["descriptionEn"];
            cityModel.Created_By = UserID;

            try
            {
                _applicantBL.AddCityDesc(cityModel);
            }
            catch (Exception)
            {
                throw;
            }

            return View();

        }



        [NonAction]
        private IEnumerable<SelectListItem> GetApplicantCategoryDropDown()
        {
            List<ApplicantCategoryViewModel> viewModels = Translator.TranslateList<ApplicantCategoryModel, ApplicantCategoryViewModel>(_applicantBL.GetApplicantSubCategories());

            List<SelectListItem> ddlItems = new List<SelectListItem>();

            ddlItems.Add(new SelectListItem() { Text = "--Select--", Value = "-1" });

            foreach (var item in viewModels)
            {
                ddlItems.Add(new SelectListItem() { Text = item.Category_Name_En, Value = item.Applicant_Category_Id.ToString() });
            }

            return ddlItems;
        }

        [NonAction]
        private IEnumerable<SelectListItem> GetContentCategoryDropDown()
        {
            List<ApplicantContentCategoryViewModel> viewModels = Translator.TranslateList<ApplicantContentCategoryModel, ApplicantContentCategoryViewModel>(_applicantBL.GetActiveApplicantContentCategory());

            List<SelectListItem> dropDown = new List<SelectListItem>();

            foreach (var item in viewModels)
            {
                dropDown.Add(new SelectListItem() { Text = item.Title_En, Value = item.Applicant_Content_Id.ToString() });
            }

            return dropDown;
        }

        [NonAction]
        private IEnumerable<SelectListItem> GetCities(bool isAdmin = true)
        {

            List<CityModel> cityModels = _applicantBL.GetActiveCities(isAdmin);

            List<SelectListItem> dropDown = new List<SelectListItem>();

            foreach (var item in cityModels)
            {
                dropDown.Add(new SelectListItem() { Text = item.City_Name_En, Value = item.City_Id.ToString() });
            }

            return dropDown;

        }

    }
}