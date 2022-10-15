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
    public class GalleryBH
    {
        private readonly IUnitOfWork _uow;
        public GalleryBH(IUnitOfWork uow)
        {
            _uow = uow;
        }

        /// <summary>
        /// Get active gallary i.e gallaries which are not deleted.
        /// </summary>
        /// <returns></returns>
        public List<GalleryModel> GetActiveGalleries()
        {

            var modelGalleries = _uow.GalleryRepository.GetActiveGalleries().Select(x => new GalleryModel()
            {

                GalleryId = x.Gallery_Id,
                TitleAr = x.Title_Ar,
                TitleEn = x.Title_En,
                RowStatus = Enum.GetName(typeof(RowStatus), x.Row_Status_Id)

            }).ToList();

            return modelGalleries;

        }



        /// <summary>
        /// Get gallery by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public GalleryModel GetByID(long id)
        {
            var dbGallery = _uow.GalleryRepository.GetByID(id);

            if (dbGallery == null)
                throw new Exception(Constants.OBJECT_NOT_FOUND_MESSAGE + id.ToString());

            GalleryModel modelGallery = new GalleryModel();
            modelGallery.GalleryId = dbGallery.Gallery_Id;
            modelGallery.TitleAr = dbGallery.Title_Ar;
            modelGallery.TitleEn = dbGallery.Title_En;

            if (dbGallery.Document != null)
            {
                modelGallery.Document = new DocumentModel();

                modelGallery.Document.FileName = dbGallery.Document.File_Name;
                modelGallery.Document.Extenstion = dbGallery.Document.Extenstion;
                modelGallery.Document.DocumentId = dbGallery.Document.Document_Id;
            }

            return modelGallery;
        }


        /// <summary>
        /// Add new gallery obj into db.
        /// </summary>
        /// <param name="news">Gallery object to add</param>
        /// <returns></returns>
        public GalleryModel Add(GalleryModel galleryModel)
        {
            if (galleryModel == null)
                throw new ArgumentNullException(Constants.OBJECT_NULL_MESSAGE);

            try
            {
                Gallery dbGallery = new Gallery();

                dbGallery.Title_En = galleryModel.TitleEn;
                dbGallery.Title_Ar = galleryModel.TitleAr;
                dbGallery.Created_By = galleryModel.CreatedBy;
                dbGallery.Row_Status_Id = (long?)RowStatus.Active;
                dbGallery.Created_Date = DateTime.Now;

                if (galleryModel.Document != null)
                {
                    dbGallery.Document = new Document();

                    dbGallery.Document.File_Name = galleryModel.Document.FileName;
                    dbGallery.Document.Extenstion = galleryModel.Document.Extenstion;
                    dbGallery.Created_By = galleryModel.CreatedBy;
                }

                _uow.GalleryRepository.Add(dbGallery);
                _uow.Save();

                galleryModel.GalleryId = dbGallery.Gallery_Id;

                return galleryModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Update gallary in data base
        /// </summary>
        /// <param name="news">gallary object to update</param>
        /// <returns>Number of rows effected</returns>
        public int Update(GalleryModel modelGallery)
        {
            if (modelGallery == null)
                throw new ArgumentNullException(Constants.OBJECT_NULL_MESSAGE);

            try
            {
                Gallery dbGallery = _uow.GalleryRepository.GetByID(modelGallery.GalleryId);

                if (dbGallery == null)
                    throw new Exception(Constants.OBJECT_NOT_FOUND_MESSAGE + modelGallery.DocumentId.ToString());


                dbGallery.Title_En = modelGallery.TitleEn;
                dbGallery.Title_Ar = modelGallery.TitleAr;
                dbGallery.Updated_By = modelGallery.UpdatedBy;
                dbGallery.Updated_Date = DateTime.Now;
                dbGallery.Row_Status_Id = (long?)RowStatus.Active;

                if (modelGallery.Document != null)
                {
                    if (dbGallery.Document == null)
                    {
                        dbGallery.Document = new Document();
                        dbGallery.Document.Created_By = modelGallery.CreatedBy;
                    }
                    else
                    {
                        dbGallery.Document.Updated_By = modelGallery.UpdatedBy;
                        dbGallery.Document.Updated_Date = DateTime.Now;
                    }


                    dbGallery.Document.File_Name = modelGallery.Document.FileName;
                    dbGallery.Document.Extenstion = modelGallery.Document.Extenstion;
                    dbGallery.Created_By = modelGallery.CreatedBy;
                    dbGallery.Created_Date = DateTime.Now;
                }


                int rowsEffected = _uow.Save();

                return rowsEffected;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Delete gallary object from data base . It should be soft delete i.e just update row status to delete
        /// </summary>
        /// <param name="menu">Gallary object to delete</param>
        /// <returns></returns>
        public int Delete(int id)
        {
            try
            {
                Gallery dbGallery = _uow.GalleryRepository.GetByID(id);

                if (dbGallery == null)
                    throw new Exception(Constants.OBJECT_NOT_FOUND_MESSAGE + id as string);

                dbGallery.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Delete;

                if (dbGallery.Document != null)
                    dbGallery.Document.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Delete;

                return _uow.Save();

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        /// <summary>
        /// Update Galleries status .
        /// </summary>
        /// <param name="menus"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int UpdateRowStatus(IEnumerable<long> eventIDList, RowStatus status)
        {
            try
            {
                bool anyObjectFound = false;

                foreach (var id in eventIDList)
                {
                    Gallery dbGallery = _uow.GalleryRepository.GetByID(id);

                    if (dbGallery != null)
                    {
                        anyObjectFound = true;

                        dbGallery.Row_Status_Id = (long?)status;

                        if (status == RowStatus.Delete)
                        {
                            if (dbGallery.Document != null)
                            {
                                dbGallery.Document.Row_Status_Id = (long?)RowStatus.Delete;
                            }
                        }
                    }
                }

                return anyObjectFound ? _uow.Save() : 0;


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
