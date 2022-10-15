using DataAccess.CommonRespository;
using DataAccess.Database;
using DataContract.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class SettingRepository : GenericRepository<Setting>, ISettingRepository
    {

        public SettingRepository(ITJCMSEntities context)
            : base(context)
        {

        }

        /*
          public List<SettingModel> GetAllSettings()
          {
              //var settingList = uow.PropSetting.GetAll().ToList();
              //return settingList;
              var settingList = uow.PropSetting.GetAll().Select(x => new SettingModel
              {
                  ClientNameArabic = x.Cleint_Name_Ar,
                  ClientNameEnglish = x.Client_Name_En,
                  DocumentId = x.Document_Id.GetValueOrDefault(),
                  Email = x.Email,
                  FaceBook = x.Facebook,
                  Id = x.Document_Id.GetValueOrDefault(),
                  Twitter = x.Twitter,
                  Website = x.Website,
              }).ToList();
              return settingList;
          }

          public SettingModel SaveSetting(SettingModel obj, DocumentModel objDoc)
          {
              ////uow.PropSetting.Add(objSetting);
              ////uow.Save();
              ////return objSetting;
              Document entityDocument = new Document
              {
                  File_Name = objDoc.FileName,
                  Extenstion = objDoc.Extenstion,
                  File_Type_Id = objDoc.FileTypeId
              };

              Setting entity = new Setting
              {
                  Cleint_Name_Ar = obj.ClientNameArabic,
                  Client_Name_En = obj.ClientNameEnglish,
                  Email = obj.Email,
                  Facebook = obj.FaceBook,
                  Twitter = obj.Twitter,
                  Website = obj.Website,
                  Document = entityDocument
              };
              uow.PropSetting.Add(entity);
              uow.Save();
              obj.Id = entity.Setting_Id;
              return obj;
          }

          */

        public Setting GetFirst()
        {
            return this._context.Settings.FirstOrDefault();
        }
    }
}
