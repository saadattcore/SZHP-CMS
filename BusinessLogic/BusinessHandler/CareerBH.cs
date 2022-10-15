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
    public class CareerBH
    {
        private readonly IUnitOfWork _uow;
        public CareerBH(IUnitOfWork uow)
        {
            _uow = uow;
        }

        /// <summary>
        /// Get all active jobs from repository.
        /// </summary>
        /// <returns></returns>
        public List<CareerJobModel> GetActiveJobs()
        {
            List<Career_Job> dbJobs = _uow.CareerRepository.GetActiveJobs();

            List<CareerJobModel> modelJobs = new List<CareerJobModel>();

            foreach (var item in dbJobs)
            {
                CareerJobModel modelJob = new CareerJobModel();

                modelJob.CareerJobId = item.Career_Job_Id;
                modelJob.BenefitsAr = item.Benefits_Ar;
                modelJob.BenefitsEn = item.Benefits_En;
                modelJob.CareerIndustry = item.Career_Industry != null ? item.Career_Industry.Name_En : string.Empty;
                modelJob.CareerType = item.Career_Type != null ? item.Career_Type.Name_En : string.Empty;
                modelJob.CreatedDate = item.Created_Date;
                modelJob.DescriptionAr = item.Description_Ar;
                modelJob.DescriptionEn = item.Description_En;
                modelJob.DesiredSkillsAr = item.Desired_Skills_Ar;
                modelJob.DesiredSkillsEn = item.Desired_Skills_En;
                modelJob.LocationAr = item.Location_Ar;
                modelJob.LocationEn = item.Location_En;
                modelJob.MinumumEductionAr = item.Minumum_Eduction_Ar;
                modelJob.MinumumEductionEn = item.Minumum_Eduction_En;
                modelJob.MinumumExperienceAr = item.Minumum_Experience_Ar;
                modelJob.MinumumExperienceEn = item.Minumum_Experience_En;
                modelJob.ResponsibilitiesAr = item.Responsibilities_Ar;
                modelJob.ResponsibilitiesEn = item.Responsibilities_En;
                modelJob.SalaryAr = item.Salary_Ar;
                modelJob.SalaryEn = item.Salary_En;
                modelJob.SalaryCurrencyAr = item.Salary_Currency_Ar;
                modelJob.SalaryCurrencyEn = item.Salary_Currency_En;
                modelJob.ShiftAr = item.Shift_Ar;
                modelJob.ShiftEn = item.Shift_En;
                modelJob.TitleAr = item.Title_Ar;
                modelJob.TitleEn = item.Title_En;
                modelJob.RowStatus = Enum.GetName(typeof(RowStatus), item.Row_Status_Id);

                modelJobs.Add(modelJob);
            }

            return modelJobs;
        }

        /// <summary>
        /// Get job by id .
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CareerJobModel GetByID(long id)
        {
            Career_Job dbJob = _uow.CareerRepository.GetByID(id);

            if (dbJob == null)
                throw new Exception(Constants.OBJECT_NOT_FOUND_MESSAGE + id.ToString());

            CareerJobModel modelJob = new CareerJobModel();
            modelJob.CareerJobId = dbJob.Career_Job_Id;
            modelJob.BenefitsAr = dbJob.Benefits_Ar;
            modelJob.BenefitsEn = dbJob.Benefits_En;
            modelJob.CareerIndustry = dbJob.Career_Industry != null ? dbJob.Career_Industry.Description_En : string.Empty;
            modelJob.CareerType = dbJob.Career_Type != null ? dbJob.Career_Type.Name_En : string.Empty;
            modelJob.CreatedDate = dbJob.Created_Date;
            modelJob.DescriptionAr = dbJob.Description_Ar;
            modelJob.DescriptionEn = dbJob.Description_En;
            modelJob.DesiredSkillsAr = dbJob.Desired_Skills_Ar;
            modelJob.DesiredSkillsEn = dbJob.Desired_Skills_En;
            modelJob.LocationAr = dbJob.Location_Ar;
            modelJob.LocationEn = dbJob.Location_En;
            modelJob.MinumumEductionAr = dbJob.Minumum_Eduction_Ar;
            modelJob.MinumumEductionEn = dbJob.Minumum_Eduction_En;
            modelJob.MinumumExperienceAr = dbJob.Minumum_Experience_Ar;
            modelJob.MinumumExperienceEn = dbJob.Minumum_Experience_En;
            modelJob.ResponsibilitiesAr = dbJob.Responsibilities_Ar;
            modelJob.ResponsibilitiesEn = dbJob.Responsibilities_En;
            modelJob.SalaryAr = dbJob.Salary_Ar;
            modelJob.SalaryEn = dbJob.Salary_En;
            modelJob.SalaryCurrencyAr = dbJob.Salary_Currency_Ar;
            modelJob.SalaryCurrencyEn = dbJob.Salary_Currency_En;
            modelJob.ShiftAr = dbJob.Shift_Ar;
            modelJob.ShiftEn = dbJob.Shift_En;
            modelJob.TitleAr = dbJob.Title_Ar;
            modelJob.TitleEn = dbJob.Title_En;
            modelJob.CareerIndustryId = dbJob.Career_Industry_Id;
            modelJob.CareerTypeId = dbJob.Career_Type_Id;


            if (dbJob.Career_Job_Document.Count > 0)
            {
                modelJob.Documents = new List<DocumentModel>();

                foreach (var item in dbJob.Career_Job_Document)
                {
                    if (item.Document != null)
                    {
                        if (item.Document.Row_Status_Id != (long?)RowStatus.Delete)
                            modelJob.Documents.Add(new DocumentModel() { DocumentId = item.Document.Document_Id, FileName = item.Document.File_Name, Extenstion = item.Document.Extenstion });
                    }
                }
            }

            return modelJob;
        }

        /// <summary>
        /// Job Object to add to data base.
        /// </summary>
        /// <param name="modelJob"></param>
        /// <returns></returns>
        public CareerJobModel Add(CareerJobModel modelJob)
        {

            if (modelJob == null)
                throw new ArgumentNullException(Constants.OBJECT_NULL_MESSAGE);
            try
            {
                Career_Job dbJob = new Career_Job();

                dbJob.Benefits_Ar = modelJob.BenefitsAr;
                dbJob.Benefits_En = modelJob.BenefitsEn;
                dbJob.Created_Date = modelJob.CreatedDate;
                dbJob.Description_Ar = modelJob.DescriptionAr;
                dbJob.Description_En = modelJob.DescriptionEn;
                dbJob.Desired_Skills_Ar = modelJob.DesiredSkillsAr;
                dbJob.Desired_Skills_En = modelJob.DesiredSkillsEn;
                dbJob.Location_Ar = modelJob.LocationAr;
                dbJob.Location_En = modelJob.LocationEn;
                dbJob.Minumum_Eduction_Ar = modelJob.MinumumEductionAr;
                dbJob.Minumum_Eduction_En = modelJob.MinumumEductionEn;
                dbJob.Minumum_Experience_Ar = modelJob.MinumumExperienceAr;
                dbJob.Minumum_Experience_En = modelJob.MinumumExperienceEn;
                dbJob.Responsibilities_Ar = modelJob.ResponsibilitiesAr;
                dbJob.Responsibilities_En = modelJob.ResponsibilitiesEn;
                dbJob.Salary_Ar = modelJob.SalaryAr;
                dbJob.Salary_En = modelJob.SalaryEn;
                dbJob.Salary_Currency_Ar = modelJob.SalaryCurrencyAr;
                dbJob.Salary_Currency_En = modelJob.SalaryCurrencyEn;
                dbJob.Shift_Ar = modelJob.ShiftAr;
                dbJob.Shift_En = modelJob.ShiftEn;
                dbJob.Title_Ar = modelJob.TitleAr;
                dbJob.Title_En = modelJob.TitleEn;
                dbJob.Career_Type_Id = modelJob.CareerTypeId;
                dbJob.Career_Industry_Id = modelJob.CareerIndustryId;
                dbJob.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Active;
                dbJob.Created_By = modelJob.CreatedBy;
                dbJob.Created_Date = modelJob.CreatedDate;

                if (modelJob.Documents.Count > 0)
                {
                    dbJob.Career_Job_Document = new List<Career_Job_Document>();

                    foreach (var item in modelJob.Documents)
                    {
                        Career_Job_Document jobDoc = new Career_Job_Document();

                        jobDoc.Created_By = modelJob.CreatedBy;
                        jobDoc.Row_Status_Id = (long?)RowStatus.Active;

                        jobDoc.Document = new Document() { File_Name = item.FileName, Extenstion = item.Extenstion, Created_By = modelJob.CreatedBy, Row_Status_Id = (long?)RowStatus.Active, Created_Date = DateTime.Now };

                        dbJob.Career_Job_Document.Add(jobDoc);
                    }
                }

                _uow.CareerRepository.Add(dbJob);

                _uow.Save();

                modelJob.CareerJobId = dbJob.Career_Job_Id;

                return modelJob;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Update Career object in data base.
        /// </summary>
        /// <param name="modelJob"></param>
        /// <returns></returns>
        public int Update(CareerJobModel modelJob)
        {
            if (modelJob == null)
                throw new ArgumentNullException(Constants.OBJECT_NULL_MESSAGE);

            var dbJob = _uow.CareerRepository.GetByID(modelJob.CareerJobId);

            try
            {
                if (dbJob == null)
                    throw new Exception(Constants.OBJECT_NOT_FOUND_MESSAGE + modelJob.CareerJobId.ToString());

                dbJob.Benefits_Ar = modelJob.BenefitsAr;
                dbJob.Benefits_En = modelJob.BenefitsEn;
                dbJob.Created_Date = modelJob.CreatedDate;
                dbJob.Description_Ar = modelJob.DescriptionAr;
                dbJob.Description_En = modelJob.DescriptionEn;
                dbJob.Desired_Skills_Ar = modelJob.DesiredSkillsAr;
                dbJob.Desired_Skills_En = modelJob.DesiredSkillsEn;
                dbJob.Location_Ar = modelJob.LocationAr;
                dbJob.Location_En = modelJob.LocationEn;
                dbJob.Minumum_Eduction_Ar = modelJob.MinumumEductionAr;
                dbJob.Minumum_Eduction_En = modelJob.MinumumEductionEn;
                dbJob.Minumum_Experience_Ar = modelJob.MinumumExperienceAr;
                dbJob.Minumum_Experience_En = modelJob.MinumumExperienceEn;
                dbJob.Responsibilities_Ar = modelJob.ResponsibilitiesAr;
                dbJob.Responsibilities_En = modelJob.ResponsibilitiesEn;
                dbJob.Salary_Ar = modelJob.SalaryAr;
                dbJob.Salary_En = modelJob.SalaryEn;
                dbJob.Salary_Currency_Ar = modelJob.SalaryCurrencyAr;
                dbJob.Salary_Currency_En = modelJob.SalaryCurrencyEn;
                dbJob.Shift_Ar = modelJob.ShiftAr;
                dbJob.Shift_En = modelJob.ShiftEn;
                dbJob.Title_Ar = modelJob.TitleAr;
                dbJob.Title_En = modelJob.TitleEn;
                dbJob.Career_Type_Id = modelJob.CareerTypeId;
                dbJob.Career_Industry_Id = modelJob.CareerIndustryId;
                dbJob.Updated_By = modelJob.UpdatedBy;
                dbJob.Updated_Date = DateTime.Now;

                if (modelJob.Documents.Count > 0)
                {
                    dbJob.Career_Job_Document = new List<Career_Job_Document>();

                    foreach (var item in modelJob.Documents)
                    {
                        Career_Job_Document jobDoc = new Career_Job_Document();

                        jobDoc.Created_By = modelJob.CreatedBy;
                        jobDoc.Row_Status_Id = (long?)RowStatus.Active;

                        jobDoc.Document = new Document() { File_Name = item.FileName, Extenstion = item.Extenstion, Created_By = modelJob.CreatedBy, Row_Status_Id = (long?)RowStatus.Active };

                        dbJob.Career_Job_Document.Add(jobDoc);
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
        /// Delete career object from data base . It should be soft delete i.e just update row status to delete
        /// </summary>
        /// <param name="menu">Job object to delete</param>
        /// <returns></returns>
        public int Delete(long id)
        {
            try
            {
                Career_Job dbJob = _uow.CareerRepository.GetByID(id);

                if (dbJob == null)
                    throw new Exception(Constants.OBJECT_NOT_FOUND_MESSAGE + id as string);

                dbJob.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Delete;

                foreach (var item in dbJob.Career_Job_Document)
                {
                    if (item.Document != null)
                    {
                        item.Document.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Delete;
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
        /// Update job's status .
        /// </summary>
        /// <param name="menus"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int UpdateRowStatus(IEnumerable<long> idList, RowStatus status)
        {
            try
            {
                bool anyObjectFound = false;

                foreach (var id in idList)
                {
                    Career_Job dbJob = _uow.CareerRepository.GetByID(id);

                    if (dbJob != null)
                    {
                        anyObjectFound = true;

                        dbJob.Row_Status_Id = (long?)status;

                        if (status == RowStatus.Delete)
                        {
                            foreach (var item in dbJob.Career_Job_Document)
                            {
                                if (item.Document != null)
                                {
                                    item.Document.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Delete;
                                }
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

        /// <summary>
        /// Get all career industries.
        /// </summary>
        /// <returns></returns>
        public List<CareerIndustryModel> GetCareerIndustries()
        {
            return _uow.CareerRepository.GetCareerIndustries().Select(x => new CareerIndustryModel() { CareerIndustryId = x.Career_Industry_Id, NameEn = x.Name_En  , NameAr = x.Name_Ar , RowStatus = Enum.GetName(typeof(SZHPCMS.Common.RowStatus),x.Row_Status_Id)}).ToList();
        }

        /// <summary>
        /// Add industry
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public CareerIndustryModel AddIndutry(CareerIndustryModel model)
        {
            if (model == null)
                throw new Exception(Constants.OBJECT_NULL_MESSAGE);

            Career_Industry dbIndustry = new Career_Industry();

            dbIndustry.Name_En = model.NameEn;
            dbIndustry.Name_Ar = model.NameAr;

            dbIndustry.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Active;
            dbIndustry.Created_By = model.CreatedBy;
            dbIndustry.Created_Date = DateTime.Now;

            _uow.CareerRepository.AddIndustry(dbIndustry);

            _uow.Save();

            model.CareerIndustryId = dbIndustry.Career_Industry_Id;

            return model;

        }

        /// <summary>
        /// Add industry
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateIndustry(CareerIndustryModel model)
        {
            if (model == null)
                throw new ArgumentNullException(Constants.OBJECT_NULL_MESSAGE);

            var dbIndustry = _uow.CareerRepository.GetIndustryByID(model.CareerIndustryId);

            if (dbIndustry == null)
                throw new Exception(Constants.OBJECT_NOT_FOUND_MESSAGE);


            dbIndustry.Name_En = model.NameEn;
            dbIndustry.Name_Ar = model.NameAr;

            dbIndustry.Updated_By = model.UpdatedBy;
            dbIndustry.Updated_Date = DateTime.Now;



            return _uow.Save();

        }

        /// <summary>
        /// Get industry by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CareerIndustryModel GetIndustryByID(long id)
        {
            var dbIndustry = _uow.CareerRepository.GetIndustryByID(id);

            if (dbIndustry == null)
            {
                throw new Exception(Constants.OBJECT_NOT_FOUND_MESSAGE);
            }

            CareerIndustryModel model = new CareerIndustryModel();

            model.NameAr = dbIndustry.Name_Ar;
            model.NameEn = dbIndustry.Name_En;

            return model;

        }


        /// <summary>
        /// Delete Career industry object from data base . It should be soft delete i.e just update row status to delete
        /// </summary>
        /// <param name="menu">Career Industry object to delete</param>
        /// <returns></returns>
        public int DeleteCareerIndustry(int id)
        {
            var dbIndustry = _uow.CareerRepository.GetIndustryByID(id);
            dbIndustry.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Delete;

            return _uow.Save();
        }

        /// <summary>
        /// Update industry row status status .
        /// </summary>
        /// <param name="menus"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int UpdateRowStatusIndustry(IEnumerable<long> idList, RowStatus status)
        {
            foreach (var id in idList)
            {
                var dbIndustry = _uow.CareerRepository.GetIndustryByID(id);
                dbIndustry.Row_Status_Id = (long?)status;
            }

            return _uow.Save();
        }


        /// <summary>
        /// Get all career types.
        /// </summary>
        /// <returns></returns>
        public IList<CareerTypeModel> GetCareerTypes()
        {
            return _uow.CareerRepository.GetCareerTypes().Select(x => new CareerTypeModel() { CareerTypeId = x.Career_Type_Id, NamEn = x.Name_En }).ToList();
        }

        /// <summary>
        /// Get all job applicants.
        /// </summary>
        /// <returns></returns>
        public List<JobApplicantModel> GetJobApplicants()
        {
            var modelApplicants = _uow.CareerRepository.GetJobApplicants().Select(x => new JobApplicantModel()
             {
                 ApplicantID = x.Career_Job_Applied_Id,
                 FirstName = x.First_Name,
                 LastName = x.Last_Name,
                 Email = x.Email,
                 Experience = x.Experience,
                 Eduction = x.Eduction,
                 HomeAddress = x.Home_Address,
                 Mobile = x.Mobile,
                 Phone = x.Phone,
                 Skills = x.Skills,
                 DOB = x.DOB,
                 Resume = x.Document != null ? (new DocumentModel() { DocumentId = x.Document.Document_Id, FileName = x.Document.File_Name, Extenstion = x.Document.Extenstion }) : null,
                 JobApplied = x.Career_Job != null ? x.Career_Job.Title_En : string.Empty,
                 RowStatus = Enum.GetName(typeof(RowStatus), x.Row_Status_Id)

             }).ToList();

            return modelApplicants;

        }

        public JobApplicantModel GetApplicantByID(long id)
        {
            Career_Job_Applied dbApplicant = _uow.CareerRepository.GetApplicantByID(id);

            if (dbApplicant == null)
                throw new Exception(Constants.OBJECT_NOT_FOUND_MESSAGE);

            return new JobApplicantModel()
              {

                  ApplicantID = dbApplicant.Career_Job_Applied_Id,
                  FirstName = dbApplicant.First_Name,
                  LastName = dbApplicant.Last_Name,
                  Email = dbApplicant.Email,
                  Experience = dbApplicant.Experience,
                  Eduction = dbApplicant.Eduction,
                  HomeAddress = dbApplicant.Home_Address,
                  Mobile = dbApplicant.Mobile,
                  Phone = dbApplicant.Phone,
                  Skills = dbApplicant.Skills,
                  DOB = dbApplicant.DOB,
                  MaritalStatus = dbApplicant.Marital_Status,
                  Resume = dbApplicant.Document != null ? (new DocumentModel() { DocumentId = dbApplicant.Document.Document_Id, FileName = dbApplicant.Document.File_Name, Extenstion = dbApplicant.Document.Extenstion }) : null,
                  JobApplied = dbApplicant.Career_Job != null ? dbApplicant.Career_Job.Title_En : string.Empty

              };


        }

        /// <summary>
        /// Delete applicant by ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public int DeleteApplicant(long id)
        {
            try
            {
                Career_Job_Applied dbApplicant = _uow.CareerRepository.GetApplicantByID(id);

                if (dbApplicant == null)
                    throw new Exception(Constants.OBJECT_NOT_FOUND_MESSAGE + id as string);

                dbApplicant.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Delete;


                if (dbApplicant.Document != null)
                {
                    dbApplicant.Document.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Delete;
                }


                return _uow.Save();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
