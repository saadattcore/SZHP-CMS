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
    public class ArchiveBH
    {
        private readonly IUnitOfWork _uow;
        public ArchiveBH(UnitOfWork uow)
        {
            _uow = uow;
        }

        #region Form Region

        /// <summary>
        /// Get active forms
        /// </summary>
        /// <returns></returns>
        public List<FormModel> GetActiveForms(string dataFor = "")
        {
            //return _uow.ArchiveRepository.GetActiveForms().Select(x => new FormModel
            //{
            //    FormId = x.Form_Id,
            //    NameEn = x.Name_En,
            //    NameAr = x.Name_Ar,
            //    RowStatus = Enum.GetName(typeof(SZHPCMS.Common.RowStatus), x.Row_Status_Id),
            //    CreatedDate = x.Created_Date
                

            //}).ToList();


         List<Form> dbForms =    _uow.ArchiveRepository.GetActiveForms();

         List<FormModel> modelForms = new List<FormModel>();

         foreach (var item in dbForms)
         {
             FormModel formModel = new FormModel();
             formModel.FormId = item.Form_Id;
             formModel.NameEn = item.Name_En;
             formModel.NameAr = item.Name_Ar;
             formModel.RowStatus = Enum.GetName(typeof(SZHPCMS.Common.RowStatus), item.Row_Status_Id);
             formModel.CreatedDate = item.Created_Date;

             formModel.Documents = new List<FormDocumentModel>();
             
             foreach (var doc in item.Form_Documents)
             {                

                 FormDocumentModel docModel = new FormDocumentModel();
                 docModel.FormID = item.Form_Id;
                 docModel.NameEn = doc.Name_En;
                 docModel.NameAr = doc.Name_Ar;
                 docModel.Document = doc.Document != null ? new DocumentModel() {DocumentId = doc.Document.Document_Id , FileName = doc.Document.File_Name } : null;

                 formModel.Documents.Add(docModel);
             }

             modelForms.Add(formModel);
         }

         return modelForms;
        }

        /// <summary>
        /// Get form by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public FormModel GetFormByID(long id)
        {

            var dbForm = _uow.ArchiveRepository.GetByID(id);

            if (dbForm == null)
                throw new Exception("Banner not found.");

            FormModel modelForm = new FormModel();

            modelForm.FormId = dbForm.Form_Id;
            modelForm.NameEn = dbForm.Name_En;
            modelForm.NameAr = dbForm.Name_Ar;
            modelForm.RowStatus = Enum.GetName(typeof(SZHPCMS.Common.RowStatus), dbForm.Row_Status_Id);

            if (dbForm.Form_Documents.Count > 0)
            {
                modelForm.Documents = new List<FormDocumentModel>();

                foreach (var item in dbForm.Form_Documents)
                {
                    if (item.Document != null)
                    {
                        if (item.Document.Row_Status_Id != (long?)SZHPCMS.Common.RowStatus.Delete)
                        {
                            // modelForm.Documents.Add(new DocumentModel() { FileName = item.Document.File_Name, Extenstion = item.Document.Extenstion, DocumentId = item.Document.Document_Id });

                            FormDocumentModel modelFormModel = new FormDocumentModel();

                            modelFormModel.FormDocumentId = item.Form_Document_Id;
                            modelFormModel.NameEn = item.Name_En;
                            modelFormModel.NameAr = item.Name_Ar;
                            modelFormModel.DocumentName = item.Document != null ? item.Document.File_Name : string.Empty;

                            modelFormModel.Document = new DocumentModel();
                            modelFormModel.Document.FileName = item.Document.File_Name;
                            modelFormModel.Document.Extenstion = item.Document.Extenstion;

                            modelForm.Documents.Add(modelFormModel);

                        }
                    }
                }
            }

            return modelForm;

        }

        /// <summary>
        /// Add form in banner table.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public FormModel AddForm(FormModel model)
        {
            if (model == null)
                throw new ArgumentNullException("Banner model cannot be null .");

            Form dbForm = new Form();

            dbForm.Name_En = model.NameEn;
            dbForm.Name_Ar = model.NameAr;
            dbForm.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Active;
            dbForm.Created_By = model.CreatedBy;
            dbForm.Created_Date = DateTime.Now;

            if (model.Documents != null)
            {
                if (model.Documents.Count > 0)
                {
                    dbForm.Form_Documents = new List<Form_Documents>();

                    foreach (var item in model.Documents)
                    {
                        Form_Documents formDoc = new Form_Documents();

                        formDoc.Created_By = model.CreatedBy;
                        formDoc.Name_Ar = item.NameAr;
                        formDoc.Name_En = item.NameEn;
                        formDoc.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Active;
                        formDoc.Created_Date = DateTime.Now;

                        formDoc.Document = new Document();
                        formDoc.Document.File_Name = item.Document.FileName;
                        formDoc.Document.Extenstion = item.Document.Extenstion;
                        formDoc.Document.Created_By = model.CreatedBy;
                        formDoc.Document.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Active;
                        formDoc.Document.Created_Date = DateTime.Now;
                        formDoc.Document.File_Type_Id = UtilityBH.GetFileTypeID(item.Document.Extenstion);


                        dbForm.Form_Documents.Add(formDoc);
                    }
                }
            }

            _uow.ArchiveRepository.Add(dbForm);
            _uow.Save();

            model.FormId = dbForm.Form_Id;

            return model;
        }

        /// <summary>
        /// Update form model in db .
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateForm(FormModel model)
        {
            if (model == null)
                throw new ArgumentNullException("Banner model cannot be null .");

            Form dbForm = _uow.ArchiveRepository.GetByID(model.FormId);

            if (dbForm == null)
                throw new Exception("Banner with id = " + model.FormId + " was not found");

            int result = 0;

            dbForm.Name_Ar = model.NameAr;
            dbForm.Name_En = model.NameEn;
            dbForm.Updated_By = model.UpdatedBy;
            dbForm.Updated_Date = DateTime.Now;

            if (model.Documents != null)
            {
                if (model.Documents.Count > 0)
                {
                    dbForm.Form_Documents = new List<Form_Documents>();

                    foreach (var item in model.Documents)
                    {
                        Form_Documents formDoc = new Form_Documents();

                        formDoc.Created_By = model.CreatedBy;
                        formDoc.Name_Ar = item.NameAr;
                        formDoc.Name_En = item.NameEn;
                        formDoc.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Active;
                        formDoc.Created_Date = DateTime.Now;

                        formDoc.Document = new Document();
                        formDoc.Document.File_Name = item.Document.FileName;
                        formDoc.Document.Extenstion = item.Document.Extenstion;
                        formDoc.Document.Created_By = model.CreatedBy;
                        formDoc.Document.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Active;
                        formDoc.Document.Created_Date = DateTime.Now;
                        formDoc.Document.File_Type_Id = UtilityBH.GetFileTypeID(item.Document.Extenstion);
                        

                        dbForm.Form_Documents.Add(formDoc);
                    }
                }
            }

            result = _uow.Save();
            return result;
        }

        /// <summary>
        /// Delete form from db . It will be a soft delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteForm(int id)
        {
            Form dbForm = _uow.ArchiveRepository.GetByID(id);

            dbForm.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Delete;

            // Delete related images also .

            foreach (var item in dbForm.Form_Documents)
            {
                item.Document.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Delete;

                item.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Delete;
            }


            return _uow.Save();
        }

        /// <summary>
        /// Bulk update form row status
        /// </summary>
        /// <param name="idList"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int UpdateRowStatus(IEnumerable<long> idList, RowStatus status, Archive archive)
        {

            switch (archive)
            {
                case Archive.Form:

                    foreach (var id in idList)
                    {
                        Form dbForm = _uow.ArchiveRepository.GetByID(id);
                        dbForm.Row_Status_Id = (long?)status;
                    }

                    break;

                default:
                    break;
            }

            return _uow.Save();
        }

        /// <summary>
        /// Get document of form by formdoc id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public FormDocumentModel GetFormDoc(long id)
        {
            var dbFormDoc = _uow.ArchiveRepository.GetFormDoc(id);

            FormDocumentModel model = new FormDocumentModel();

            if (dbFormDoc != null)
            {
                model.FormDocumentId = dbFormDoc.Form_Document_Id;
                model.NameAr = dbFormDoc.Name_Ar;
                model.NameEn = dbFormDoc.Name_En;

                if (dbFormDoc.Document != null)
                {
                    model.DocumentName = dbFormDoc.Document.File_Name;
                }
            }

            return model;
        }


        /// <summary>
        /// Update form document.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateFormDoc(FormDocumentModel model, string op)
        {
            if (model == null)
                throw new ArgumentNullException(Constants.OBJECT_NULL_MESSAGE);

            Form_Documents dbFormDoc = null;

            if (op == "Update")
            {
                dbFormDoc = _uow.ArchiveRepository.GetFormDoc(model.FormDocumentId);

                if (dbFormDoc == null)
                    throw new Exception(Constants.OBJECT_NOT_FOUND_MESSAGE + model.FormDocumentId);

                dbFormDoc.Updated_Date = DateTime.Now;
                dbFormDoc.Updated_By = model.UpdatedBy;
            }
            else if (op == "Create")
            {
                dbFormDoc = new Form_Documents();
                dbFormDoc.Form_Id = model.FormID;
                dbFormDoc.Created_Date = DateTime.Now;
                dbFormDoc.Created_By = model.CreatedBy;
                dbFormDoc.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Active;
            }


            dbFormDoc.Name_Ar = model.NameAr;
            dbFormDoc.Name_En = model.NameEn;           
            

            if (model.Document != null)
            {
                if (dbFormDoc.Document == null)
                {
                    dbFormDoc.Document = new Document();

                    dbFormDoc.Document.Created_By = model.CreatedBy;
                    dbFormDoc.Document.Created_Date = DateTime.Now;
                    dbFormDoc.Document.File_Type_Id = UtilityBH.GetFileTypeID(model.Document.Extenstion);

                }
                else
                {
                    dbFormDoc.Document.Updated_By = model.UpdatedBy;
                    dbFormDoc.Document.Updated_Date = DateTime.Now;
                }

                dbFormDoc.Document.File_Name = model.Document.FileName;
                dbFormDoc.Document.Extenstion = model.Document.Extenstion.Contains("sheet") ? "xls" : model.Document.Extenstion;

            }

            if (op == "Create")
            {
                _uow.ArchiveRepository.AddFormDoc(dbFormDoc);
            }

            return _uow.Save();

        }


        #endregion

        #region Rules and Regulation Region
        /// <summary>
        /// Get active rules  i.e rules and regulation which are not deleted.
        /// </summary>
        /// <returns></returns>
        public List<RulesAndRegulationModel> GetActiveRules(bool isAdmin = true)
        {

            var modelGalleries = _uow.ArchiveRepository.GetActiveRulesAndRegulation(isAdmin).Select(x => new RulesAndRegulationModel()
            {

                RuleId = x.Rule_Id,
                TitleAr = x.Title_Ar,
                TitleEn = x.Title_En,
                Year = x.Year,
                CreatedDate = x.Created_Date,
                RowStatus = Enum.GetName(typeof(RowStatus), x.Row_Status_Id),
                DocumentName = x.Document != null ? x.Document.File_Name : ""

            }).ToList();

            return modelGalleries;

        }

        /// <summary>
        /// Get rule and regulation by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RulesAndRegulationModel GetRuleById(long id)
        {
            var dbRule = _uow.ArchiveRepository.GetRulesById(id);

            if (dbRule == null)
                throw new Exception(Constants.OBJECT_NOT_FOUND_MESSAGE + id.ToString());

            RulesAndRegulationModel modelRule = new RulesAndRegulationModel();

            modelRule.RuleId = dbRule.Rule_Id;
            modelRule.TitleAr = dbRule.Title_Ar;
            modelRule.TitleEn = dbRule.Title_En;
            modelRule.Year = dbRule.Year;

            if (dbRule.Document != null)
            {
                modelRule.Document = new DocumentModel();

                modelRule.Document.FileName = dbRule.Document.File_Name;
                modelRule.Document.Extenstion = dbRule.Document.Extenstion;
                modelRule.Document.DocumentId = dbRule.Document.Document_Id;
            }

            return modelRule;
        }


        /// <summary>
        /// Add new rule obj into db.
        /// </summary>
        /// <param name="news">Rule object to add</param>
        /// <returns></returns>
        public RulesAndRegulationModel AddRule(RulesAndRegulationModel model)
        {
            if (model == null)
                throw new ArgumentNullException(Constants.OBJECT_NULL_MESSAGE);

            try
            {
                Rules_And_Regulation dbGallery = new Rules_And_Regulation();

                dbGallery.Title_En = model.TitleEn;
                dbGallery.Title_Ar = model.TitleAr;
                dbGallery.Created_By = model.CreatedBy;
                dbGallery.Row_Status_Id = (long?)RowStatus.Active;
                dbGallery.Created_Date = DateTime.Now;
                dbGallery.Year = model.Year;

                if (model.Document != null)
                {
                    dbGallery.Document = new Document();

                    dbGallery.Document.File_Name = model.Document.FileName;
                    dbGallery.Document.Extenstion = model.Document.Extenstion;
                    dbGallery.Created_By = model.CreatedBy;
                }

                _uow.ArchiveRepository.AddRulesAndRegulation(dbGallery);
                _uow.Save();

                model.RuleId = dbGallery.Rule_Id;

                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Update rule in data base
        /// </summary>
        /// <param name="news">gallary object to update</param>
        /// <returns>Number of rows effected</returns>
        public int UpdateRule(RulesAndRegulationModel model)
        {
            if (model == null)
                throw new ArgumentNullException(Constants.OBJECT_NULL_MESSAGE);

            try
            {
                Rules_And_Regulation dbRule = _uow.ArchiveRepository.GetRulesById(model.RuleId);

                if (dbRule == null)
                    throw new Exception(Constants.OBJECT_NOT_FOUND_MESSAGE + model.RuleId.ToString());


                dbRule.Title_En = model.TitleEn;
                dbRule.Title_Ar = model.TitleAr;
                dbRule.Created_By = model.CreatedBy;
                dbRule.Row_Status_Id = (long?)RowStatus.Active;
                dbRule.Year = model.Year;

                if (model.Document != null)
                {
                    if (dbRule.Document == null)
                    {
                        dbRule.Document = new Document();
                        dbRule.Document.Created_By = model.CreatedBy;
                    }
                    else
                    {
                        dbRule.Document.Updated_By = model.UpdatedBy;
                        dbRule.Document.Updated_Date = DateTime.Now;
                    }


                    dbRule.Document.File_Name = model.Document.FileName;
                    dbRule.Document.Extenstion = model.Document.Extenstion;
                    dbRule.Created_By = model.CreatedBy;
                    dbRule.Created_Date = DateTime.Now;
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
        /// Delete rule object from data base . It should be soft delete i.e just update row status to delete
        /// </summary>
        /// <param name="menu">Gallary object to delete</param>
        /// <returns></returns>
        public int DeleteRule(int id)
        {
            try
            {
                Rules_And_Regulation dbRule = _uow.ArchiveRepository.GetRulesById(id);

                if (dbRule == null)
                    throw new Exception(Constants.OBJECT_NOT_FOUND_MESSAGE + id as string);

                dbRule.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Delete;

                if (dbRule.Document != null)
                    dbRule.Document.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Delete;

                return _uow.Save();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Update rules status .
        /// </summary>
        /// <param name="menus"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int UpdateRowStatusRule(IEnumerable<long> eventIDList, RowStatus status)
        {
            try
            {
                foreach (var id in eventIDList)
                {
                    Rules_And_Regulation dbRule = _uow.ArchiveRepository.GetRulesById(id);

                    dbRule.Row_Status_Id = (long?)status;
                }

                return _uow.Save();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region Magzine Region
        /// <summary>
        /// Get active magzines  i.e magzine which are not deleted.
        /// </summary>
        /// <returns></returns>
        public List<MagzineModel> GetActiveMagzine(bool isAdmin = true)
        {

            var modelMagzines = _uow.ArchiveRepository.GetActiveMagzines().Select(x => new MagzineModel()
            {

                MagzineId = x.Magzine_Id,
                TitleAr = x.Title_Ar,
                TitleEn = x.Title_En,
                Date = x.Date,
                FileDoc = x.Document1 != null ? new DocumentModel(){DocumentId = x.Document1.Document_Id , FileName = x.Document1.File_Name}  : null,
                CoverDoc = x.Document != null ? new DocumentModel() { DocumentId = x.Document.Document_Id, FileName = x.Document.File_Name } : null,
                RowStatus = Enum.GetName(typeof(RowStatus), x.Row_Status_Id)

            }).ToList();

            return modelMagzines;

        }

        /// <summary>
        /// Get magzine by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MagzineModel GetMagzineById(long id)
        {
            var dbMagzine = _uow.ArchiveRepository.GetMagzineById(id);

            if (dbMagzine == null)
                throw new Exception(Constants.OBJECT_NOT_FOUND_MESSAGE + id.ToString());

            MagzineModel modelMagzine = new MagzineModel();

            modelMagzine.MagzineId = dbMagzine.Magzine_Id;
            modelMagzine.TitleAr = dbMagzine.Title_Ar;
            modelMagzine.TitleEn = dbMagzine.Title_En;
            modelMagzine.Date = dbMagzine.Date;

            if (dbMagzine.Document != null)
            {
                modelMagzine.CoverDoc = new DocumentModel();

                modelMagzine.CoverDoc.FileName = dbMagzine.Document.File_Name;
                modelMagzine.CoverDoc.Extenstion = dbMagzine.Document.Extenstion;
                modelMagzine.CoverDoc.DocumentId = dbMagzine.Document.Document_Id;
            }

            if (dbMagzine.Document1 != null)
            {
                modelMagzine.FileDoc = new DocumentModel();

                modelMagzine.FileDoc.FileName = dbMagzine.Document1.File_Name;
                modelMagzine.FileDoc.Extenstion = dbMagzine.Document1.Extenstion;
                modelMagzine.FileDoc.DocumentId = dbMagzine.Document1.Document_Id;
            }

            return modelMagzine;
        }


        /// <summary>
        /// Add new magzine obj into db.
        /// </summary>
        /// <param name="news">Rule object to add</param>
        /// <returns></returns>
        public MagzineModel AddMagzine(MagzineModel model)
        {
            if (model == null)
                throw new ArgumentNullException(Constants.OBJECT_NULL_MESSAGE);

            try
            {
                Magzine dbMagzine = new Magzine();

                dbMagzine.Title_En = model.TitleEn;
                dbMagzine.Title_Ar = model.TitleAr;
                dbMagzine.Created_By = model.CreatedBy;
                dbMagzine.Row_Status_Id = (long?)RowStatus.Active;
                dbMagzine.Created_Date = DateTime.Now;
                dbMagzine.Date = model.Date;

                if (model.CoverDoc != null)
                {
                    dbMagzine.Document = new Document();

                    dbMagzine.Document.File_Name = model.CoverDoc.FileName;
                    dbMagzine.Document.Extenstion = model.CoverDoc.Extenstion;
                    dbMagzine.Document.Created_By = model.CreatedBy;
                    dbMagzine.Document.Created_Date = DateTime.Now;
                    dbMagzine.Document.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Active;

                }

                if (model.FileDoc != null)
                {
                    dbMagzine.Document1 = new Document();

                    dbMagzine.Document1.File_Name = model.FileDoc.FileName;
                    dbMagzine.Document1.Extenstion = model.FileDoc.Extenstion;
                    dbMagzine.Document1.Created_By = model.CreatedBy;
                    dbMagzine.Document1.Created_Date = DateTime.Now;
                    dbMagzine.Document1.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Active;
                }

                _uow.ArchiveRepository.AddMagzine(dbMagzine);
                _uow.Save();

                model.MagzineId = dbMagzine.Magzine_Id;

                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Update magzine in data base
        /// </summary>
        /// <param name="news">gallary object to update</param>
        /// <returns>Number of rows effected</returns>
        public int UpdateMagzine(MagzineModel model)
        {
            if (model == null)
                throw new ArgumentNullException(Constants.OBJECT_NULL_MESSAGE);

            try
            {
                Magzine dbMagzine = _uow.ArchiveRepository.GetMagzineById(model.MagzineId);

                if (dbMagzine == null)
                    throw new Exception(Constants.OBJECT_NOT_FOUND_MESSAGE + model.MagzineId.ToString());


                dbMagzine.Title_En = model.TitleEn;
                dbMagzine.Title_Ar = model.TitleAr;
                dbMagzine.Created_By = model.CreatedBy;
                dbMagzine.Row_Status_Id = (long?)RowStatus.Active;
                dbMagzine.Date = model.Date;

                if (model.CoverDoc != null)
                {
                    if (dbMagzine.Document == null)
                    {
                        dbMagzine.Document = new Document();

                        dbMagzine.Document.Created_By = model.CreatedBy;
                        dbMagzine.Document.Created_Date = DateTime.Now;
                    }
                    else
                    {
                        dbMagzine.Document.Updated_By = model.UpdatedBy;
                        dbMagzine.Document.Updated_Date = DateTime.Now;
                    }

                    dbMagzine.Document.File_Name = model.CoverDoc.FileName;
                    dbMagzine.Document.Extenstion = model.CoverDoc.Extenstion;
                    dbMagzine.Document.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Active;

                }

                if (model.FileDoc != null)
                {
                    if (dbMagzine.Document1 == null)
                    {
                        dbMagzine.Document1 = new Document();

                        dbMagzine.Document1.Created_By = model.CreatedBy;
                        dbMagzine.Document1.Created_Date = DateTime.Now;
                    }
                    else
                    {
                        dbMagzine.Document1.Updated_By = model.UpdatedBy;
                        dbMagzine.Document1.Updated_Date = DateTime.Now;
                    }

                    dbMagzine.Document1.File_Name = model.FileDoc.FileName;
                    dbMagzine.Document1.Extenstion = model.FileDoc.Extenstion;
                    dbMagzine.Document1.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Active;

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
        /// Delete magzine object from data base . It should be soft delete i.e just update row status to delete
        /// </summary>
        /// <param name="menu">Gallary object to delete</param>
        /// <returns></returns>
        public int DeleteMagzine(int id)
        {
            try
            {
                Magzine dbMagzine = _uow.ArchiveRepository.GetMagzineById(id);

                if (dbMagzine == null)
                    throw new Exception(Constants.OBJECT_NOT_FOUND_MESSAGE + id as string);

                dbMagzine.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Delete;

                if (dbMagzine.Document != null)
                    dbMagzine.Document.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Delete;

                return _uow.Save();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Update rules status .
        /// </summary>
        /// <param name="menus"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int UpdateRowStatusMagzine(IEnumerable<long> eventIDList, RowStatus status)
        {
            try
            {
                foreach (var id in eventIDList)
                {
                    Magzine dbMagzine = _uow.ArchiveRepository.GetMagzineById(id);

                    dbMagzine.Row_Status_Id = (long?)status;
                }

                return _uow.Save();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region Conditions and Requirements Region
        /// <summary>
        /// Get active conditions  i.e conditions and requirements which are not deleted.
        /// </summary>
        /// <returns></returns>
        public List<ConditionModel> GetConditions(bool isAdmin = true)
        {

            var mdConditions = _uow.ArchiveRepository.GetConditions(isAdmin).Select(x => new ConditionModel()
            {

                ConditionId = x.Condition_Id,
                TitleAr = x.Title_Ar,
                TitleEn = x.Title_En,
                DescriptionEn = x.Description_En,
                DescriptionAr = x.Description_Ar,
                CreatedDate = x.Created_Date,
                RowStatus = Enum.GetName(typeof(RowStatus), x.Row_Status_Id)

            }).ToList();

            return mdConditions;

        }

        /// <summary>
        /// Get conditions and requirements by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ConditionModel GetCondition(long id)
        {
            var dbCondition = _uow.ArchiveRepository.GetCondition(id);

            if (dbCondition == null)
                throw new Exception(Constants.OBJECT_NOT_FOUND_MESSAGE + id.ToString());

            ConditionModel mdCondition = new ConditionModel();

            mdCondition.ConditionId = dbCondition.Condition_Id;
            mdCondition.TitleAr = dbCondition.Title_Ar;
            mdCondition.TitleEn = dbCondition.Title_En;
            mdCondition.DescriptionEn = dbCondition.Description_En;
            mdCondition.DescriptionAr = dbCondition.Description_Ar;

            return mdCondition;
        }


        /// <summary>
        /// Add new condition obj into db.
        /// </summary>
        /// <param name="news">Condition object to add</param>
        /// <returns></returns>
        public ConditionModel AddCondition(ConditionModel model)
        {
            if (model == null)
                throw new ArgumentNullException(Constants.OBJECT_NULL_MESSAGE);

            try
            {
                Conditions_And_Requirements dbCondition = new Conditions_And_Requirements();

                dbCondition.Title_En = model.TitleEn;
                dbCondition.Title_Ar = model.TitleAr;
                dbCondition.Description_En = model.DescriptionEn;
                dbCondition.Description_Ar = model.DescriptionAr;

                dbCondition.Created_By = model.CreatedBy;
                dbCondition.Row_Status_Id = (long?)RowStatus.Active;
                dbCondition.Created_Date = DateTime.Now;

                _uow.ArchiveRepository.AddCondition(dbCondition);
                _uow.Save();

                model.ConditionId = dbCondition.Condition_Id;

                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Update condition in data base
        /// </summary>
        /// <param name="news">gallary object to update</param>
        /// <returns>Number of rows effected</returns>
        public int UpdateCondition(ConditionModel model)
        {
            if (model == null)
                throw new ArgumentNullException(Constants.OBJECT_NULL_MESSAGE);

            try
            {
                Conditions_And_Requirements dbCondition = _uow.ArchiveRepository.GetCondition(model.ConditionId);

                if (dbCondition == null)
                    throw new Exception(Constants.OBJECT_NOT_FOUND_MESSAGE + model.ConditionId.ToString());


                dbCondition.Title_En = model.TitleEn;
                dbCondition.Title_Ar = model.TitleAr;
                dbCondition.Description_En = model.DescriptionEn;
                dbCondition.Description_Ar = model.DescriptionAr;

                dbCondition.Updated_By = model.UpdatedBy;
                dbCondition.Updated_Date = DateTime.Now;
                dbCondition.Row_Status_Id = (long?)RowStatus.Active;

                int rowsEffected = _uow.Save();

                return rowsEffected;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Delete condition object from data base . It should be soft delete i.e just update row status to delete
        /// </summary>
        /// <param name="menu">Condition object to delete</param>
        /// <returns></returns>
        public int DeleteCondition(int id)
        {
            try
            {
                Conditions_And_Requirements dbCondition = _uow.ArchiveRepository.GetCondition(id);

                if (dbCondition == null)
                    throw new Exception(Constants.OBJECT_NOT_FOUND_MESSAGE + id as string);

                dbCondition.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Delete;

                return _uow.Save();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Update condition status .
        /// </summary>
        /// <param name="menus"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int UpdateRowStatusCondition(IEnumerable<long> idList, RowStatus status)
        {
            try
            {
                foreach (var id in idList)
                {
                    Conditions_And_Requirements dbCondition = _uow.ArchiveRepository.GetCondition(id);

                    dbCondition.Row_Status_Id = (long?)status;
                }

                return _uow.Save();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

    }
}
