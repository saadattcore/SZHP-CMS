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
    public class CannedEmailBH
    {
        private readonly IUnitOfWork _uow;
        public CannedEmailBH(IUnitOfWork uow)
        {
            _uow = uow;
        }

        /// <summary>
        /// Get all active links from repository.
        /// </summary>
        /// <returns></returns>
        public List<CannedEmailModel> GetActiveTemplates()
        {
            List<Canned_Email> dbCannedEmail = _uow.CannedEmailRepository.GetActiveEmailTemplates();

            List<CannedEmailModel> modelCannedEmails = new List<CannedEmailModel>();

            foreach (var item in dbCannedEmail)
            {
                CannedEmailModel modelCannedEmail = new CannedEmailModel();

                modelCannedEmail.CannedEmailId = item.Canned_Email_Id;
                modelCannedEmail.NameEn = item.Name_En;
                modelCannedEmail.NameAr = item.Name_Ar;
                modelCannedEmail.SubjectAr = item.Subject_Ar;
                modelCannedEmail.SubjectEn = item.Subject_En;
                modelCannedEmail.EmailFrom = item.Email_From;
                modelCannedEmail.CreatedDate = item.Created_Date;

                modelCannedEmail.RowStatus = Enum.GetName(typeof(RowStatus), item.Row_Status_Id);

                modelCannedEmails.Add(modelCannedEmail);
            }

            return modelCannedEmails;
        }

        /// <summary>
        /// Get email template by id .
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CannedEmailModel GetByID(long id)
        {
            Canned_Email dbEmail = _uow.CannedEmailRepository.GetByID(id);

            if (dbEmail == null)
                throw new Exception(Constants.OBJECT_NOT_FOUND_MESSAGE + id.ToString());

            CannedEmailModel modelEmail = new CannedEmailModel()
            {
                CannedEmailId = dbEmail.Canned_Email_Id,
                NameEn = dbEmail.Name_En,
                NameAr = dbEmail.Name_Ar,
                SubjectAr = dbEmail.Subject_Ar,
                SubjectEn = dbEmail.Subject_En,
                EmailFrom = dbEmail.Email_From,
                EmailTemplateEn = dbEmail.Email_Template_En,
                EmailTemplateAr = dbEmail.Email_Template_Ar
            };

            if (dbEmail.Canned_Email_Documents.Count > 0)
            {
                modelEmail.Documents = new List<DocumentModel>();

                foreach (var item in dbEmail.Canned_Email_Documents)
                {
                    if (item.Document != null)
                    {
                        if (item.Document.Row_Status_Id != (long?)RowStatus.Delete)
                            modelEmail.Documents.Add(new DocumentModel() { DocumentId = item.Document.Document_Id, FileName = item.Document.File_Name, Extenstion = item.Document.Extenstion });
                    }
                }
            }

            return modelEmail;
        }

        /// <summary>
        /// Canned Email Object to add to data base.
        /// </summary>
        /// <param name="modelEmail"></param>
        /// <returns></returns>
        public CannedEmailModel Add(CannedEmailModel modelEmail)
        {

            if (modelEmail == null)
                throw new ArgumentNullException(Constants.OBJECT_NULL_MESSAGE);
            try
            {
                Canned_Email dbEmail = new Canned_Email();

                dbEmail.Name_En = modelEmail.NameEn;
                dbEmail.Name_Ar = modelEmail.NameAr;
                dbEmail.Subject_Ar = modelEmail.SubjectAr;
                dbEmail.Subject_En = modelEmail.SubjectEn;
                dbEmail.Email_From = modelEmail.EmailFrom;
                dbEmail.Email_Template_En = modelEmail.EmailTemplateEn;
                dbEmail.Email_Template_Ar = modelEmail.EmailTemplateAr;
                dbEmail.Row_Status_Id = (long?)RowStatus.Active;
                dbEmail.Created_Date = DateTime.Now;


                if (modelEmail.Documents.Count > 0)
                {
                    dbEmail.Canned_Email_Documents = new List<Canned_Email_Documents>();

                    foreach (var item in modelEmail.Documents)
                    {
                        Canned_Email_Documents emailDoc = new Canned_Email_Documents();

                        emailDoc.Created_By = modelEmail.CreatedBy;
                        emailDoc.Row_Status_Id = (long?)RowStatus.Active;

                        emailDoc.Document = new Document() { File_Name = item.FileName, Extenstion = item.Extenstion, Created_By = modelEmail.CreatedBy, Row_Status_Id = (long?)RowStatus.Active, Created_Date = DateTime.Now };

                        dbEmail.Canned_Email_Documents.Add(emailDoc);
                    }
                }

                _uow.CannedEmailRepository.Add(dbEmail);

                _uow.Save();

                modelEmail.CannedEmailId = dbEmail.Canned_Email_Id;

                return modelEmail;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Update canned email object in data base.
        /// </summary>
        /// <param name="modelEmail"></param>
        /// <returns></returns>
        public int Update(CannedEmailModel modelEmail)
        {
            if (modelEmail == null)
                throw new ArgumentNullException(Constants.OBJECT_NULL_MESSAGE);

            var dbEmail = _uow.CannedEmailRepository.GetByID(modelEmail.CannedEmailId);

            try
            {
                if (dbEmail == null)
                    throw new Exception(Constants.OBJECT_NOT_FOUND_MESSAGE + modelEmail.CannedEmailId.ToString());


                dbEmail.Name_En = modelEmail.NameEn;
                dbEmail.Name_Ar = modelEmail.NameAr;
                dbEmail.Subject_Ar = modelEmail.SubjectAr;
                dbEmail.Subject_En = modelEmail.SubjectEn;
                dbEmail.Email_From = modelEmail.EmailFrom;
                dbEmail.Email_Template_En = modelEmail.EmailTemplateEn;
                dbEmail.Email_Template_Ar = modelEmail.EmailTemplateAr;
                dbEmail.Updated_By = modelEmail.UpdatedBy;
                dbEmail.Updated_Date = DateTime.Now;

                if (modelEmail.Documents.Count > 0)
                {
                    dbEmail.Canned_Email_Documents = new List<Canned_Email_Documents>();

                    foreach (var item in modelEmail.Documents)
                    {
                        Canned_Email_Documents emailDoc = new Canned_Email_Documents();

                        emailDoc.Created_By = modelEmail.CreatedBy;
                        emailDoc.Row_Status_Id = (long?)RowStatus.Active;

                        emailDoc.Document = new Document() { File_Name = item.FileName, Extenstion = item.Extenstion, Created_By = modelEmail.CreatedBy, Row_Status_Id = (long?)RowStatus.Active };

                        dbEmail.Canned_Email_Documents.Add(emailDoc);
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
        /// Delete canned email object from data base . It should be soft delete i.e just update row status to delete
        /// </summary>
        /// <param name="menu">Canned Email object to delete</param>
        /// <returns></returns>
        public int Delete(long id)
        {
            try
            {
                Canned_Email dbEmail = _uow.CannedEmailRepository.GetByID(id);

                if (dbEmail == null)
                    throw new Exception(Constants.OBJECT_NOT_FOUND_MESSAGE + id as string);

                dbEmail.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Delete;

                foreach (var item in dbEmail.Canned_Email_Documents)
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
        /// Update Links status .
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
                    Canned_Email dbEmail = _uow.CannedEmailRepository.GetByID(id);

                    if (dbEmail != null)
                    {
                        anyObjectFound = true;

                        dbEmail.Row_Status_Id = (long?)status;

                        if (status == RowStatus.Delete)
                        {
                            foreach (var item in dbEmail.Canned_Email_Documents)
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
    }
}
