using SZHPCMS.Common;
using DataAccess.CommonRespository;
using DataAccess.Database;
using DataContract.Implementation;
using DataContract.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.BusinessHandler
{
    /*
    public class BannerBH : ICommonHandler<BannerModel>
    {
        public List<BannerModel> GetAll(System.Linq.Expressions.Expression<Func<BannerModel, bool>> predicate = null)
        {
            return new List<BannerModel>();
        }

        public BannerModel Delete(int id)
        {
            throw new NotImplementedException();
        }

        public BannerModel FindBy(System.Linq.Expressions.Expression<Func<BannerModel, bool>> predicate = null)
        {
            throw new NotImplementedException();
        }

        public BannerModel Edit(BannerModel obj)
        {
            throw new NotImplementedException();
        }

        public BannerModel Insert(BannerModel obj)
        {
            return new BannerModel();
        }

    }
     */

    public class BannerBH
    {

        private readonly IUnitOfWork _uow;
        public BannerBH(UnitOfWork uow)
        {
            _uow = uow;
        }

        /// <summary>
        /// Get active banners
        /// </summary>
        /// <returns></returns>
        public List<BannerModel> GetActiveBanners()
        {
            return _uow.BannerRepository.GetActiveBanners().Select(x => new BannerModel()
            {
                BannerId = x.Banner_Id,
                TitleAr = x.Title_Ar,
                TitleEn = x.Title_En,
                CreatedDate = x.Created_Date,
                RowStatus = Enum.GetName(typeof(SZHPCMS.Common.RowStatus), x.Row_Status_Id)

            }).ToList();

        }

        /// <summary>
        /// Get banner by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BannerModel GetByID(long id)
        {
            // var obj = _uow.BannerRepository.GetBanner(1, 2);

            var dbBanner = _uow.BannerRepository.GetByID(id);

            if (dbBanner == null)
                throw new Exception("Banner not found.");

            BannerModel modelBanner = new BannerModel();

            modelBanner.BannerId = dbBanner.Banner_Id;
            modelBanner.TitleEn = dbBanner.Title_En;
            modelBanner.TitleAr = dbBanner.Title_Ar;
            modelBanner.CreatedDate = dbBanner.Created_Date;
            modelBanner.RowStatus = Enum.GetName(typeof(SZHPCMS.Common.RowStatus), dbBanner.Row_Status_Id);

            if (dbBanner.Banner_Documents.Count > 0)
            {
                modelBanner.Documents = new List<DocumentModel>();

                //   dbBanner.Banner_Documents = dbBanner.Banner_Documents.Where(x => x.Row_Status_Id != (long?)Common.RowStatus.Delete).ToList();

                foreach (var item in dbBanner.Banner_Documents)
                {
                    if (item.Document != null)
                    {
                        if (item.Document.Row_Status_Id != (long?)SZHPCMS.Common.RowStatus.Delete)
                        {
                            modelBanner.Documents.Add(new DocumentModel() { FileName = item.Document.File_Name, Extenstion = item.Document.Extenstion, DocumentId = item.Document.Document_Id });
                        }
                    }
                }
            }

            return modelBanner;
        }

        /// <summary>
        /// Add banner record in banner table.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public BannerModel Add(BannerModel model)
        {
            if (model == null)
                throw new ArgumentNullException("Banner model cannot be null .");

            Banner dbBanner = new Banner();

            dbBanner.Title_Ar = model.TitleAr;
            dbBanner.Title_En = model.TitleEn;
            dbBanner.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Active;
            dbBanner.Created_By = model.CreatedBy;
            dbBanner.Created_Date = DateTime.Now;

            if (model.Documents != null)
            {
                if (model.Documents.Count > 0)
                {
                    dbBanner.Banner_Documents = new List<Banner_Documents>();

                    foreach (var item in model.Documents)
                    {
                        Banner_Documents bannerDoc = new Banner_Documents();

                        bannerDoc.Created_By = model.CreatedBy;
                        bannerDoc.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Active;
                        bannerDoc.Created_Date = DateTime.Now;


                        bannerDoc.Document = new Document();

                        bannerDoc.Document.File_Name = item.FileName;
                        bannerDoc.Document.Extenstion = item.Extenstion;
                        bannerDoc.Document.Created_By = model.CreatedBy;
                        bannerDoc.Created_Date = DateTime.Now;
                        bannerDoc.Document.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Active;

                        dbBanner.Banner_Documents.Add(bannerDoc);
                    }
                }
            }

            _uow.BannerRepository.Add(dbBanner);
            _uow.Save();

            model.BannerId = dbBanner.Banner_Id;

            return model;
        }

        /// <summary>
        /// Update banner model in db .
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Update(BannerModel model)
        {
            if (model == null)
                throw new ArgumentNullException("Banner model cannot be null .");

            Banner dbBanner = _uow.BannerRepository.GetByID(model.BannerId);

            if (dbBanner == null)
                throw new Exception("Banner with id = " + model.BannerId + " was not found");

            int result = 0;

            dbBanner.Title_Ar = model.TitleAr;
            dbBanner.Title_En = model.TitleEn;
            dbBanner.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Active;
            dbBanner.Updated_By = model.UpdatedBy;
            dbBanner.Updated_Date = DateTime.Now;

            if (model.Documents != null)
            {
                if (model.Documents.Count > 0)
                {
                    foreach (var item in model.Documents)
                    {

                        Banner_Documents bannerDoc = new Banner_Documents();

                        bannerDoc.Created_By = model.CreatedBy;
                        bannerDoc.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Active;
                        bannerDoc.Created_Date = DateTime.Now;

                        bannerDoc.Document = new Document();

                        bannerDoc.Document.File_Name = item.FileName;
                        bannerDoc.Document.Extenstion = item.Extenstion;
                        bannerDoc.Document.File_Type_Id = (long?)SZHPCMS.Common.FileTypes.Picture;
                        bannerDoc.Document.Created_By = model.CreatedBy;
                        bannerDoc.Document.Created_Date = DateTime.Now;

                        dbBanner.Banner_Documents.Add(bannerDoc);

                    }

                }
            }

            result = _uow.Save();
            return result;
        }

        /// <summary>
        /// Delete banner from db . It will be a soft delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete(int id)
        {
            Banner dbBanner = _uow.BannerRepository.GetByID(id);

            dbBanner.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Delete;

            // Delete related images also .
            if (dbBanner.Banner_Documents.Count > 0)
            {
                foreach (var item in dbBanner.Banner_Documents)
                {
                    item.Document.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Delete;
                }
            }

            return _uow.Save();
        }

        /// <summary>
        /// Bulk update banner row status
        /// </summary>
        /// <param name="idList"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int UpdateRowStatus(IEnumerable<long> idList, RowStatus status)
        {
            foreach (var id in idList)
            {
                Banner dbBanner = _uow.BannerRepository.GetByID(id);
                dbBanner.Row_Status_Id = (long?)status;
            }

            return _uow.Save();
        }

    }
}
