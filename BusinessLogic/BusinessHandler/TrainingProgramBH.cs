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
    public class TrainingProgramBH
    {
        private readonly IUnitOfWork _uow;
        public TrainingProgramBH(UnitOfWork uow)
        {
            _uow = uow;
        }

        /// <summary>
        /// Get active training plans objects
        /// </summary>
        /// <returns></returns>
        public List<TrainingPlanModel> GetActiveTrainingPlans()
        {
            var dbTrainingPlan = _uow.TraningPlanRepository.GetActivePlans();

            var mdTrainingPlans = new List<TrainingPlanModel>();

            foreach (var item in dbTrainingPlan)
            {
                var objTrainingPlan = new TrainingPlanModel();

                objTrainingPlan.TrainingPlanId = item.Training_Plan_Id;
                objTrainingPlan.LocationAr = item.Location_Ar;
                objTrainingPlan.LocationEn = item.Location_En;
                objTrainingPlan.NotesAr = item.Notes_Ar;
                objTrainingPlan.NotesEn = item.Notes_En;
                objTrainingPlan.ThemeAr = item.Theme_Ar;
                objTrainingPlan.ThemeEn = item.Theme_En;
                objTrainingPlan.AudienceAr = item.Audience_Ar;
                objTrainingPlan.AudienceEn = item.Audience_En;
                objTrainingPlan.RowStatusId = item.Row_Status_Id;
                objTrainingPlan.RowStatus = Enum.GetName(typeof(RowStatus), item.Row_Status_Id);
                objTrainingPlan.Date = item.Date;
                objTrainingPlan.TrainingProgramId = item.Training_Program_Id;
                objTrainingPlan.TrainingProgram = item.Training_Program != null ? item.Training_Program.Name_En : string.Empty;

                mdTrainingPlans.Add(objTrainingPlan);

            }

            return mdTrainingPlans;
        }

        /// <summary>
        /// Get training program object by id 
        /// </summary>
        /// <returns></returns>
        public TrainingPlanModel GetByID(long id)
        {
            var dbTrainingPlan = _uow.TraningPlanRepository.GetByID(id);

            var objTrainingPlan = new TrainingPlanModel();

            objTrainingPlan.TrainingPlanId = dbTrainingPlan.Training_Plan_Id;
            objTrainingPlan.LocationAr = dbTrainingPlan.Location_Ar;
            objTrainingPlan.LocationEn = dbTrainingPlan.Location_En;
            objTrainingPlan.NotesAr = dbTrainingPlan.Notes_Ar;
            objTrainingPlan.NotesEn = dbTrainingPlan.Notes_En;
            objTrainingPlan.ThemeAr = dbTrainingPlan.Theme_Ar;
            objTrainingPlan.ThemeEn = dbTrainingPlan.Theme_En;
            objTrainingPlan.AudienceAr = dbTrainingPlan.Audience_Ar;
            objTrainingPlan.AudienceEn = dbTrainingPlan.Audience_En;
            objTrainingPlan.RowStatusId = dbTrainingPlan.Row_Status_Id;
            objTrainingPlan.RowStatus = Enum.GetName(typeof(SZHPCMS.Common.RowStatus), dbTrainingPlan.Row_Status_Id);
            objTrainingPlan.Date = dbTrainingPlan.Date;
            objTrainingPlan.TrainingProgramId = dbTrainingPlan.Training_Program_Id;

            return objTrainingPlan;
        }

        /// <summary>
        /// Add traning plan object into db 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public TrainingPlanModel Add(TrainingPlanModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model is null");

            var dbTraningPlan = new Training_Plan();

            try
            {

                dbTraningPlan.Location_Ar = model.LocationAr;
                dbTraningPlan.Location_En = model.LocationEn;
                dbTraningPlan.Notes_Ar = model.NotesAr;
                dbTraningPlan.Notes_En = model.NotesEn;
                dbTraningPlan.Theme_Ar = model.ThemeAr;
                dbTraningPlan.Theme_En = model.ThemeEn;
                dbTraningPlan.Audience_Ar = model.AudienceAr;
                dbTraningPlan.Audience_En = model.AudienceEn;
                dbTraningPlan.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Active;
                dbTraningPlan.Created_By = model.CreatedBy;
                dbTraningPlan.Date = model.Date;
                dbTraningPlan.Training_Program_Id = model.TrainingProgramId;
                dbTraningPlan.Created_Date = DateTime.Now;

                _uow.TraningPlanRepository.Add(dbTraningPlan);
                _uow.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return model;
        }

        /// <summary>
        /// Update traning plan object
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Update(TrainingPlanModel model)
        {
            var dbTraningPlan = _uow.TraningPlanRepository.GetByID(model.TrainingPlanId);

            if (dbTraningPlan == null)
                throw new Exception("Traning plan object not found with ID = " + model.TrainingPlanId);

            int result = 0;

            try
            {
                dbTraningPlan.Location_Ar = model.LocationAr;
                dbTraningPlan.Location_En = model.LocationEn;
                dbTraningPlan.Notes_Ar = model.NotesAr;
                dbTraningPlan.Notes_En = model.NotesEn;
                dbTraningPlan.Theme_Ar = model.ThemeAr;
                dbTraningPlan.Theme_En = model.ThemeEn;
                dbTraningPlan.Audience_Ar = model.AudienceAr;
                dbTraningPlan.Audience_En = model.AudienceEn;
                dbTraningPlan.Date = model.Date;
                dbTraningPlan.Training_Program_Id = model.TrainingProgramId;

                dbTraningPlan.Updated_By = model.UpdatedBy;
                dbTraningPlan.Updated_Date = DateTime.Now;

                result = _uow.Save();
            }
            catch (Exception)
            {
                throw;
            }

            return result;

        }

        /// <summary>
        /// Delete Traning Plan object from data base . It should be soft delete i.e just update row status to delete
        /// </summary>
        /// <param name="menu">Traning plan object to delete</param>
        /// <returns></returns>
        public int Delete(int id)
        {
            Training_Plan dbTrainingPlan = _uow.TraningPlanRepository.GetByID(id);
            dbTrainingPlan.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Delete;

            return _uow.Save();
        }

        /// <summary>
        /// Update Traning Plan status .
        /// </summary>
        /// <param name="menus"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int UpdateRowStatus(IEnumerable<long> idList, RowStatus status)
        {
            foreach (var id in idList)
            {
                Training_Plan dbTrainingPlan = _uow.TraningPlanRepository.GetByID(id);
                dbTrainingPlan.Row_Status_Id = (long?)status;
            }

            return _uow.Save();
        }

        /// <summary>
        /// Get all traning programs
        /// </summary>
        /// <returns></returns>
        public List<TraningProgramModel> GetTraningPrograms()
        {
            return _uow.TraningPlanRepository.GetAllTrainingProgarms().Select(x => new TraningProgramModel()
            {

                TrainingProgramId = x.Training_Program_Id,
                NameEnglish = x.Name_En

            }).ToList();
        }
    }
}
