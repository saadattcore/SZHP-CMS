using BusinessLogic.BusinessObjects;
using DataAccess.CommonRespository;
using DataAccess.Repository;
using DataContract.Implementation;
using DataContract.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Db = DataAccess.Database;

namespace BusinessLogic.BusinessHandler
{
    public class SettingBH
    {
        private readonly IUnitOfWork _uow;

        /// <summary>
        /// ctor
        /// </summary>
        public SettingBH()
        {
            _uow = new UnitOfWork();
        }

        /// <summary>
        /// Get first setting object.
        /// </summary>
        /// <returns></returns>
        public SettingModel GetSiteSetting()
        {
            var dbSetting = _uow.SettingRepository.GetFirst();

            if (dbSetting == null)
                return null;

            SettingModel objSetting = new SettingModel()
            {
                ClientNameArabic = dbSetting.Cleint_Name_Ar,
                ClientNameEnglish = dbSetting.Client_Name_En,
                CMSTitleEnglish = dbSetting.CMS_Title_En,
                CMSTitleArabic = dbSetting.CMS_Title_Ar,
                DocumentId = dbSetting.Document_Id.GetValueOrDefault(),
                Email = dbSetting.Email,
                FaceBook = dbSetting.Facebook,
                Id = dbSetting.Setting_Id,
                Twitter = dbSetting.Twitter,
                Website = dbSetting.Website,
                

            };

            if (dbSetting.Document != null)
            {
                objSetting.DocumentName = dbSetting.Document.File_Name;
            }

            return objSetting;
        }

        /// <summary>
        /// Update model and also update corresponsing logo (document)
        /// </summary>
        /// <param name="model"></param>
        /// <returns>newly added id</returns>
        public int Update(ref SettingModel model)
        {
            if (model == null)
                throw new ArgumentNullException("Object is null");

            var dbSetting = _uow.SettingRepository.GetFirst();

            bool isAdd = false;

            if (dbSetting == null)
            {
                dbSetting = new Db.Setting();
                isAdd = true;
            }


            dbSetting.Cleint_Name_Ar = model.ClientNameArabic;
            dbSetting.Client_Name_En = model.ClientNameEnglish;
            dbSetting.CMS_Title_Ar = model.CMSTitleArabic;
            dbSetting.CMS_Title_En = model.CMSTitleEnglish;
            dbSetting.Email = model.Email;
            dbSetting.Facebook = model.FaceBook;
            dbSetting.Twitter = model.Twitter;
            dbSetting.Website = model.Website;
            dbSetting.Updated_By = model.UpdatedBy;
            dbSetting.Updated_Date = System.DateTime.Now;

            if (model.Document != null)
            {

                if (dbSetting.Document == null)
                {
                    dbSetting.Document = new Db.Document();
                }

                dbSetting.Document.File_Name = model.Document.FileName;
                dbSetting.Document.Extenstion = model.Document.Extenstion;
                dbSetting.Document.File_Type_Id = (long?)SZHPCMS.Common.FileTypes.Picture;
            }

            if (isAdd)
            {
                _uow.SettingRepository.Add(dbSetting);
            }

            int rowsEff =  _uow.Save();

            model.Id = dbSetting.Setting_Id;

            return rowsEff;

        }
    }
}
