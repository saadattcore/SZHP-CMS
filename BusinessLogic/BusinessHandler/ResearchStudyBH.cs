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
    public class ResearchStudyBH
    {
        private readonly IUnitOfWork _uow;

        public ResearchStudyBH(UnitOfWork uow)
        {
            _uow = uow;
        }

        /// <summary>
        /// Get active research studies
        /// </summary>
        /// <returns></returns>
        public List<ResearchStudyModel> GetActiveResearchStudies()
        {
            return _uow.ResearchStudyRepository.GetActiveResearchStudies().Select(x => new ResearchStudyModel()
             {
                 ResearchStudyId = x.Research_Study_Id,
                 TitleAr = x.Title_Ar,
                 TitleEn = x.Title_En,
                 DescriptionAr = x.Description_Ar,
                 DescriptionEn = x.Description_En,
                 ThemeAr = x.Theme_Ar,
                 ThemeEn = x.Theme_En,
                 CreatedDate = x.Created_Date,
                 CategoryName = x.Research_Study_Category != null ? x.Research_Study_Category.Name_En : string.Empty,
                 RowStatus = Enum.GetName(typeof(RowStatus), x.Row_Status_Id)


             }).ToList();
        }

        /// <summary>
        /// Get research study object by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResearchStudyModel GetByID(long id)
        {
            var dbResearchStudy = _uow.ResearchStudyRepository.GetByID(id);

            if (dbResearchStudy == null)
                throw new Exception(SZHPCMS.Common.Constants.OBJECT_NOT_FOUND_MESSAGE + id.ToString());

            var modelResearchStudy = new ResearchStudyModel()
            {
                ResearchStudyId = dbResearchStudy.Research_Study_Id,
                TitleAr = dbResearchStudy.Title_Ar,
                TitleEn = dbResearchStudy.Title_En,
                DescriptionAr = dbResearchStudy.Description_Ar,
                DescriptionEn = dbResearchStudy.Description_En,
                ThemeEn = dbResearchStudy.Theme_En,
                ThemeAr = dbResearchStudy.Theme_Ar,
                CategoryName = dbResearchStudy.Research_Study_Category != null ? dbResearchStudy.Research_Study_Category.Name_En : string.Empty,
                RowStatus = Enum.GetName(typeof(SZHPCMS.Common.RowStatus), dbResearchStudy.Row_Status_Id),
                ResearchStudyCategoryId = dbResearchStudy.Research_Study_Category_Id

            };

            return modelResearchStudy;
        }

        /// <summary>
        /// Add research study 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ResearchStudyModel Add(ResearchStudyModel model)
        {
            if (model == null)
                throw new ArgumentException(SZHPCMS.Common.Constants.OBJECT_NULL_MESSAGE);

            Research_Study dbResearchStudy = new Research_Study();

            dbResearchStudy.Title_Ar = model.TitleAr;
            dbResearchStudy.Title_En = model.TitleEn;
            dbResearchStudy.Description_En = model.DescriptionEn;
            dbResearchStudy.Description_Ar = model.DescriptionAr;
            dbResearchStudy.Theme_Ar = model.ThemeAr;
            dbResearchStudy.Theme_En = model.ThemeEn;
            dbResearchStudy.Created_By = model.CreatedBy;
            dbResearchStudy.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Active;
            dbResearchStudy.Created_Date = DateTime.Now;

            if (model.ResearchStudyCategoryId > 0)
            {
                dbResearchStudy.Research_Study_Category_Id = model.ResearchStudyCategoryId;
            }

            _uow.ResearchStudyRepository.Add(dbResearchStudy);
            _uow.Save();

            model.ResearchStudyId = dbResearchStudy.Research_Study_Id;

            return model;

        }

        /// <summary>
        /// Update research study object
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Update(ResearchStudyModel model)
        {
            var dbResearchStudy = _uow.ResearchStudyRepository.GetByID(model.ResearchStudyId);

            if (dbResearchStudy == null)
                throw new ArgumentNullException(SZHPCMS.Common.Constants.OBJECT_NOT_FOUND_MESSAGE + model.ResearchStudyId.ToString());

            try
            {
                dbResearchStudy.Title_Ar = model.TitleAr;
                dbResearchStudy.Title_En = model.TitleEn;
                dbResearchStudy.Description_Ar = model.DescriptionAr;
                dbResearchStudy.Description_En = model.DescriptionEn;
                dbResearchStudy.Theme_Ar = model.ThemeAr;
                dbResearchStudy.Theme_En = model.ThemeEn;
                dbResearchStudy.Updated_By = model.UpdatedBy;
                dbResearchStudy.Updated_Date = DateTime.Now;
                dbResearchStudy.Research_Study_Category_Id = model.ResearchStudyCategoryId;

                return _uow.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Delete Research study object from data base . It should be soft delete i.e just update row status to delete
        /// </summary>
        /// <param name="menu">Research study object to delete</param>
        /// <returns></returns>
        public int Delete(long id)
        {
            Research_Study dbResearchStudy = _uow.ResearchStudyRepository.GetByID(id);
            dbResearchStudy.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Delete;

            return _uow.Save();
        }

        /// <summary>
        /// Update research studies status .
        /// </summary>
        /// <param name="menus"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int UpdateRowStatus(IEnumerable<long> idList, RowStatus status)
        {
            foreach (var id in idList)
            {
                Research_Study dbResearchStudy = _uow.ResearchStudyRepository.GetByID(id);

                dbResearchStudy.Row_Status_Id = (long?)status;
            }

            return _uow.Save();
        }

        /// <summary>
        /// Get research study categories
        /// </summary>
        /// <returns></returns>
        public List<ResearchStudyCategoryModel> GetCategories()
        {
            return _uow.ResearchStudyRepository.GetResearchStudiesCategories().Select(x => new ResearchStudyCategoryModel()
            {

                ResearchStudyCategoryId = x.Research_Study_Category_Id,
                NameAr = x.Name_Ar,
                NameEn = x.Name_En

            }).ToList();

        }

    }
}
