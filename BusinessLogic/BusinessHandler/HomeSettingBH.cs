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
    public class HomeSettingBH
    {
        private readonly IUnitOfWork _uow;
        public HomeSettingBH(UnitOfWork uow)
        {
            _uow = uow;

        }

        #region Home Settings Region
        /// <summary>
        /// Get home setting object
        /// </summary>
        /// <returns></returns>
        public HomeSettingModel GetHomeSetting()
        {
            var dbHomeSetting = _uow.HomeSettingRepository.GetHomeSetting();

            if (dbHomeSetting == null)
                return new HomeSettingModel();

            HomeSettingModel modelHomeSetting = new HomeSettingModel()
            {
                HomeSettingId = dbHomeSetting.Home_Setting_Id,
                Facebook = dbHomeSetting.Facebook,
                Twitter = dbHomeSetting.Twitter,
                Instagram = dbHomeSetting.Twitter,
                Youtube = dbHomeSetting.Youtube,
                //Word = dbHomeSetting.Word,
                //Excel = dbHomeSetting.Excel,
                //PDF = dbHomeSetting.PDF,
                //AndroidLink = dbHomeSetting.Android_Link,
                //IphoneLink = dbHomeSetting.Iphone_Link,

                RowStatus = Enum.GetName(typeof(SZHPCMS.Common.RowStatus), dbHomeSetting.Row_Status_Id)
            };

            if (dbHomeSetting.Document != null)
            {
                modelHomeSetting.BannerDocument = new DocumentModel();
                modelHomeSetting.BannerDocument.FileName = dbHomeSetting.Document.File_Name;
                modelHomeSetting.BannerDocument.Extenstion = dbHomeSetting.Document.Extenstion;
            }

            if (dbHomeSetting.Document1 != null)
            {

                modelHomeSetting.ExcelDocument = new DocumentModel();
                modelHomeSetting.ExcelDocument.FileName = dbHomeSetting.Document1.File_Name;
                modelHomeSetting.ExcelDocument.Extenstion = dbHomeSetting.Document1.Extenstion;
            }

            if (dbHomeSetting.Document2 != null)
            {

                modelHomeSetting.PDFDocument = new DocumentModel();
                modelHomeSetting.PDFDocument.FileName = dbHomeSetting.Document2.File_Name;
                modelHomeSetting.PDFDocument.Extenstion = dbHomeSetting.Document2.Extenstion;
            }

            if (dbHomeSetting.Document3 != null)
            {

                modelHomeSetting.WordDocument = new DocumentModel();
                modelHomeSetting.WordDocument.FileName = dbHomeSetting.Document3.File_Name;
                modelHomeSetting.WordDocument.Extenstion = dbHomeSetting.Document3.Extenstion;
            }


            return modelHomeSetting;
        }

        /// <summary>
        /// Add home setting object . It will called only one time in start .
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public HomeSettingModel Add(HomeSettingModel model)
        {
            if (model == null)
                throw new ArgumentNullException("Home setting model cannot be null.");

            Home_Setting dbHomeSetting = new Home_Setting();

            dbHomeSetting.Facebook = model.Facebook;
            dbHomeSetting.Twitter = model.Twitter;
            dbHomeSetting.Instagram = model.Twitter;
            dbHomeSetting.Youtube = model.Youtube;



            //dbHomeSetting.Document = model.BannerDocument;
            //dbHomeSetting.Excel = model.Excel;
            //dbHomeSetting.PDF = model.PDF;
            //dbHomeSetting.Android_Link = model.AndroidLink;
            //dbHomeSetting.Android_Link = model.AndroidLink;



            dbHomeSetting.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Active;
            dbHomeSetting.Created_By = model.CreatedBy;
            dbHomeSetting.Created_Date = DateTime.Now;


            if (model.BannerDocument != null)
            {
                dbHomeSetting.Document = new Document();

                dbHomeSetting.Document.File_Name = model.BannerDocument.FileName;
                dbHomeSetting.Document.Extenstion = model.BannerDocument.Extenstion;
                dbHomeSetting.Document.File_Type_Id = (long?)SZHPCMS.Common.FileTypes.Picture;
            }

            if (model.ExcelDocument != null)
            {
                dbHomeSetting.Document1 = new Document();

                dbHomeSetting.Document1.File_Name = model.ExcelDocument.FileName;
                dbHomeSetting.Document1.Extenstion = model.ExcelDocument.Extenstion;

                dbHomeSetting.Document1.File_Type_Id = (long?)SZHPCMS.Common.FileTypes.Picture;
            }

            if (model.PDFDocument != null)
            {
                dbHomeSetting.Document2 = new Document();

                dbHomeSetting.Document2.File_Name = model.PDFDocument.FileName;
                dbHomeSetting.Document2.Extenstion = model.PDFDocument.Extenstion;
                dbHomeSetting.Document2.File_Type_Id = (long?)SZHPCMS.Common.FileTypes.Picture;
            }

            if (model.WordDocument != null)
            {
                dbHomeSetting.Document3 = new Document();

                dbHomeSetting.Document3.File_Name = model.WordDocument.FileName;
                dbHomeSetting.Document3.Extenstion = model.WordDocument.Extenstion;
                dbHomeSetting.Document3.File_Type_Id = (long?)SZHPCMS.Common.FileTypes.Picture;
            }





            try
            {
                _uow.HomeSettingRepository.Add(dbHomeSetting);
                _uow.Save();

                model.HomeSettingId = dbHomeSetting.Home_Setting_Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return model;
        }

        /// <summary>
        /// Update setting home
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Update(HomeSettingModel model)
        {
            if (model == null && model.HomeSettingId <= 0)
                throw new ArgumentNullException("Home setting model cannot be null.");

            int result = 0;

            try
            {
                var dbHomeSetting = _uow.HomeSettingRepository.GetByID(model.HomeSettingId);

                if (dbHomeSetting == null)
                    throw new Exception("Home Setting with id= " + model.HomeSettingId + " not found");


                dbHomeSetting.Facebook = model.Facebook;
                dbHomeSetting.Twitter = model.Twitter;
                dbHomeSetting.Instagram = model.Instagram;
                dbHomeSetting.Youtube = model.Youtube;
                //dbHomeSetting.Word = model.Word;
                //dbHomeSetting.Excel = model.Excel;
                //dbHomeSetting.PDF = model.PDF;
                //dbHomeSetting.Android_Link = model.AndroidLink;
                //dbHomeSetting.Iphone_Link = model.IphoneLink;
                dbHomeSetting.Updated_By = model.Updated_By;
                dbHomeSetting.Updated_Date = DateTime.Now;

                if (model.BannerDocument != null)
                {

                    if (dbHomeSetting.Document == null)
                    {
                        dbHomeSetting.Document = new Document();

                        dbHomeSetting.Document.Created_Date = DateTime.Now;
                        dbHomeSetting.Document.Created_By = model.CreatedBy;
                    }
                    else
                    {
                        dbHomeSetting.Document.Updated_Date = DateTime.Now;
                        dbHomeSetting.Document.Updated_By = model.CreatedBy;
                    }


                    dbHomeSetting.Document.File_Name = model.BannerDocument.FileName;
                    dbHomeSetting.Document.Extenstion = model.BannerDocument.Extenstion;
                }

                if (model.ExcelDocument != null)
                {

                    if (dbHomeSetting.Document1 == null)
                    {
                        dbHomeSetting.Document1 = new Document();

                        dbHomeSetting.Document1.Created_Date = DateTime.Now;
                        dbHomeSetting.Document1.Created_By = model.CreatedBy;
                    }
                    else
                    {
                        dbHomeSetting.Document1.Updated_Date = DateTime.Now;
                        dbHomeSetting.Document1.Updated_By = model.CreatedBy;
                    }


                    dbHomeSetting.Document1.File_Name = model.ExcelDocument.FileName;
                    dbHomeSetting.Document1.Extenstion = model.ExcelDocument.Extenstion;
                }

                if (model.PDFDocument != null)
                {

                    if (dbHomeSetting.Document2 == null)
                    {
                        dbHomeSetting.Document2 = new Document();

                        dbHomeSetting.Document2.Created_Date = DateTime.Now;
                        dbHomeSetting.Document2.Created_By = model.CreatedBy;
                    }
                    else
                    {
                        dbHomeSetting.Document2.Updated_Date = DateTime.Now;
                        dbHomeSetting.Document2.Updated_By = model.CreatedBy;
                    }


                    dbHomeSetting.Document2.File_Name = model.PDFDocument.FileName;
                    dbHomeSetting.Document2.Extenstion = model.PDFDocument.Extenstion;
                }

                if (model.WordDocument != null)
                {

                    if (dbHomeSetting.Document3 == null)
                    {
                        dbHomeSetting.Document3 = new Document();

                        dbHomeSetting.Document3.Created_Date = DateTime.Now;
                        dbHomeSetting.Document3.Created_By = model.CreatedBy;
                    }
                    else
                    {
                        dbHomeSetting.Document3.Updated_Date = DateTime.Now;
                        dbHomeSetting.Document3.Updated_By = model.CreatedBy;
                    }


                    dbHomeSetting.Document3.File_Name = model.WordDocument.FileName;
                    dbHomeSetting.Document3.Extenstion = model.WordDocument.Extenstion;
                }



                result = _uow.Save();

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
        #endregion

        #region Mobile App Region
        /// <summary>
        /// Get mobile apps
        /// </summary>
        /// <returns></returns>
        public List<MobileAppModel> GetMobileApps(string dataFor = "")
        {
            return _uow.HomeSettingRepository.GetMobileApps(dataFor).Select(x => new MobileAppModel()
             {
                 MobileApplicationId = x.Mobile_Application_Id,
                 NameEn = x.Name_En,
                 NameAr = x.Name_Ar,
                 CreatedDate = x.Created_Date,
                 DescriptionEn = x.Description_En,
                 DescriptionAr = x.Description_Ar,
                 Document = x.Document2 != null ? new DocumentModel() { DocumentId = x.Document2.Document_Id, FileName = x.Document2.File_Name, Extenstion = x.Document2.Extenstion } : null,
                 RowStatus = Enum.GetName(typeof(SZHPCMS.Common.RowStatus), x.Row_Status_Id),
                 DocumentName = x.Document2 != null ? x.Document2.File_Name : string.Empty,
                 AppStoreURL = x.App_Store_URL,
                 PlayStoreURL = x.Play_Store_URL

             }).ToList();
        }

        /// <summary>
        /// Get app by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MobileAppModel GetAppById(long id)
        {
            Mobile_App dbMobileApp = _uow.HomeSettingRepository.GetAppById(id);

            if (dbMobileApp == null)
                throw new Exception(SZHPCMS.Common.Constants.OBJECT_NOT_FOUND_MESSAGE + id as string);

            MobileAppModel modelApp = new MobileAppModel();

            modelApp.NameAr = dbMobileApp.Name_Ar;
            modelApp.NameEn = dbMobileApp.Name_En;
            modelApp.MobileApplicationId = dbMobileApp.Mobile_Application_Id;
            modelApp.DescriptionEn = dbMobileApp.Description_En;
            modelApp.DescriptionAr = dbMobileApp.Description_Ar;
            modelApp.AppStoreURL = dbMobileApp.App_Store_URL;
            modelApp.PlayStoreURL = dbMobileApp.Play_Store_URL;
            modelApp.BlackBerryWorldURL = dbMobileApp.BlackBerry_World_URL;
            modelApp.WinStoreURL = dbMobileApp.Win_Store_URL;

            modelApp.Document = dbMobileApp.Document2 != null ? new DocumentModel() { DocumentId = dbMobileApp.Document2.Document_Id, FileName = dbMobileApp.Document2.File_Name, Extenstion = dbMobileApp.Document2.Extenstion } : null;
            modelApp.IPhoneQRCodeDoc = dbMobileApp.Document3 != null ? new DocumentModel() { DocumentId = dbMobileApp.Document3.Document_Id, FileName = dbMobileApp.Document3.File_Name, Extenstion = dbMobileApp.Document3.Extenstion } : null;
            modelApp.AndroidQRCodeDoc = dbMobileApp.Document != null ? new DocumentModel() { DocumentId = dbMobileApp.Document.Document_Id, FileName = dbMobileApp.Document.File_Name, Extenstion = dbMobileApp.Document.Extenstion } : null;
            modelApp.BlackBerryQRCodDoc = dbMobileApp.Document1 != null ? new DocumentModel() { DocumentId = dbMobileApp.Document1.Document_Id, FileName = dbMobileApp.Document1.File_Name, Extenstion = dbMobileApp.Document1.Extenstion } : null;
            modelApp.WinQRCodeDoc = dbMobileApp.Document4 != null ? new DocumentModel() { DocumentId = dbMobileApp.Document4.Document_Id, FileName = dbMobileApp.Document4.File_Name, Extenstion = dbMobileApp.Document4.Extenstion } : null;

            return modelApp;

        }

        /// <summary>
        /// Add new mobile app in database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        public MobileAppModel AddMobileApp(MobileAppModel model)
        {
            if (model == null)
                throw new ArgumentNullException(SZHPCMS.Common.Constants.OBJECT_NULL_MESSAGE);

            try
            {
                Mobile_App dbApp = new Mobile_App();

                dbApp.Name_Ar = model.NameAr;
                dbApp.Name_En = model.NameEn;
                dbApp.Description_En = model.DescriptionEn;
                dbApp.Description_Ar = model.DescriptionAr;
                dbApp.App_Store_URL = model.AppStoreURL;
                dbApp.Play_Store_URL = model.PlayStoreURL;
                dbApp.BlackBerry_World_URL = model.BlackBerryWorldURL;
                dbApp.Win_Store_URL = model.WinStoreURL;
                dbApp.Created_By = model.CreatedBy;
                dbApp.Created_Date = DateTime.Now;
                dbApp.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Active;

                if (model.AndroidQRCodeDoc != null)
                    dbApp.Document = new Document() { File_Name = model.AndroidQRCodeDoc.FileName, Extenstion = model.AndroidQRCodeDoc.Extenstion, File_Type_Id = (long?)SZHPCMS.Common.FileTypes.Picture, Created_By = model.CreatedBy, Created_Date = DateTime.Now };

                if (model.BlackBerryQRCodDoc != null)
                    dbApp.Document1 = new Document() { File_Name = model.BlackBerryQRCodDoc.FileName, Extenstion = model.BlackBerryQRCodDoc.Extenstion, File_Type_Id = (long?)SZHPCMS.Common.FileTypes.Picture, Created_By = model.CreatedBy, Created_Date = DateTime.Now };

                if (model.Document != null)
                    dbApp.Document2 = new Document() { File_Name = model.Document.FileName, Extenstion = model.Document.Extenstion, File_Type_Id = (long?)SZHPCMS.Common.FileTypes.Picture, Created_By = model.CreatedBy, Created_Date = DateTime.Now };

                if (model.IPhoneQRCodeDoc != null)
                    dbApp.Document3 = new Document() { File_Name = model.IPhoneQRCodeDoc.FileName, Extenstion = model.IPhoneQRCodeDoc.Extenstion, File_Type_Id = (long?)SZHPCMS.Common.FileTypes.Picture, Created_By = model.CreatedBy, Created_Date = DateTime.Now };

                if (model.WinQRCodeDoc != null)
                    dbApp.Document4 = new Document() { File_Name = model.WinQRCodeDoc.FileName, Extenstion = model.WinQRCodeDoc.Extenstion, File_Type_Id = (long?)SZHPCMS.Common.FileTypes.Picture, Created_By = model.CreatedBy, Created_Date = DateTime.Now };

                _uow.HomeSettingRepository.AddMobileApp(dbApp);
                _uow.Save();

                model.MobileApplicationId = dbApp.Mobile_Application_Id;

                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Update mobile app
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateMobileApp(MobileAppModel model)
        {
            if (model == null)
                throw new ArgumentNullException(SZHPCMS.Common.Constants.OBJECT_NULL_MESSAGE);

            var dbApp = _uow.HomeSettingRepository.GetAppById(model.MobileApplicationId);

            if (dbApp == null)
                throw new Exception(SZHPCMS.Common.Constants.OBJECT_NOT_FOUND_MESSAGE + model.MobileApplicationId as string);

            try
            {
                dbApp.Name_Ar = model.NameAr;
                dbApp.Name_En = model.NameEn;
                dbApp.Description_En = model.DescriptionEn;
                dbApp.Description_Ar = model.DescriptionAr;
                dbApp.App_Store_URL = model.AppStoreURL;
                dbApp.Play_Store_URL = model.PlayStoreURL;
                dbApp.BlackBerry_World_URL = model.BlackBerryWorldURL;
                dbApp.Win_Store_URL = model.WinStoreURL;
                dbApp.Updated_By = model.CreatedBy;
                dbApp.Updated_Date = DateTime.Now;


                if (model.AndroidQRCodeDoc != null)
                {

                    if (dbApp.Document == null)
                    {
                        dbApp.Document = new Document() { File_Name = model.AndroidQRCodeDoc.FileName, Extenstion = model.AndroidQRCodeDoc.Extenstion, File_Type_Id = (long?)SZHPCMS.Common.FileTypes.Picture, Created_By = model.CreatedBy, Created_Date = DateTime.Now };
                    }
                    else
                    {
                        dbApp.Document.File_Name = model.AndroidQRCodeDoc.FileName;
                        dbApp.Document.Extenstion = model.AndroidQRCodeDoc.Extenstion;
                        dbApp.Document.Updated_By = model.UpdatedBy;
                        dbApp.Document.Updated_Date = DateTime.Now;
                    }
                }

                if (model.BlackBerryQRCodDoc != null)
                {

                    if (dbApp.Document1 == null)
                    {
                        dbApp.Document1 = new Document() { File_Name = model.BlackBerryQRCodDoc.FileName, Extenstion = model.BlackBerryQRCodDoc.Extenstion, File_Type_Id = (long?)SZHPCMS.Common.FileTypes.Picture, Created_By = model.CreatedBy, Created_Date = DateTime.Now };
                    }
                    else
                    {
                        dbApp.Document1.File_Name = model.BlackBerryQRCodDoc.FileName;
                        dbApp.Document1.Extenstion = model.BlackBerryQRCodDoc.Extenstion;
                        dbApp.Document1.Updated_By = model.UpdatedBy;
                        dbApp.Document1.Updated_Date = DateTime.Now;
                    }
                }

                if (model.Document != null)
                {

                    if (dbApp.Document2 == null)
                    {
                        dbApp.Document2 = new Document() { File_Name = model.Document.FileName, Extenstion = model.Document.Extenstion, File_Type_Id = (long?)SZHPCMS.Common.FileTypes.Picture, Created_By = model.CreatedBy, Created_Date = DateTime.Now };
                    }
                    else
                    {
                        dbApp.Document2.File_Name = model.Document.FileName;
                        dbApp.Document2.Extenstion = model.Document.Extenstion;
                        dbApp.Document2.Updated_By = model.UpdatedBy;
                        dbApp.Document2.Updated_Date = DateTime.Now;
                    }
                }

                if (model.IPhoneQRCodeDoc != null)
                {

                    if (dbApp.Document3 == null)
                    {
                        dbApp.Document3 = new Document() { File_Name = model.IPhoneQRCodeDoc.FileName, Extenstion = model.IPhoneQRCodeDoc.Extenstion, File_Type_Id = (long?)SZHPCMS.Common.FileTypes.Picture, Created_By = model.CreatedBy, Created_Date = DateTime.Now };
                    }
                    else
                    {
                        dbApp.Document3.File_Name = model.IPhoneQRCodeDoc.FileName;
                        dbApp.Document3.Extenstion = model.IPhoneQRCodeDoc.Extenstion;
                        dbApp.Document3.Updated_By = model.UpdatedBy;
                        dbApp.Document3.Updated_Date = DateTime.Now;
                    }
                }

                if (model.WinQRCodeDoc != null)
                {

                    if (dbApp.Document4 == null)
                    {
                        dbApp.Document4 = new Document() { File_Name = model.WinQRCodeDoc.FileName, Extenstion = model.WinQRCodeDoc.Extenstion, File_Type_Id = (long?)SZHPCMS.Common.FileTypes.Picture, Created_By = model.CreatedBy, Created_Date = DateTime.Now };
                    }
                    else
                    {
                        dbApp.Document4.File_Name = model.WinQRCodeDoc.FileName;
                        dbApp.Document4.Extenstion = model.WinQRCodeDoc.Extenstion;
                        dbApp.Document4.Updated_By = model.UpdatedBy;
                        dbApp.Document4.Updated_Date = DateTime.Now;
                    }
                }


                return _uow.Save();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Delete Mobile app  object from data base . It should be soft delete i.e just update row status to delete
        /// </summary>
        /// <param name="menu">Mobile app object to delete</param>
        /// <returns></returns>
        public int DeleteMobileApp(int id)
        {
            Mobile_App dbApp = _uow.HomeSettingRepository.GetAppById(id);
            dbApp.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Delete;


            if (dbApp.Document != null)
                dbApp.Document.Row_Status_Id = (long?)RowStatus.Delete;


            if (dbApp.Document1 != null)
                dbApp.Document1.Row_Status_Id = (long?)RowStatus.Delete;


            if (dbApp.Document2 != null)
                dbApp.Document2.Row_Status_Id = (long?)RowStatus.Delete;


            if (dbApp.Document3 != null)
                dbApp.Document3.Row_Status_Id = (long?)RowStatus.Delete;


            if (dbApp.Document4 != null)
                dbApp.Document4.Row_Status_Id = (long?)RowStatus.Delete;


            return _uow.Save();
        }

        /// <summary>
        /// Update App's status .
        /// </summary>
        /// <param name="menus"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int UpdateRowStatus(IEnumerable<long> appList, RowStatus status)
        {
            foreach (var id in appList)
            {
                Mobile_App dbApp = _uow.HomeSettingRepository.GetAppById(id);
                dbApp.Row_Status_Id = (long?)status;
            }

            return _uow.Save();
        }
        #endregion

        #region Partner Service Region
        /// <summary>
        /// Get partner services
        /// </summary>
        /// <returns></returns>
        public List<PartnerServiceModel> GetPartnerServices(string dataFor = "")
        {
            return _uow.HomeSettingRepository.GetPartnerServices(dataFor).Select(x => new PartnerServiceModel()
            {
                PartnerServiceId = x.Partner_Service_Id,
                NameEn = x.Name_En,
                NameAr = x.Name_Ar,
                CreatedDate = x.Created_Date,
                Document = x.Document != null ? new DocumentModel() { DocumentId = x.Document.Document_Id, FileName = x.Document.File_Name, Extenstion = x.Document.Extenstion } : null,
                RowStatus = Enum.GetName(typeof(SZHPCMS.Common.RowStatus), x.Row_Status_Id),
                DocumentName = x.Document != null ? x.Document.File_Name : "N/A",
                Url = x.Url
                

            }).ToList();
        }

        /// <summary>
        /// Get partner service by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PartnerServiceModel GetPartnerServiceById(long id)
        {
            var dbPartnerService = _uow.HomeSettingRepository.GetPartnerServiceById(id);

            if (dbPartnerService == null)
                throw new Exception(SZHPCMS.Common.Constants.OBJECT_NOT_FOUND_MESSAGE + id as string);

            PartnerServiceModel model = new PartnerServiceModel();

            model.PartnerServiceId = dbPartnerService.Partner_Service_Id;
            model.NameAr = dbPartnerService.Name_Ar;
            model.NameEn = dbPartnerService.Name_En;
            model.Url = dbPartnerService.Url;
            model.Document = dbPartnerService.Document != null ? new DocumentModel() { DocumentId = dbPartnerService.Document.Document_Id, FileName = dbPartnerService.Document.File_Name, Extenstion = dbPartnerService.Document.Extenstion } : null;

            return model;
        }

        /// <summary>
        /// Add Partner Service
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public PartnerServiceModel AddPartnerService(PartnerServiceModel model)
        {
            if (model == null)
                throw new ArgumentNullException(SZHPCMS.Common.Constants.OBJECT_NULL_MESSAGE);

            try
            {
                Partner_Service dbService = new Partner_Service();
                dbService.Name_En = model.NameEn;
                dbService.Name_Ar = model.NameAr;
                dbService.Url = model.Url;
                dbService.Created_By = model.CreatedBy;
                dbService.Created_Date = DateTime.Now;
                dbService.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Active;

                dbService.Document = model.Document != null ? new Document() { File_Name = model.Document.FileName, File_Type_Id = (long?)SZHPCMS.Common.FileTypes.Picture, Created_By = model.CreatedBy, Created_Date = DateTime.Now, Extenstion = model.Document.Extenstion } : null;

                _uow.HomeSettingRepository.AddPartnerService(dbService);
                _uow.Save();

                model.PartnerServiceId = dbService.Partner_Service_Id;

                return model;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// update partner service
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdatePartnerService(PartnerServiceModel model)
        {
            if (model == null)
                throw new ArgumentNullException(SZHPCMS.Common.Constants.OBJECT_NULL_MESSAGE);

            var dbService = _uow.HomeSettingRepository.GetPartnerServiceById(model.PartnerServiceId);

            if (dbService == null)
                throw new Exception(SZHPCMS.Common.Constants.OBJECT_NOT_FOUND_MESSAGE + model.PartnerServiceId as string);

            dbService.Name_Ar = model.NameAr;
            dbService.Name_En = model.NameEn;
            dbService.Url = model.Url;
            dbService.Updated_By = model.UpdatedBy;
            dbService.Updated_Date = DateTime.Now;

            if (model.Document != null)
            {
                if (dbService.Document == null)
                {
                    dbService.Document = new Document();

                    dbService.Created_By = model.CreatedBy;
                    dbService.Created_Date = DateTime.Now;
                }
                else
                {
                    dbService.Updated_By = model.UpdatedBy;
                    dbService.Updated_Date = DateTime.Now;
                }

                dbService.Document.File_Name = model.Document.FileName;
                dbService.Document.Extenstion = model.Document.Extenstion;
            }

            return _uow.Save();

        }

        /// <summary>
        /// Delete Partner Service app  object from data base . It should be soft delete i.e just update row status to delete
        /// </summary>
        /// <param name="menu">Partner Service app object to delete</param>
        /// <returns></returns>
        public int DeletePartnerService(int id)
        {
            Partner_Service dbPartnerService = _uow.HomeSettingRepository.GetPartnerServiceById(id);
            dbPartnerService.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Delete;


            if (dbPartnerService.Document != null)
                dbPartnerService.Document.Row_Status_Id = (long?)RowStatus.Delete;

            return _uow.Save();
        }

        /// <summary>
        /// Update partner service status .
        /// </summary>
        /// <param name="menus"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int UpdatePartnerServiceRowStatus(IEnumerable<long> partServiceIDList, RowStatus status)
        {
            foreach (var id in partServiceIDList)
            {
                Partner_Service dbPartnerService = _uow.HomeSettingRepository.GetPartnerServiceById(id);
                dbPartnerService.Row_Status_Id = (long?)status;
            }

            return _uow.Save();
        }
        #endregion

    }
}
