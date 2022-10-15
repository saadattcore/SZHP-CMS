using SZHPCMS.Common;
using DataAccess.CommonRespository;
using DataAccess.Database;
using DataContract.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.BusinessHandler
{
    public class EServiceBH
    {
        private readonly IUnitOfWork _uow;
        public EServiceBH(UnitOfWork uow)
        {
            _uow = uow;
        }

        #region E-Services Region

        /// <summary>
        /// Get active e-services objects
        /// </summary>
        /// <returns></returns>
        public List<EServiceModel> GetActiveEServices()
        {
            var dbEServices = _uow.EServiceRepository.GetActiveEServices();

            var mdEServices = new List<EServiceModel>();

            foreach (var item in dbEServices)
            {
                var objEService = new EServiceModel();

                objEService.EServiceID = item.E_Service_Id;
                objEService.TitleAr = item.Title_Ar;
                objEService.TitleEn = item.Title_En;
                objEService.VideoURL = item.Url;
                objEService.CategoryID = item.E_Service_Category != null ? item.E_Service_Category.E_Service_Category_Id : -1;
                objEService.CategoryName = item.E_Service_Category != null ? item.E_Service_Category.Category_Name_En : string.Empty;

                objEService.RowStatusId = item.Row_Status_Id;

                if (item.Document != null)
                {
                    objEService.Document = new DocumentModel();

                    objEService.Document.FileName = item.Document.File_Name;
                    objEService.Document.Extenstion = item.Document.Extenstion;
                }

                objEService.RowStatus = Enum.GetName(typeof(SZHPCMS.Common.RowStatus), item.Row_Status_Id);

                mdEServices.Add(objEService);

            }

            return mdEServices;
        }

        /// <summary>
        /// Get e-service object by id 
        /// </summary>
        /// <returns></returns>
        public EServiceModel GetByID(long id)
        {
            var dbEService = _uow.EServiceRepository.GetByID(id);

            var objEService = new EServiceModel();

            objEService.EServiceID = dbEService.E_Service_Id;
            objEService.TitleAr = dbEService.Title_Ar;
            objEService.TitleEn = dbEService.Title_En;
            objEService.DescriptionAr = dbEService.Description_Ar;
            objEService.DescriptionEn = dbEService.Description_En;
            objEService.CategoryID = dbEService.E_Service_Category != null ? dbEService.E_Service_Category.E_Service_Category_Id : -1;
            objEService.CategoryName = dbEService.E_Service_Category != null ? dbEService.E_Service_Category.Category_Name_Ar : string.Empty;
            objEService.VideoURL = dbEService.Url;

            objEService.RowStatusId = dbEService.Row_Status_Id;

            if (dbEService.Document != null)
            {
                objEService.Document = new DocumentModel();

                objEService.Document.FileName = dbEService.Document.File_Name;
                objEService.Document.Extenstion = dbEService.Document.Extenstion;
            }

            return objEService;
        }

        /// <summary>
        /// Add e-service object into db 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public EServiceModel Add(EServiceModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model is null");

            var dbEService = new E_Services();

            try
            {
                dbEService.Title_En = model.TitleEn;
                dbEService.Title_Ar = model.TitleAr;
                dbEService.Url = model.VideoURL;
                dbEService.Description_En = model.DescriptionEn;
                dbEService.Description_Ar = model.DescriptionAr;
                dbEService.E_Service_Category_Id = model.CategoryID;
                dbEService.Url = model.VideoURL;


                dbEService.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Active;
                dbEService.Created_By = model.CreatedBy;
                dbEService.Created_Date = DateTime.Now;

                if (model.Document != null)
                {
                    dbEService.Document = new Document();

                    dbEService.Document.File_Name = model.Document.FileName;
                    dbEService.Document.Extenstion = model.Document.Extenstion;
                    dbEService.Document.File_Type_Id = (long?)SZHPCMS.Common.FileTypes.Picture;
                    dbEService.Document.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Active;
                    dbEService.Document.Created_By = model.CreatedBy;
                    dbEService.Document.Created_Date = model.CreatedDate;
                }

                _uow.EServiceRepository.Add(dbEService);
                _uow.Save();

                model.EServiceID = dbEService.E_Service_Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return model;
        }

        /// <summary>
        /// Update e-service and related document if documet is not null
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Update(EServiceModel model)
        {
            var dbEService = _uow.EServiceRepository.GetByID(model.EServiceID);

            if (dbEService == null)
                throw new Exception("E service object not found with ID = " + model.EServiceID);

            int result = 0;

            try
            {
                dbEService.Title_En = model.TitleEn;
                dbEService.Title_Ar = model.TitleAr;
                dbEService.Url = model.VideoURL;
                dbEService.Description_Ar = model.DescriptionAr;
                dbEService.Description_En = model.DescriptionEn;
                dbEService.E_Service_Category_Id = model.CategoryID;
                dbEService.Url = model.VideoURL;

                dbEService.Updated_By = model.UpdatedBy;
                dbEService.Updated_Date = System.DateTime.Now;

                if (model.Document != null)
                {
                    if (dbEService.Document == null)
                    {
                        dbEService.Document = new Document();
                        dbEService.Created_By = model.CreatedBy;
                        dbEService.Created_Date = DateTime.Now;
                    }

                    else
                    {
                        dbEService.Updated_By = model.UpdatedBy;
                        dbEService.Updated_Date = DateTime.Now;
                    }


                    dbEService.Document.File_Name = model.Document.FileName;
                    dbEService.Document.Extenstion = model.Document.Extenstion;
                }

                result = _uow.Save();
            }
            catch (Exception)
            {
                throw;
            }

            return result;

        }

        /// <summary>
        /// Delete E-Service object from data base . It should be soft delete i.e just update row status to delete
        /// </summary>
        /// <param name="menu">E-Service object to delete</param>
        /// <returns></returns>
        public int Delete(int id)
        {
            E_Services dbEService = _uow.EServiceRepository.GetByID(id);
            dbEService.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Delete;

            return _uow.Save();
        }

        /// <summary>
        /// Update E-Service's status .
        /// </summary>
        /// <param name="menus"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int UpdateRowStatus(IEnumerable<long> idList, RowStatus status)
        {
            foreach (var id in idList)
            {
                E_Services dbEService = _uow.EServiceRepository.GetByID(id);
                dbEService.Row_Status_Id = (long?)status;
            }

            return _uow.Save();
        }

        #endregion

        #region E-Participation

        /// <summary>
        /// Get active e-participation objects
        /// </summary>
        /// <returns></returns>
        public List<EParticipationModel> GetActiveEParticipation(bool isAdmin = true)
        {
            var dbParticipations = _uow.EServiceRepository.GetActiveEParticipations(isAdmin);

            var modelParticipations = new List<EParticipationModel>();

            foreach (var item in dbParticipations)
            {
                var modelParticipation = new EParticipationModel();

                modelParticipation.EParticipationId = item.E_Participation_Id;
                modelParticipation.DescriptionEn = item.Description_En;
                modelParticipation.DescriptionAr = item.Description_Ar;
                modelParticipation.DocumentTextEn = item.Document_Text_En;
                modelParticipation.DocumentTextAr = item.Document_Text_Ar;
                modelParticipation.CreatedDate = item.Created_Date;

                if (item.Document != null)
                {
                    modelParticipation.Document = new DocumentModel();

                    modelParticipation.Document.FileName = item.Document.File_Name;
                    modelParticipation.Document.Extenstion = item.Document.Extenstion;
                }

                modelParticipation.RowStatus = Enum.GetName(typeof(SZHPCMS.Common.RowStatus), item.Row_Status_Id);

                modelParticipations.Add(modelParticipation);

            }

            return modelParticipations;
        }

        /// <summary>
        /// Get e-participation object by id 
        /// </summary>
        /// <returns></returns>
        public EParticipationModel GetParticipationByID(long id)
        {
            var dbParticipation = _uow.EServiceRepository.GetEParticipationById(id);

            var modelParticipation = new EParticipationModel();

            modelParticipation.EParticipationId = dbParticipation.E_Participation_Id;
            modelParticipation.DescriptionEn = dbParticipation.Description_En;
            modelParticipation.DescriptionAr = dbParticipation.Description_Ar;
            modelParticipation.DocumentTextEn = dbParticipation.Document_Text_En;
            modelParticipation.DocumentTextAr = dbParticipation.Document_Text_Ar;

            if (dbParticipation.Document != null)
            {
                modelParticipation.Document = new DocumentModel();

                modelParticipation.Document.FileName = dbParticipation.Document.File_Name;
                modelParticipation.Document.Extenstion = dbParticipation.Document.Extenstion;
            }

            return modelParticipation;
        }

        /// <summary>
        /// Add e-participation object into db 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public EParticipationModel AddParticipation(EParticipationModel model)
        {
            if (model == null)
                throw new ArgumentNullException(Constants.OBJECT_NULL_MESSAGE + model.EParticipationId);

            var dbParticipation = new E_Participation();

            try
            {
                dbParticipation.Description_En = model.DescriptionEn;
                dbParticipation.Description_Ar = model.DescriptionAr;
                dbParticipation.Document_Text_En = model.DocumentTextEn;
                dbParticipation.Document_Text_Ar = model.DocumentTextAr;

                dbParticipation.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Active;
                dbParticipation.Created_By = model.CreatedBy;
                dbParticipation.Created_Date = DateTime.Now;

                if (model.Document != null)
                {
                    dbParticipation.Document = new Document();

                    dbParticipation.Document.File_Name = model.Document.FileName;
                    dbParticipation.Document.Extenstion = model.Document.Extenstion;
                    dbParticipation.Document.File_Type_Id = (long?)SZHPCMS.Common.FileTypes.Pdf;
                    dbParticipation.Document.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Active;
                    dbParticipation.Document.Created_By = model.CreatedBy;
                    dbParticipation.Document.Created_Date = DateTime.Now;
                }

                _uow.EServiceRepository.AddEParticipation(dbParticipation);
                _uow.Save();

                model.EParticipationId = dbParticipation.E_Participation_Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return model;
        }

        /// <summary>
        /// Update e-participation and related document if documet is not null
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateParticipation(EParticipationModel model)
        {
            var dbParticipation = _uow.EServiceRepository.GetEParticipationById(model.EParticipationId);

            if (dbParticipation == null)
                throw new Exception("E service object not found with ID = " + model.EParticipationId);

            int result = 0;

            try
            {
                dbParticipation.Description_En = model.DescriptionEn;
                dbParticipation.Description_Ar = model.DescriptionAr;
                dbParticipation.Document_Text_En = model.DocumentTextEn;
                dbParticipation.Document_Text_Ar = model.DocumentTextAr;


                dbParticipation.Updated_By = model.UpdatedBy;
                dbParticipation.Updated_Date = System.DateTime.Now;

                if (model.Document != null)
                {

                    dbParticipation.Document.File_Name = model.Document.FileName;
                    dbParticipation.Document.Extenstion = model.Document.Extenstion;
                    dbParticipation.Document.Updated_By = model.UpdatedBy;
                    dbParticipation.Document.Updated_Date = DateTime.Now;
                }

                result = _uow.Save();
            }
            catch (Exception)
            {
                throw;
            }

            return result;

        }

        /// <summary>
        /// Delete E-Participation object from data base . It should be soft delete i.e just update row status to delete
        /// </summary>
        /// <param name="menu">E-Participation object to delete</param>
        /// <returns></returns>
        public int DeleteParticipation(int id)
        {
            var dbParticipation = _uow.EServiceRepository.GetEParticipationById(id);
            dbParticipation.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Delete;

            return _uow.Save();
        }

        /// <summary>
        /// Update E-Participation's status .
        /// </summary>
        /// <param name="menus"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int UpdateRowStatusParticipation(IEnumerable<long> idList, RowStatus status)
        {
            foreach (var id in idList)
            {
                var dbParticipation = _uow.EServiceRepository.GetEParticipationById(id);
                dbParticipation.Row_Status_Id = (long?)status;
            }

            return _uow.Save();
        }
        #endregion

        #region E-Service-Category Region
        public List<EServiceCategoryModel> GetActiveServiceCategories(string dataFor = "")
        {
            List<E_Service_Category> dbCategories = _uow.EServiceRepository.GetServiceCategories(dataFor);

            List<EServiceCategoryModel> modelCategories = new List<EServiceCategoryModel>();

            foreach (var x in dbCategories)
            {

                EServiceCategoryModel modelCategory = new EServiceCategoryModel();


                modelCategory.EServiceCategoryId = x.E_Service_Category_Id;
                modelCategory.CategoryNameEn = x.Category_Name_En;
                modelCategory.CategoryNameAr = x.Category_Name_Ar;
                modelCategory.CreatedDate = x.Created_Date;
                modelCategory.RowStatus = Enum.GetName(typeof(SZHPCMS.Common.RowStatus), x.Row_Status_Id);

                modelCategory.EServices = new List<EServiceModel>();

                foreach (var item in x.E_Services)
                {
                    EServiceModel modelEService = new EServiceModel();

                    modelEService.CategoryID = (long)item.E_Service_Category_Id;
                    modelEService.EServiceID = item.E_Service_Id;
                    modelEService.TitleEn = item.Title_En;
                    modelEService.TitleAr = item.Title_Ar;

                    modelCategory.EServices.Add(modelEService);

                }

                modelCategories.Add(modelCategory);

            }

            return modelCategories;

        }

        public EServiceCategoryModel GetServiceCatById(long id)
        {
            var dbCat = _uow.EServiceRepository.GetServiceCategoryById(id);

            if (dbCat == null)
                throw new Exception(Constants.OBJECT_NOT_FOUND_MESSAGE + id);

            EServiceCategoryModel modelCat = new EServiceCategoryModel();

            modelCat.EServiceCategoryId = dbCat.E_Service_Category_Id;
            modelCat.CategoryNameEn = dbCat.Category_Name_En;
            modelCat.CategoryNameAr = dbCat.Category_Name_Ar;

            return modelCat;

        }

        public EServiceCategoryModel AddServiceCat(EServiceCategoryModel modelCat)
        {
            if (modelCat == null)
                throw new ArgumentNullException(Constants.OBJECT_NULL_MESSAGE);

            E_Service_Category dbCat = new E_Service_Category();

            dbCat.Category_Name_En = modelCat.CategoryNameEn;
            dbCat.Category_Name_Ar = modelCat.CategoryNameAr;
            dbCat.Created_By = modelCat.CreatedBy;
            dbCat.Created_Date = DateTime.Now;
            dbCat.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Active;

            _uow.EServiceRepository.AddServiceCategory(dbCat);
            _uow.Save();

            modelCat.EServiceCategoryId = modelCat.EServiceCategoryId;

            return modelCat;

        }

        public int UpdateServiceCat(EServiceCategoryModel modelCat)
        {
            if (modelCat == null)
                throw new ArgumentNullException(Constants.OBJECT_NULL_MESSAGE);

            var dbCat = _uow.EServiceRepository.GetServiceCategoryById(modelCat.EServiceCategoryId);

            if (dbCat == null)
                throw new Exception(Constants.OBJECT_NOT_FOUND_MESSAGE + modelCat.EServiceCategoryId);

            dbCat.Category_Name_En = modelCat.CategoryNameEn;
            dbCat.Category_Name_Ar = modelCat.CategoryNameAr;

            dbCat.Updated_By = modelCat.UpdatedBy;
            dbCat.Updated_Date = DateTime.Now;

            return _uow.Save();


        }

        /// <summary>
        /// Delete E-Service-Category object from data base . It should be soft delete i.e just update row status to delete
        /// </summary>
        /// <param name="menu">E-Participation object to delete</param>
        /// <returns></returns>
        public int DeleteServiceCategory(int id)
        {
            var dbCat = _uow.EServiceRepository.GetServiceCategoryById(id);
            dbCat.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Delete;

            return _uow.Save();
        }

        /// <summary>
        /// Update E-Service-Category's status .
        /// </summary>
        /// <param name="menus"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int UpdateRowStatusServiceCategory(IEnumerable<long> idList, RowStatus status)
        {
            foreach (var id in idList)
            {
                var dbCat = _uow.EServiceRepository.GetServiceCategoryById(id);
                dbCat.Row_Status_Id = (long?)status;
            }

            return _uow.Save();
        }
        #endregion
    }
}
