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
    public class ProjectBH
    {
        private readonly IUnitOfWork _uow;
        public ProjectBH(IUnitOfWork uow)
        {
            _uow = uow;
        }

        #region Project Region
        /// <summary>
        /// Get active projects i.e projects which are not deleted.
        /// </summary>
        /// <returns></returns>
        public List<ProjectModel> GetActiveProjects(string dataFor = "")
        {
            var modelProjects = _uow.ProjectRepository.GetActiveProjects(dataFor).Select(x => new ProjectModel()
            {

                ProjectId = x.Project_Id,
                TitleAr = x.Title_Ar,
                TitleEn = x.Title_En,
                RowStatus = Enum.GetName(typeof(RowStatus), x.Row_Status_Id),
                CreatedDate = x.Created_Date,
                DescriptionEn = x.Description_En,
                DescriptionAr = x.Description_Ar,
                Document = x.Document != null ? new DocumentModel() { FileName = x.Document.File_Name, DocumentId = x.Document.Document_Id } : null

            }).ToList();

            return modelProjects;

        }


        /// <summary>
        /// Get project by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ProjectModel GetByID(long id)
        {
            var dbProject = _uow.ProjectRepository.GetByID(id);

            if (dbProject == null)
                throw new Exception(Constants.OBJECT_NOT_FOUND_MESSAGE + id.ToString());

            ProjectModel modelProject = new ProjectModel();

            modelProject.ProjectId = dbProject.Project_Id;
            modelProject.TitleAr = dbProject.Title_Ar;
            modelProject.TitleEn = dbProject.Title_En;
            modelProject.DescriptionAr = dbProject.Description_Ar;
            modelProject.DescriptionEn = dbProject.Description_En;

            if (dbProject.Document != null)
            {
                modelProject.Document = new DocumentModel();

                modelProject.Document.FileName = dbProject.Document.File_Name;
                modelProject.Document.Extenstion = dbProject.Document.Extenstion;
                modelProject.Document.DocumentId = dbProject.Document.Document_Id;
            }

            return modelProject;
        }


        /// <summary>
        /// Add new project obj into db.
        /// </summary>
        /// <param name="news">Gallery object to add</param>
        /// <returns></returns>
        public ProjectModel Add(ProjectModel model)
        {
            if (model == null)
                throw new ArgumentNullException(Constants.OBJECT_NULL_MESSAGE);

            try
            {
                Project dbProject = new Project();

                dbProject.Title_En = model.TitleEn;
                dbProject.Title_Ar = model.TitleAr;
                dbProject.Description_En = model.DescriptionEn;
                dbProject.Description_Ar = model.DescriptionAr;
                dbProject.Created_By = model.CreatedBy;
                dbProject.Row_Status_Id = (long?)RowStatus.Active;
                dbProject.Created_Date = DateTime.Now;

                if (model.Document != null)
                {
                    dbProject.Document = new Document();

                    dbProject.Document.File_Name = model.Document.FileName;
                    dbProject.Document.Extenstion = model.Document.Extenstion;
                    dbProject.Created_By = model.CreatedBy;
                    dbProject.Document.File_Type_Id = (long?)FileTypes.Picture;
                    dbProject.Created_Date = DateTime.Now;
                }

                _uow.ProjectRepository.Add(dbProject);
                _uow.Save();

                model.ProjectId = dbProject.Project_Id;

                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Update project in data base
        /// </summary>
        /// <param name="news">pronject object to update</param>
        /// <returns>Number of rows effected</returns>
        public int Update(ProjectModel model)
        {
            if (model == null)
                throw new ArgumentNullException(Constants.OBJECT_NULL_MESSAGE);

            try
            {
                Project dbProject = _uow.ProjectRepository.GetByID(model.ProjectId);

                if (dbProject == null)
                    throw new Exception(Constants.OBJECT_NOT_FOUND_MESSAGE + model.DocumentId.ToString());


                dbProject.Title_En = model.TitleEn;
                dbProject.Title_Ar = model.TitleAr;
                dbProject.Description_Ar = model.DescriptionAr;
                dbProject.Description_En = model.DescriptionEn;
                dbProject.Updated_By = model.UpdatedBy;
                dbProject.Updated_Date = DateTime.Now;

                if (model.Document != null)
                {
                    if (dbProject.Document == null)
                    {
                        dbProject.Document = new Document();
                        dbProject.Document.Created_By = model.CreatedBy;
                        dbProject.Document.Created_Date = DateTime.Now;
                        dbProject.Document.File_Type_Id = (long?)FileTypes.Picture;
                    }
                    else
                    {
                        dbProject.Document.Updated_By = model.UpdatedBy;
                        dbProject.Document.Updated_Date = DateTime.Now;
                    }


                    dbProject.Document.File_Name = model.Document.FileName;
                    dbProject.Document.Extenstion = model.Document.Extenstion;
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
        /// Delete project object from data base . It should be soft delete i.e just update row status to delete
        /// </summary>
        /// <param name="menu">project object to delete</param>
        /// <returns></returns>
        public int Delete(int id)
        {
            try
            {
                Project dbProject = _uow.ProjectRepository.GetByID(id);

                if (dbProject == null)
                    throw new Exception(Constants.OBJECT_NOT_FOUND_MESSAGE + id as string);

                dbProject.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Delete;

                if (dbProject.Document != null)
                    dbProject.Document.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Delete;

                return _uow.Save();

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        /// <summary>
        /// Update projects status .
        /// </summary>
        /// <param name="menus"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int UpdateRowStatus(IEnumerable<long> idList, RowStatus status)
        {
            try
            {
                foreach (var id in idList)
                {
                    Project dbProject = _uow.ProjectRepository.GetByID(id);
                    dbProject.Row_Status_Id = (long?)status;
                }

                return _uow.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion

        #region Open Data Region

        /// <summary>
        /// Get active open data  i.e projects which are not deleted.
        /// </summary>
        /// <returns></returns>
        public List<OpenDataModel> GetActiveOpenData(string dataFor = "")
        {
            var modelOpenData = _uow.ProjectRepository.GetActiveOpenData(dataFor).Select(x => new OpenDataModel()
            {

                OpenDataId = x.Open_Data_Id,
                TitleAr = x.Title_Ar,
                TitleEn = x.Title_En,
                RowStatus = Enum.GetName(typeof(RowStatus), x.Row_Status_Id),
                CreatedDate = x.Created_Date,
                PublicationDate = x.Publication_Date,
                ExcelDoc = x.Document != null ? new DocumentModel() { FileName = x.Document.File_Name, DocumentId = x.Document.Document_Id } : null,
                PDFDoc = x.Document1 != null ? new DocumentModel() { FileName = x.Document1.File_Name, DocumentId = x.Document1.Document_Id } : null


            }).ToList();

            return modelOpenData;

        }


        /// <summary>
        /// Get open data  by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public OpenDataModel GetOpenDataById(long id)
        {
            var dbOpenData = _uow.ProjectRepository.GetOpenDataById(id);

            if (dbOpenData == null)
                throw new Exception(Constants.OBJECT_NOT_FOUND_MESSAGE + id.ToString());

            OpenDataModel modelProject = new OpenDataModel();

            modelProject.OpenDataId = dbOpenData.Open_Data_Id;
            modelProject.TitleAr = dbOpenData.Title_Ar;
            modelProject.TitleEn = dbOpenData.Title_En;
            modelProject.PublicationDate = dbOpenData.Publication_Date;

            if (dbOpenData.Document != null)
            {
                modelProject.ExcelDoc = new DocumentModel();

                modelProject.ExcelDoc.FileName = dbOpenData.Document.File_Name;
                modelProject.ExcelDoc.Extenstion = dbOpenData.Document.Extenstion;
                modelProject.ExcelDoc.DocumentId = dbOpenData.Document.Document_Id;
            }

            if (dbOpenData.Document1 != null)
            {
                modelProject.PDFDoc = new DocumentModel();

                modelProject.PDFDoc.FileName = dbOpenData.Document1.File_Name;
                modelProject.PDFDoc.Extenstion = dbOpenData.Document1.Extenstion;
                modelProject.PDFDoc.DocumentId = dbOpenData.Document1.Document_Id;
            }

            return modelProject;
        }


        /// <summary>
        /// Add new open data obj into db.
        /// </summary>
        /// <param name="news">Gallery object to add</param>
        /// <returns></returns>
        public OpenDataModel AddOpenData(OpenDataModel model)
        {
            if (model == null)
                throw new ArgumentNullException(Constants.OBJECT_NULL_MESSAGE);

            try
            {
                Open_Data dbOpenData = new Open_Data();

                dbOpenData.Title_En = model.TitleEn;
                dbOpenData.Title_Ar = model.TitleAr;
                dbOpenData.Publication_Date = model.PublicationDate;
                dbOpenData.Created_By = model.CreatedBy;
                dbOpenData.Row_Status_Id = (long?)RowStatus.Active;
                dbOpenData.Created_Date = DateTime.Now;

                if (model.ExcelDoc != null)
                {
                    dbOpenData.Document = new Document();

                    dbOpenData.Document.File_Name = model.ExcelDoc.FileName;
                    dbOpenData.Document.Extenstion = model.ExcelDoc.Extenstion;
                    dbOpenData.Created_By = model.CreatedBy;
                    dbOpenData.Document.File_Type_Id = (long?)FileTypes.Excel;
                    dbOpenData.Created_Date = DateTime.Now;
                }

                if (model.PDFDoc != null)
                {
                    dbOpenData.Document1 = new Document();

                    dbOpenData.Document1.File_Name = model.PDFDoc.FileName;
                    dbOpenData.Document1.Extenstion = model.PDFDoc.Extenstion;
                    dbOpenData.Created_By = model.CreatedBy;
                    dbOpenData.Document1.File_Type_Id = (long?)FileTypes.Pdf;
                    dbOpenData.Created_Date = DateTime.Now;
                }

                _uow.ProjectRepository.AddOpenData(dbOpenData);
                _uow.Save();

                model.OpenDataId = dbOpenData.Open_Data_Id;

                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Update open data in data base
        /// </summary>
        /// <param name="news">open data object to update</param>
        /// <returns>Number of rows effected</returns>
        public int UpdateOpenData(OpenDataModel model)
        {
            if (model == null)
                throw new ArgumentNullException(Constants.OBJECT_NULL_MESSAGE);

            try
            {
                Open_Data dbOpenData = _uow.ProjectRepository.GetOpenDataById(model.OpenDataId);

                if (dbOpenData == null)
                    throw new Exception(Constants.OBJECT_NOT_FOUND_MESSAGE + model.OpenDataId.ToString());


                dbOpenData.Title_En = model.TitleEn;
                dbOpenData.Title_Ar = model.TitleAr;
                dbOpenData.Publication_Date = model.PublicationDate;
                dbOpenData.Updated_By = model.UpdatedBy;
                dbOpenData.Updated_Date = DateTime.Now;

                if (model.ExcelDoc != null)
                {
                    if (dbOpenData.Document == null)
                    {
                        dbOpenData.Document = new Document();

                        dbOpenData.Document.Created_By = model.CreatedBy;
                        dbOpenData.Document.Created_Date = DateTime.Now;
                        dbOpenData.Document.File_Type_Id = (long?)FileTypes.Excel;
                    }
                    else
                    {
                        dbOpenData.Document.Updated_By = model.UpdatedBy;
                        dbOpenData.Document.Updated_Date = DateTime.Now;
                    }


                    dbOpenData.Document.File_Name = model.ExcelDoc.FileName;
                    dbOpenData.Document.Extenstion = model.ExcelDoc.Extenstion;
                }


                if (model.PDFDoc != null)
                {
                    if (dbOpenData.Document1 == null)
                    {
                        dbOpenData.Document1 = new Document();

                        dbOpenData.Document1.Created_By = model.CreatedBy;
                        dbOpenData.Document1.Created_Date = DateTime.Now;
                        dbOpenData.Document1.File_Type_Id = (long?)FileTypes.Pdf;
                    }
                    else
                    {
                        dbOpenData.Document1.Updated_By = model.UpdatedBy;
                        dbOpenData.Document1.Updated_Date = DateTime.Now;
                    }


                    dbOpenData.Document1.File_Name = model.PDFDoc.FileName;
                    dbOpenData.Document1.Extenstion = model.PDFDoc.Extenstion;
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
        /// Delete open data object from data base . It should be soft delete i.e just update row status to delete
        /// </summary>
        /// <param name="menu">project object to delete</param>
        /// <returns></returns>
        public int DeleteOpenData(int id)
        {
            try
            {
                Open_Data dbOpenData = _uow.ProjectRepository.GetOpenDataById(id);

                if (dbOpenData == null)
                    throw new Exception(Constants.OBJECT_NOT_FOUND_MESSAGE + id as string);

                dbOpenData.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Delete;

                if (dbOpenData.Document != null)
                    dbOpenData.Document.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Delete;

                if (dbOpenData.Document1 != null)
                    dbOpenData.Document1.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Delete;

                return _uow.Save();

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        /// <summary>
        /// Update open data status .
        /// </summary>
        /// <param name="menus"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int UpdateRowStatusOpenData(IEnumerable<long> idList, RowStatus status)
        {
            try
            {
                foreach (var id in idList)
                {
                    Open_Data dbOpenData = _uow.ProjectRepository.GetOpenDataById(id);
                    dbOpenData.Row_Status_Id = (long?)status;
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
