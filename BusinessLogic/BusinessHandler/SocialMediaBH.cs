using DataAccess.CommonRespository;
using DataAccess.Database;
using DataContract.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SZHPCMS.Common;

namespace BusinessLogic.BusinessHandler
{
    public class SocialMediaBH
    {
        private IUnitOfWork _uow;

        public SocialMediaBH(IUnitOfWork uow)
        {
            _uow = uow;
        }

        /// <summary>
        /// Get social media items based on param isadmin
        /// </summary>
        /// <param name="isAdmin"></param>
        /// <returns></returns>
        public List<SocialMediaModel> GetActiveSocialMedia(bool isAdmin = true)
        {
            return _uow.SocialRepository.GetActiveSocialMedia(isAdmin).Select(x => new SocialMediaModel()
            {
                Social_Media_Id = x.Social_Media_Id,
                Title_En = x.Title_En,
                Title_Ar = x.Title_Ar,
                Tool_Tip_En = x.Tool_Tip_En,
                Tool_Tip_Ar = x.Tool_Tip_Ar,
                Row_Status_Id = x.Row_Status_Id,
                Created_Date = x.Created_Date,
                Url = x.Url,
                RowStatus = Enum.GetName(typeof(SZHPCMS.Common.RowStatus), x.Row_Status_Id),
                Document = x.Document != null ? new DocumentModel() { FileName = x.Document.File_Name, DocumentId = x.Document.Document_Id } : null,
                Created_By = x.Created_By,



            }).ToList();
        }

        /// <summary>
        /// Get social media by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SocialMediaModel GetSocialMediaByID(long id)
        {
            var dbSocialMedia = _uow.SocialRepository.GetByID(id);

            if (dbSocialMedia == null)
                throw new Exception(SZHPCMS.Common.Constants.OBJECT_NOT_FOUND_MESSAGE + "id=" + id);

            SocialMediaModel modelSocialMedia = new SocialMediaModel();

            modelSocialMedia.Social_Media_Id = dbSocialMedia.Social_Media_Id;
            modelSocialMedia.Title_Ar = dbSocialMedia.Title_Ar;
            modelSocialMedia.Title_En = dbSocialMedia.Title_En;
            modelSocialMedia.Tool_Tip_Ar = dbSocialMedia.Tool_Tip_Ar;
            modelSocialMedia.Tool_Tip_En = dbSocialMedia.Tool_Tip_En;
            modelSocialMedia.Url = dbSocialMedia.Url;

            if (dbSocialMedia.Document != null)
            {
                if (dbSocialMedia.Document.Row_Status_Id == (long?)SZHPCMS.Common.RowStatus.Active)
                {
                    modelSocialMedia.Document = new DocumentModel() { DocumentId = dbSocialMedia.Document.Document_Id, FileName = dbSocialMedia.Document.File_Name };
                }

            }

            modelSocialMedia.Created_Date = dbSocialMedia.Created_Date;
            modelSocialMedia.Created_By = dbSocialMedia.Created_By;
            modelSocialMedia.Update_Date = dbSocialMedia.Update_Date;
            modelSocialMedia.Updated_By = dbSocialMedia.Updated_By;

            return modelSocialMedia;
        }

        /// <summary>
        /// Add social media object to database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public SocialMediaModel AddSocialMedia(SocialMediaModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(SZHPCMS.Common.Constants.OBJECT_NULL_MESSAGE);
            }

            Social_Media dbSocialMedia = new Social_Media();
            dbSocialMedia.Title_Ar = model.Title_Ar;
            dbSocialMedia.Title_En = model.Title_En;
            dbSocialMedia.Tool_Tip_Ar = model.Tool_Tip_Ar;
            dbSocialMedia.Tool_Tip_En = model.Tool_Tip_En;
            dbSocialMedia.Url = model.Url;
            dbSocialMedia.Created_Date = DateTime.Now;
            dbSocialMedia.Created_By = model.Created_By;
            dbSocialMedia.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Active;

            if (model.Document != null)
            {
                dbSocialMedia.Document = new Document() { File_Name = model.Document.FileName, Extenstion = model.Document.Extenstion, File_Type_Id = (long?)SZHPCMS.Common.FileTypes.Picture, Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Active };
            }

            _uow.SocialRepository.Add(dbSocialMedia);

            try
            {
                _uow.Save();

                model.Social_Media_Id = dbSocialMedia.Social_Media_Id;
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return model;
        }

        /// <summary>
        /// Update social media and associated document
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateSocialMedia(SocialMediaModel model)
        {

            var dbSocialMedia = _uow.SocialRepository.GetByID(model.Social_Media_Id);

            if (dbSocialMedia == null)
                throw new Exception(SZHPCMS.Common.Constants.OBJECT_NOT_FOUND_MESSAGE);

            int numOfRecords = 0;

            try
            {
                dbSocialMedia.Title_Ar = model.Title_Ar;
                dbSocialMedia.Title_En = model.Title_En;
                dbSocialMedia.Tool_Tip_Ar = model.Tool_Tip_Ar;
                dbSocialMedia.Tool_Tip_En = model.Tool_Tip_En;
                dbSocialMedia.Url = model.Url;
                dbSocialMedia.Update_Date = DateTime.Now;
                dbSocialMedia.Updated_By = model.Created_By;

                if (model.Document != null)
                {
                    dbSocialMedia.Document.File_Name = model.Document.FileName;
                    dbSocialMedia.Document.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Active;
                }

                numOfRecords = _uow.Save();

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return numOfRecords;

        }

        /// <summary>
        /// Delete Social Media object from data base . It should be soft delete i.e just update row status to delete
        /// </summary>
        /// <param name="menu">FAQ object to delete</param>
        /// <returns></returns>
        public int Delete(int id)
        {
            Social_Media dbObject = _uow.SocialRepository.GetByID(id);

            dbObject.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Delete;

            if (dbObject.Document != null)
            {
                dbObject.Document.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Delete;
            }

            return _uow.Save();
        }

        /// <summary>
        /// Update social media status .
        /// </summary>
        /// <param name="menus"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int UpdateRowStatus(IEnumerable<long> ids, RowStatus status)
        {
            foreach (var id in ids)
            {
                Social_Media dbObject = _uow.SocialRepository.GetByID(id);

                dbObject.Row_Status_Id = (long?)status;

                if (dbObject.Document != null)
                {
                    dbObject.Document.Row_Status_Id = (long?)status;
                }
            }

            return _uow.Save();
        }
    }
}
