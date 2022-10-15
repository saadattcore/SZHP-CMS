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
    public class FAQBH
    {
        private readonly IUnitOfWork _uow;

        public FAQBH(IUnitOfWork uow)
        {
            _uow = uow;
        }

        /// <summary>
        /// Get all FAQ categories.
        /// </summary>
        /// <returns></returns>
        public List<NewsCategoryModel> GetFAQCategories()
        {
            return _uow.FAQRepository.GetFAQCategories().Select(x => new NewsCategoryModel()
            {
                CategoryID = x.FAQ_Category_Id,
                NameEnglish = x.Name_En,
                NameArabic = x.Name_Ar

            }).ToList();
        }

        /// <summary>
        /// Get FAQ object by ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public FAQModel GetByID(long id)
        {
            var dbFAQ = _uow.FAQRepository.GetByID(id);
            var objFAQ = new FAQModel()
            {
                Id = dbFAQ.FAQ_Id,
                QuestionEnglish = dbFAQ.Question_En,
                QuestionArabic = dbFAQ.Question_Ar,
                CategoryID = dbFAQ.FAQ_Category != null ?  dbFAQ.FAQ_Category_Id : null,
                AnswerEnglish = dbFAQ.Answer_En,
                AnswerArabic = dbFAQ.Answer_Ar,
                CategoryName = dbFAQ.FAQ_Category != null ? dbFAQ.FAQ_Category.Name_En : string.Empty

            };

            return objFAQ;
        }

        /// <summary>
        /// Get all FAQ from db.
        /// </summary>
        /// <returns></returns>
        public List<FAQModel> GetAll()
        {
            var faqModelList = _uow.FAQRepository.GetAll().Select(x => new FAQModel()
            {

                Id = x.FAQ_Id,
                QuestionEnglish = x.Question_En,
                QuestionArabic = x.Question_Ar,
                CategoryID = x.FAQ_Category_Id,
                AnswerEnglish = x.Answer_En,
                AnswerArabic = x.Answer_Ar,
                CategoryName = x.FAQ_Category.Name_En,
                RowStatusID = x.Row_Status_Id


            }).ToList();


            foreach (var item in faqModelList)
            {
                item.RowStatus = Enum.GetName(typeof(RowStatus), item.RowStatusID);
            }

            return faqModelList;
        }

        /// <summary>
        /// Get active FAQ objects from db. Whose status are not delete.
        /// </summary>
        /// <returns></returns>
        public List<FAQModel> GetActiveFAQ(string dataFor = "")
        {
            var faqModelList = _uow.FAQRepository.GetActiveFAQ(dataFor).Select(x => new FAQModel()
            {
                Id = x.FAQ_Id,
                QuestionEnglish = x.Question_En,
                QuestionArabic = x.Question_Ar,
                CategoryID = x.FAQ_Category_Id,
                AnswerEnglish = x.Answer_En,
                AnswerArabic = x.Answer_Ar,
                CategoryName = x.FAQ_Category != null ? x.FAQ_Category.Name_En : "N/A",
                RowStatusID = x.Row_Status_Id,
                CreatedDate = x.Created_Date


            }).ToList();


            foreach (var item in faqModelList)
            {
                item.RowStatus = Enum.GetName(typeof(RowStatus), item.RowStatusID);
            }

            return faqModelList;
        }

        /// <summary>
        /// Add new FAQ obj into db.
        /// </summary>
        /// <param name="news">FAQ object to add</param>
        /// <returns></returns>
        public FAQModel Add(FAQModel faq)
        {
            try
            {
                FAQ objNews = new FAQ()
                {
                    Question_En = faq.QuestionEnglish,
                    Question_Ar = faq.QuestionArabic,
                    Answer_En = faq.AnswerEnglish,
                    Answer_Ar = faq.AnswerArabic,
                    FAQ_Category_Id = faq.CategoryID,
                    Created_By = faq.CreatedBy,
                    Row_Status_Id = (long?)RowStatus.Active,
                    Created_Date = DateTime.Now

                };

                _uow.FAQRepository.Add(objNews);
                _uow.Save();

                return faq;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Delete FAQ object from data base . It should be soft delete i.e just update row status to delete
        /// </summary>
        /// <param name="menu">FAQ object to delete</param>
        /// <returns></returns>
        public int Delete(int faqID)
        {
            FAQ dbFAQ = _uow.FAQRepository.GetByID(faqID);
            dbFAQ.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Delete;

            return _uow.Save();
        }

        /// <summary>
        /// Update FAQ's status .
        /// </summary>
        /// <param name="menus"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int UpdateRowStatus(IEnumerable<long> faqID, RowStatus status)
        {
            foreach (var id in faqID)
            {
                FAQ dbFAQ = _uow.FAQRepository.GetByID(id);
                dbFAQ.Row_Status_Id = (long?)status;
            }

            return _uow.Save();
        }

        /// <summary>
        /// Update FAQ in data base
        /// </summary>
        /// <param name="news">FAQ object to update</param>
        /// <returns>Number of rows effected</returns>
        public int Update(FAQModel faq)
        {
            FAQ dbFAQ = _uow.FAQRepository.GetByID(faq.Id);

            dbFAQ.Question_En = faq.QuestionEnglish;
            dbFAQ.Question_Ar = faq.QuestionArabic;
            dbFAQ.FAQ_Category_Id = faq.CategoryID;
            dbFAQ.Answer_En = faq.AnswerEnglish;
            dbFAQ.Answer_Ar = faq.AnswerArabic;
            dbFAQ.Updated_Date = System.DateTime.Now;

            return _uow.Save();
        }

    }
}
