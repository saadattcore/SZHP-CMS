using DataAccess.CommonRespository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.BusinessHandler;
using DataContract.Implementation;
using DataAccess.Database;


namespace BusinessLogic.BusinessHandler
{
    public class ApplicantBH
    {
        private readonly IUnitOfWork _uow;
        public ApplicantBH(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public ApplicantCategoryModel AddApplicantCategory(ApplicantCategoryModel applicantCategory)
        {
            if (applicantCategory == null)
                throw new ArgumentNullException(SZHPCMS.Common.Constants.OBJECT_NULL_MESSAGE);

            Applicant_Category dbApplicantCategory = new Applicant_Category();

            dbApplicantCategory.Category_Name_Ar = applicantCategory.Category_Name_Ar;
            dbApplicantCategory.Category_Name_En = applicantCategory.Category_Name_En;
            dbApplicantCategory.Header_Description_Ar = applicantCategory.Header_Description_Ar;
            dbApplicantCategory.Header_Description_En = applicantCategory.Header_Description_En;
            dbApplicantCategory.Parent_Id = applicantCategory.Parent_Id;
            dbApplicantCategory.Row_Status_Id = (long)SZHPCMS.Common.RowStatus.Active;
            dbApplicantCategory.Created_By = applicantCategory.Created_By;
            dbApplicantCategory.Created_Date = DateTime.Now;

            if (applicantCategory.ApplicantCategoryModels != null)
            {
                foreach (var item in applicantCategory.ApplicantCategoryModels)
                {
                    Applicant_Category dbSubCategory = new Applicant_Category();

                    dbSubCategory.Category_Name_Ar = item.Category_Name_Ar;
                    dbSubCategory.Category_Name_En = item.Category_Name_En;
                    dbSubCategory.Header_Description_Ar = item.Header_Description_Ar;
                    dbSubCategory.Header_Description_En = item.Header_Description_En;
                    dbSubCategory.Parent_Id = item.Parent_Id;
                    dbSubCategory.Row_Status_Id = (long)SZHPCMS.Common.RowStatus.Active;
                    dbSubCategory.Created_By = applicantCategory.Created_By;
                    dbSubCategory.Created_Date = DateTime.Now;

                    dbApplicantCategory.Applicant_Category1.Add(dbSubCategory);

                }
            }


            if (applicantCategory.CityDescriptions != null)
            {
                foreach (var item in applicantCategory.CityDescriptions)
                {
                    City_Description cityDesc = new City_Description();
                    cityDesc.Description_Ar = item.Description_Ar;
                    cityDesc.Description_En = item.Description_En;
                    cityDesc.Created_By = item.Created_By;
                    cityDesc.Created_Date = DateTime.Now;
                    cityDesc.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Active;

                    dbApplicantCategory.City_Description.Add(cityDesc);
                }
            }

            try
            {
                _uow.ApplicantRepository.Add(dbApplicantCategory);
                _uow.Save();

                applicantCategory.Applicant_Category_Id = dbApplicantCategory.Applicant_Category_Id;

                return applicantCategory;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<ApplicantCategoryModel> GetActiveApplicantCategory(bool isAdmin = true)
        {
            List<Applicant_Category> dbApplicantCategories = isAdmin ? _uow.ApplicantRepository.GetAll().Where(x => x.Row_Status_Id != (long?)SZHPCMS.Common.RowStatus.Delete).ToList() : _uow.ApplicantRepository.GetAll().Where(x => x.Row_Status_Id == (long?)SZHPCMS.Common.RowStatus.Active).ToList();

            List<ApplicantCategoryModel> applicantModels = new List<ApplicantCategoryModel>();

            foreach (var item in dbApplicantCategories)
            {
                ApplicantCategoryModel model = new ApplicantCategoryModel();

                model.Applicant_Category_Id = item.Applicant_Category_Id;
                model.Category_Name_Ar = item.Category_Name_Ar;
                model.Category_Name_En = item.Category_Name_En;
                model.Header_Description_Ar = item.Header_Description_Ar;
                model.Header_Description_En = item.Header_Description_En;
                model.Parent_Id = item.Parent_Id;
                model.Parent = item.Applicant_Category2 != null ? item.Applicant_Category2.Category_Name_En : "N/A";
                model.Row_Status = Enum.GetName(typeof(SZHPCMS.Common.RowStatus), item.Row_Status_Id);
                model.Created_Date = item.Created_Date;

                applicantModels.Add(model);
            }

            return applicantModels;
        }

        public ApplicantContentCategoryModel AddApplicantContentCategory(ApplicantContentCategoryModel applicantContentCategory)
        {
            if (applicantContentCategory == null)
                throw new ArgumentNullException(SZHPCMS.Common.Constants.OBJECT_NULL_MESSAGE);

            Applicant_Content_Category dbApplicantContentCategory = new Applicant_Content_Category();

            dbApplicantContentCategory.Title_Ar = applicantContentCategory.Title_Ar;
            dbApplicantContentCategory.Title_En = applicantContentCategory.Title_En;
            dbApplicantContentCategory.Description_Ar = applicantContentCategory.Description_Ar;
            dbApplicantContentCategory.Description_En = applicantContentCategory.Description_En;
            dbApplicantContentCategory.Applicant_Category = applicantContentCategory.Applicant_Category;
            dbApplicantContentCategory.Row_Status_Id = (long)SZHPCMS.Common.RowStatus.Active;
            dbApplicantContentCategory.Created_By = applicantContentCategory.Created_By;
            dbApplicantContentCategory.Created_Date = DateTime.Now;

            dbApplicantContentCategory.Applicant_Content_Sub_Category = new List<Applicant_Content_Sub_Category>();

            foreach (var item in applicantContentCategory.ApplicantContentSubCategories)
            {
                Applicant_Content_Sub_Category subCategory = new Applicant_Content_Sub_Category();

                subCategory.Description_Ar = item.Description_Ar;
                subCategory.Description_En = item.Description_En;
                subCategory.Created_By = item.Created_By;
                subCategory.Created_Date = DateTime.Now;
                subCategory.Title_Ar = item.Title_Ar;
                subCategory.Title_En = item.Title_En;
                subCategory.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Active;

                dbApplicantContentCategory.Applicant_Content_Sub_Category.Add(subCategory);

            }

            try
            {
                _uow.ApplicantRepository.AddApplicantContentCategory(dbApplicantContentCategory);

                applicantContentCategory.Applicant_Content_Id = applicantContentCategory.Applicant_Content_Id;

                return applicantContentCategory;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public int AddContentCategoryRange(List<ApplicantContentCategoryModel> contentCategories)
        {
            if (contentCategories == null)
                throw new ArgumentNullException(SZHPCMS.Common.Constants.OBJECT_NULL_MESSAGE);

            List<Applicant_Content_Category> dbContentCategories = new List<Applicant_Content_Category>();

            foreach (var item in contentCategories)
            {
                Applicant_Content_Category dbApplicantContentCategory = new Applicant_Content_Category();

                dbApplicantContentCategory.Title_Ar = item.Title_Ar;
                dbApplicantContentCategory.Title_En = item.Title_En;
                dbApplicantContentCategory.Description_Ar = item.Description_Ar;
                dbApplicantContentCategory.Description_En = item.Description_En;
                dbApplicantContentCategory.Applicant_Category = item.Applicant_Category;
                dbApplicantContentCategory.Row_Status_Id = (long)SZHPCMS.Common.RowStatus.Active;
                dbApplicantContentCategory.Created_By = item.Created_By;
                dbApplicantContentCategory.Created_Date = DateTime.Now;

                dbContentCategories.Add(dbApplicantContentCategory);
            }

            try
            {
                _uow.ApplicantRepository.AddContentCategoryRange(dbContentCategories);

                return _uow.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<ApplicantContentCategoryModel> GetActiveApplicantContentCategory(bool isAdmin = true)
        {
            List<Applicant_Content_Category> dbApplicantContentCategory = _uow.ApplicantRepository.GetActiveContentCategory(isAdmin);

            List<ApplicantContentCategoryModel> applicantModels = new List<ApplicantContentCategoryModel>();

            foreach (var item in dbApplicantContentCategory)
            {
                ApplicantContentCategoryModel model = new ApplicantContentCategoryModel();

                model.Applicant_Content_Id = item.Applicant_Content_Id;
                model.Title_Ar = item.Title_Ar;
                model.Title_En = item.Title_En;
                model.Description_Ar = item.Description_Ar;
                model.Description_En = item.Description_En;
                model.Created_Date = item.Created_Date;

                if (item.Applicant_Category1 != null)
                {
                    model.ApplicantCategory = new ApplicantCategoryModel();

                    model.ApplicantCategory.Header_Description_Ar = item.Applicant_Category1.Header_Description_Ar;
                    model.ApplicantCategory.Header_Description_En = item.Applicant_Category1.Header_Description_En;
                    model.ApplicantCategory.Category_Name_Ar = item.Applicant_Category1.Category_Name_Ar;
                    model.ApplicantCategory.Category_Name_En = item.Applicant_Category1.Category_Name_En;

                    model.ParentSubCategory = item.Applicant_Category1.Category_Name_En;
                }

                applicantModels.Add(model);
            }

            return applicantModels;
        }

        public List<ApplicantContentSubCategoryModel> GetActiveApplicantContentSubCategory(bool isAdmin = true)
        {
            var dbObjects = _uow.ApplicantRepository.GetActiveContentSubCategory(isAdmin);

            List<ApplicantContentSubCategoryModel> models = new List<ApplicantContentSubCategoryModel>();

            foreach (var item in dbObjects)
            {
                var model = new ApplicantContentSubCategoryModel();

                model.Applicant_Content_SubCategory_Id = item.Applicant_Content_SubCategory_Id;
                model.Title_Ar = item.Title_Ar;
                model.Title_En = item.Title_En;
                model.Description_Ar = item.Description_Ar;
                model.Description_En = item.Description_En;
                model.Created_Date = item.Created_Date;
                model.Row_Status = Enum.GetName(typeof(SZHPCMS.Common.RowStatus), item.Row_Status_Id);

                models.Add(model);
            }


            return models;

        }


        public List<ApplicantCategoryModel> GetApplicantSubCategories(bool isAdmin = true)
        {
            return _uow.ApplicantRepository.GetApplicantSubCategories(isAdmin).Select(x => new ApplicantCategoryModel()
              {

                  Applicant_Category_Id = x.Applicant_Category_Id,
                  Category_Name_Ar = x.Category_Name_Ar,
                  Category_Name_En = x.Category_Name_En,
                  Created_By = x.Created_By,
                  Created_Date = x.Created_Date,
                  Header_Description_Ar = x.Header_Description_Ar,
                  Header_Description_En = x.Header_Description_En
              }).ToList();


        }

        public int AddContentSubCategoryRange(List<ApplicantContentSubCategoryModel> contentSubCategories)
        {

            if (contentSubCategories == null)
                throw new ArgumentNullException(SZHPCMS.Common.Constants.OBJECT_NULL_MESSAGE);

            List<Applicant_Content_Sub_Category> dbContentCategories = new List<Applicant_Content_Sub_Category>();

            foreach (var item in contentSubCategories)
            {
                Applicant_Content_Sub_Category dbApplicantContentCategory = new Applicant_Content_Sub_Category();

                dbApplicantContentCategory.Title_Ar = item.Title_Ar;
                dbApplicantContentCategory.Title_En = item.Title_En;
                dbApplicantContentCategory.Description_Ar = item.Description_Ar;
                dbApplicantContentCategory.Description_En = item.Description_En;
                dbApplicantContentCategory.Applicant_Content_Id = item.Applicant_Content_Id;
                dbApplicantContentCategory.Row_Status_Id = (long)SZHPCMS.Common.RowStatus.Active;
                dbApplicantContentCategory.Created_By = item.Created_By;
                dbApplicantContentCategory.Created_Date = DateTime.Now;

                dbContentCategories.Add(dbApplicantContentCategory);
            }

            try
            {
                _uow.ApplicantRepository.AddContentSubCategoryRange(dbContentCategories);

                return _uow.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<CityModel> GetActiveCities(bool isAdmin = true)
        {
            return _uow.ApplicantRepository.GetActiveCities(isAdmin).Select(x => new CityModel() { City_Id = x.City_Id, City_Name_En = x.City_Name_En , City_Name_Ar = x.City_Name_Ar }).ToList();
        }

        public int AddCityDesc(CityDescriptionModel cityDescModel)
        {
            if (cityDescModel == null)
                throw new ArgumentNullException(SZHPCMS.Common.Constants.OBJECT_NULL_MESSAGE);

            City_Description dbObject = new City_Description();

            dbObject.Description_Ar = cityDescModel.Description_Ar;
            dbObject.Description_En = cityDescModel.Description_En;
            dbObject.Created_By = dbObject.Created_By;
            dbObject.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Active;
            dbObject.Created_Date = DateTime.Now;

            try
            {
                _uow.ApplicantRepository.AddCityDescription(dbObject);
                return _uow.Save();
            }
            catch (Exception)
            {

                throw;
            }




        }

        public List<ApplicantContentCategoryModel> GetContentCategoryByApplicantSubCategoryID(long applicantSubCategoryId , bool isAdmin = true )
        {
            List<Applicant_Content_Category> dbApplicantContentCategory = _uow.ApplicantRepository.GetContentCategoriesByApplicantSubCategoryID(applicantSubCategoryId, isAdmin);

            List<ApplicantContentCategoryModel> applicantModels = new List<ApplicantContentCategoryModel>();

            foreach (var item in dbApplicantContentCategory)
            {
                ApplicantContentCategoryModel model = new ApplicantContentCategoryModel();

                model.Applicant_Content_Id = item.Applicant_Content_Id;
                model.Title_Ar = item.Title_Ar;
                model.Title_En = item.Title_En;
                model.Description_Ar = item.Description_Ar;
                model.Description_En = item.Description_En;
                model.Created_Date = item.Created_Date;

                model.ApplicantContentSubCategories = new List<ApplicantContentSubCategoryModel>();

                foreach (var contentSubCat in item.Applicant_Content_Sub_Category)
                {
                    ApplicantContentSubCategoryModel subCatModel = new ApplicantContentSubCategoryModel();

                    subCatModel.Description_Ar = contentSubCat.Description_Ar;
                    subCatModel.Description_En = contentSubCat.Description_En;
                    subCatModel.Title_Ar = contentSubCat.Title_Ar;
                    subCatModel.Title_En = contentSubCat.Title_En;
                    subCatModel.Applicant_Content_SubCategory_Id = contentSubCat.Applicant_Content_SubCategory_Id;

                    model.ApplicantContentSubCategories.Add(subCatModel);


                }

             

                applicantModels.Add(model);
            }

            return applicantModels;
        }

        public List<CityDescriptionModel> GetCityDescriptionsByApplicantSubCategoryId(long appSubCategoryId, bool isAdmin = true)
        {
            List<City_Description> dbObjects = _uow.ApplicantRepository.GetCityDescriptionsByApplicantSubCategoryID(appSubCategoryId, isAdmin);

            List<CityDescriptionModel> modelObjects = new List<CityDescriptionModel>();

            foreach (var item in dbObjects)
            {
                modelObjects.Add(new CityDescriptionModel() {Description_Id = item.Description_Id , Description_Ar = item.Description_Ar , Description_En = item.Description_En , Created_Date = item.Created_Date });
            }

            return modelObjects;
        
        }

        public CityDescriptionModel GetCityDescription(long applicantCategoryID, long cityID)
        {

            var dbObj = _uow.ApplicantRepository.GetCityDescription(applicantCategoryID, cityID);

            var modelObj = new CityDescriptionModel() {Description_Id = dbObj.Description_Id , Description_Ar = dbObj.Description_Ar , Description_En = dbObj.Description_En };

            modelObj.City = new CityModel();

            modelObj.City.City_Name_Ar = dbObj.City.City_Name_Ar;
            modelObj.City.City_Name_En = dbObj.City.City_Name_En;

            return modelObj;
        
        }
    }
}
