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
    public class SiteMapBH
    {
        private readonly IUnitOfWork _uow;
        public SiteMapBH(IUnitOfWork uow)
        {
            _uow = uow;
        }

        #region Site Map Region

        /// <summary>
        /// Get active site maps
        /// </summary>
        /// <returns></returns>
        public List<SiteMapModel> GetActiveSiteMaps(out int totalRecords, long objectToExclude = 0, int pageIndex = 0, int pageSize = 10)
        {

            return _uow.SiteMapRepository.GetActiveSiteMaps(out totalRecords, objectToExclude, pageIndex, pageSize).Select(x => new SiteMapModel()
            {

                SiteMapId = x.Site_Map_Id,
                NameAr = x.Name_Ar,
                NameEn = x.Name_En,
                ParentName = x.Site_Map2 != null ? x.Site_Map2.Name_En : string.Empty,
                URL = x.URL,
                CreatedDate = x.Created_Date,
                RowStatus = Enum.GetName(typeof(SZHPCMS.Common.RowStatus), x.Row_Status_Id)

            }).ToList();
        }

        /// <summary>
        /// Get site map by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SiteMapModel GetSiteMapByID(long id)
        {
            var dbSiteMap = _uow.SiteMapRepository.GetByID(id);

            return new SiteMapModel()
            {
                SiteMapId = dbSiteMap.Site_Map_Id,
                NameAr = dbSiteMap.Name_Ar,
                NameEn = dbSiteMap.Name_En,
                URL = dbSiteMap.URL,
                ParentId = dbSiteMap.Parent_Id,
                ParentName = dbSiteMap.Site_Map2 != null ? dbSiteMap.Site_Map2.Name_En : string.Empty
            };
        }

        /// <summary>
        /// Add site map record in data base
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public SiteMapModel Add(SiteMapModel model)
        {
            if (model == null)
                throw new ArgumentNullException(SZHPCMS.Common.Constants.OBJECT_NULL_MESSAGE);

            try
            {
                Site_Map dbSiteMap = new Site_Map();
                dbSiteMap.Name_En = model.NameEn;
                dbSiteMap.Name_Ar = model.NameAr;
                dbSiteMap.URL = model.URL;
                dbSiteMap.Created_By = model.CreatedBy;
                dbSiteMap.Created_Date = DateTime.Now;
                dbSiteMap.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Active;
                dbSiteMap.Parent_Id = model.ParentId;

                _uow.SiteMapRepository.Add(dbSiteMap);
                _uow.Save();

                model.SiteMapId = dbSiteMap.Site_Map_Id;
            }
            catch (Exception ex)
            {
                throw;
            }

            return model;
        }

        /// <summary>
        /// Update site map record in data base
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public int Update(SiteMapModel model)
        {
            if (model == null)
                throw new ArgumentNullException(SZHPCMS.Common.Constants.OBJECT_NULL_MESSAGE);

            var dbSiteMap = _uow.SiteMapRepository.GetByID(model.SiteMapId);

            if (dbSiteMap == null)
                throw new Exception(SZHPCMS.Common.Constants.OBJECT_NOT_FOUND_MESSAGE + model.SiteMapId as string);

            try
            {
                dbSiteMap.Name_En = model.NameEn;
                dbSiteMap.Name_Ar = model.NameAr;
                dbSiteMap.URL = model.URL;
                dbSiteMap.Updated_By = model.UpdatedBy;
                dbSiteMap.Updated_Date = DateTime.Now;
                dbSiteMap.Parent_Id = model.ParentId;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _uow.Save();

        }


        /// <summary>
        /// Delete site map object from data base
        /// </summary>
        /// <param name="menu">Menu object to delete</param>
        /// <returns></returns>
        public int Delete(int id)
        {
            var dbSiteMap = _uow.SiteMapRepository.GetByID(id);
            dbSiteMap.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Delete;

            return _uow.Save();
        }

        /// <summary>
        /// Update menus status .
        /// </summary>
        /// <param name="menus"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int UpdateRowStatus(IEnumerable<long> idList, RowStatus status)
        {
            foreach (var id in idList)
            {
                var dbSiteMap = _uow.SiteMapRepository.GetByID(id);
                dbSiteMap.Row_Status_Id = (long?)status;
            }

            return _uow.Save();
        }
        #endregion

        #region Help Region

        /// <summary>
        /// Get active help
        /// </summary>
        /// <returns></returns>
        public List<HelpModel> GetActiveHelp(string dataFor = "")
        {
            return _uow.SiteMapRepository.GetActiveHelp(dataFor).Select(x => new HelpModel()
            {

                HelpId = x.Help_Id,
                TitleAr = x.Title_Ar,
                TitleEn = x.Title_En,
                DescriptionAr = x.Description_Ar,
                DescriptionEn = x.Description_En,
                CreatedDate = x.Created_Date,
                RowStatus = Enum.GetName(typeof(SZHPCMS.Common.RowStatus), x.Row_Status_Id)

            }).ToList();
        }

        /// <summary>
        /// Get help by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HelpModel GetHelpByID(long id)
        {
            var dbSiteMap = _uow.SiteMapRepository.GetHelpByID(id);

            return new HelpModel()
            {
                HelpId = dbSiteMap.Help_Id,
                TitleEn = dbSiteMap.Title_En,
                TitleAr = dbSiteMap.Title_Ar,
                DescriptionEn = dbSiteMap.Description_En,
                DescriptionAr = dbSiteMap.Description_Ar,

            };
        }

        /// <summary>
        /// Add help record in data base
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public HelpModel AddHelp(HelpModel model)
        {
            if (model == null)
                throw new ArgumentNullException(SZHPCMS.Common.Constants.OBJECT_NULL_MESSAGE);

            try
            {
                Help dbHelp = new Help();
                dbHelp.Title_En = model.TitleEn;
                dbHelp.Title_Ar = model.TitleAr;
                dbHelp.Description_En = model.DescriptionEn;
                dbHelp.Description_Ar = model.DescriptionAr;
                dbHelp.Created_Date = DateTime.Now;
                dbHelp.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Active;
                dbHelp.Created_By = model.CreatedBy;

                _uow.SiteMapRepository.AddHelp(dbHelp);
                _uow.Save();

                model.HelpId = dbHelp.Help_Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return model;
        }

        /// <summary>
        /// Update help record in data base
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public int UpdateHelp(HelpModel model)
        {
            if (model == null)
                throw new ArgumentNullException(SZHPCMS.Common.Constants.OBJECT_NULL_MESSAGE);

            var dbHelp = _uow.SiteMapRepository.GetHelpByID(model.HelpId);

            if (dbHelp == null)
                throw new Exception(SZHPCMS.Common.Constants.OBJECT_NOT_FOUND_MESSAGE + model.HelpId as string);

            try
            {
                dbHelp.Title_En = model.TitleEn;
                dbHelp.Title_Ar = model.TitleAr;
                dbHelp.Description_En = model.DescriptionEn;
                dbHelp.Description_Ar = model.DescriptionAr;
                dbHelp.Updated_Date = DateTime.Now;
                dbHelp.Updated_By = model.UpdatedBy;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _uow.Save();

        }


        /// <summary>
        /// Delete help object from data base
        /// </summary>
        /// <param name="menu">Help object to delete</param>
        /// <returns></returns>
        public int DeleteHelp(int id)
        {
            var dbHelp = _uow.SiteMapRepository.GetHelpByID(id);
            dbHelp.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Delete;

            return _uow.Save();
        }

        /// <summary>
        /// Update help status .
        /// </summary>
        /// <param name="menus"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int UpdateRowStatusHelp(IEnumerable<long> idList, RowStatus status)
        {
            foreach (var id in idList)
            {
                var dbHelp = _uow.SiteMapRepository.GetHelpByID(id);
                dbHelp.Row_Status_Id = (long?)status;
            }

            return _uow.Save();
        }
        #endregion

    }
}
