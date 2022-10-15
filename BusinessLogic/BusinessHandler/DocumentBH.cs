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
    public class DocumentBH
    {
        private readonly IUnitOfWork _uow;
        public DocumentBH(UnitOfWork uow)
        {
            _uow = uow;
        }

        /// <summary>
        /// Get document by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DocumentModel GetByID(long id)
        {
            var dbDocument = _uow.DocumentRepsitory.GetByID(id);

            DocumentModel modelDoc = new DocumentModel()
            {

                DocumentId = dbDocument.Document_Id,
                FileName = dbDocument.File_Name,
                Extenstion = dbDocument.Extenstion
            };

            return modelDoc;
        }

        /// <summary>
        /// Delete document object from db . It would be a soft delete.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete(long id)
        {
            var dbDoc = _uow.DocumentRepsitory.GetByID(id);

            if (dbDoc == null)
                throw new Exception(SZHPCMS.Common.Constants.OBJECT_NULL_MESSAGE);

            dbDoc.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Delete;

            return _uow.Save();
        }


    }
}
