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
    public class LinkBH
    {
        private readonly IUnitOfWork _uow;
        public LinkBH(IUnitOfWork uow)
        {
            _uow = uow;
        }

        /// <summary>
        /// Get all active links from repository.
        /// </summary>
        /// <returns></returns>
        public List<LinkModel> GetActiveLinks()
        {
            List<Link> dbLinks = _uow.LinkRepository.GetActiveLinks();

            List<LinkModel> modelLinks = new List<LinkModel>();

            foreach (var item in dbLinks)
            {
                LinkModel modelLink = new LinkModel();

                modelLink.LinkID = item.Link_Id;
                modelLink.TitleAr = item.Title_Ar;
                modelLink.TitleEn = item.Title_En;
                modelLink.LinkUrl = item.Link_Url;
                modelLink.CreatedDate = item.Created_Date;
                modelLink.RowStatus = Enum.GetName(typeof(RowStatus), item.Row_Status_Id);

                //modelLink.Documents = new List<DocumentModel>();

                //if (item.Link_Document.Count > 0)
                //{
                //    foreach (var linkDoc in item.Link_Document)
                //    {
                //        if (linkDoc.Document != null)
                //        {
                //            modelLink.Documents.Add(new DocumentModel() { DocumentId = linkDoc.Document.Document_Id, FileName = linkDoc.Document.File_Name, Extenstion = linkDoc.Document.Extenstion });
                //        }
                //    }
                //}

                modelLinks.Add(modelLink);
            }

            return modelLinks;
        }

        /// <summary>
        /// Get link by id .
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public LinkModel GetByID(long id)
        {
            Link dbLink = _uow.LinkRepository.GetByID(id);

            if (dbLink == null)
                throw new Exception(Constants.OBJECT_NOT_FOUND_MESSAGE + id.ToString());

            LinkModel modelLink = new LinkModel()
            {
                LinkID = dbLink.Link_Id,
                TitleEn = dbLink.Title_En,
                TitleAr = dbLink.Title_Ar,
                LinkUrl = dbLink.Link_Url
            };

            if (dbLink.Link_Document.Count > 0)
            {
                modelLink.Documents = new List<DocumentModel>();

                foreach (var item in dbLink.Link_Document)
                {
                    if (item.Document != null)
                    {
                        if (item.Document.Row_Status_Id != (long?)RowStatus.Delete)
                            modelLink.Documents.Add(new DocumentModel() { DocumentId = item.Document.Document_Id, FileName = item.Document.File_Name, Extenstion = item.Document.Extenstion });
                    }
                }
            }

            return modelLink;
        }

        /// <summary>
        /// Link Object to add to data base.
        /// </summary>
        /// <param name="modelLink"></param>
        /// <returns></returns>
        public LinkModel Add(LinkModel modelLink)
        {

            if (modelLink == null)
                throw new ArgumentNullException(Constants.OBJECT_NULL_MESSAGE);
            try
            {
                Link dbLink = new Link();
                dbLink.Title_Ar = modelLink.TitleAr;
                dbLink.Title_En = modelLink.TitleEn;
                dbLink.Link_Url = modelLink.LinkUrl;
                dbLink.Created_By = modelLink.CreatedBy;
                dbLink.Row_Status_Id = (long?)RowStatus.Active;
                dbLink.Created_Date = DateTime.Now;

                if (modelLink.Documents.Count > 0)
                {
                    dbLink.Link_Document = new List<Link_Document>();

                    foreach (var item in modelLink.Documents)
                    {
                        Link_Document linkDoc = new Link_Document();

                        linkDoc.Created_By = modelLink.CreatedBy;
                        linkDoc.Row_Status_Id = (long?)RowStatus.Active;

                        linkDoc.Document = new Document() { File_Name = item.FileName, Extenstion = item.Extenstion, Created_By = modelLink.CreatedBy, Row_Status_Id = (long?)RowStatus.Active };

                        dbLink.Link_Document.Add(linkDoc);
                    }
                }

                _uow.LinkRepository.Add(dbLink);

                _uow.Save();

                modelLink.LinkID = dbLink.Link_Id;

                return modelLink;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Update link object in data base.
        /// </summary>
        /// <param name="modelLink"></param>
        /// <returns></returns>
        public int Update(LinkModel modelLink)
        {
            if (modelLink == null)
                throw new ArgumentNullException(Constants.OBJECT_NULL_MESSAGE);

            var dbLink = _uow.LinkRepository.GetByID(modelLink.LinkID);

            try
            {
                if (dbLink == null)
                    throw new Exception(Constants.OBJECT_NOT_FOUND_MESSAGE + modelLink.LinkID.ToString());

                dbLink.Title_Ar = modelLink.TitleAr;
                dbLink.Title_En = modelLink.TitleEn;
                dbLink.Link_Url = modelLink.LinkUrl;
                dbLink.Updated_By = modelLink.UpdatedBy;
                dbLink.Updated_Date = DateTime.Now;

                if (modelLink.Documents.Count > 0)
                {
                    dbLink.Link_Document = new List<Link_Document>();

                    foreach (var item in modelLink.Documents)
                    {
                        Link_Document linkDoc = new Link_Document();

                        linkDoc.Created_By = modelLink.CreatedBy;
                        linkDoc.Row_Status_Id = (long?)RowStatus.Active;

                        linkDoc.Document = new Document() { File_Name = item.FileName, Extenstion = item.Extenstion, Created_By = modelLink.CreatedBy, Row_Status_Id = (long?)RowStatus.Active };

                        dbLink.Link_Document.Add(linkDoc);
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
        /// Delete link object from data base . It should be soft delete i.e just update row status to delete
        /// </summary>
        /// <param name="menu">Gallary object to delete</param>
        /// <returns></returns>
        public int Delete(long id)
        {
            try
            {
                Link dbLink = _uow.LinkRepository.GetByID(id);

                if (dbLink == null)
                    throw new Exception(Constants.OBJECT_NOT_FOUND_MESSAGE + id as string);

                dbLink.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Delete;

                foreach (var item in dbLink.Link_Document)
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
                    Link dbLink = _uow.LinkRepository.GetByID(id);

                    if (dbLink != null)
                    {
                        anyObjectFound = true;

                        dbLink.Row_Status_Id = (long?)status;

                        if (status == RowStatus.Delete)
                        {
                            foreach (var item in dbLink.Link_Document)
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
