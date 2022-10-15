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
    public class NewsBH
    {
        private readonly IUnitOfWork _uow;
        public NewsBH(IUnitOfWork uow)
        {
            _uow = uow;
        }

        /// <summary>
        /// Get all news categories.
        /// </summary>
        /// <returns></returns>
        public List<NewsCategoryModel> GetNewsCategories()
        {
            return _uow.NewsRepository.GetNewsCategories().Select(x => new NewsCategoryModel()
            {
                CategoryID = x.News_Category_Id,
                NameEnglish = x.Name_En,
                NameArabic = x.Name_Ar

            }).ToList();
        }

        /// <summary>
        /// Get news object by ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public NewsModel GetByID(long id)
        {
            News dbNews = _uow.NewsRepository.GetByID(id);

            var modelNews = new NewsModel()
            {
                Id = dbNews.News_Id,
                TitleEnglish = dbNews.Title_En,
                TitleArabic = dbNews.Title_Ar,
                //CategoryID = dbNews.News_Category_Id,
                DescriptionEnglish = dbNews.Description_En,
                DescriptionArabic = dbNews.Description_Ar,
                //CategoryName = dbNews.News_Category.Name_En,
                CreatedDate = dbNews.Created_Date

            };

            if (dbNews.News_Documents.Count > 0)
            {
                modelNews.Documents = new List<DocumentModel>();

                foreach (var item in dbNews.News_Documents)
                {
                    if (item.Document != null)
                    {
                        if (item.Document.Row_Status_Id != (long?)SZHPCMS.Common.RowStatus.Delete)
                        {
                            modelNews.Documents.Add(new DocumentModel() { DocumentId = item.Document.Document_Id, FileName = item.Document.File_Name, Extenstion = item.Document.Extenstion });
                        }
                    }
                }
            }

            return modelNews;
        }

        /// <summary>
        /// Get all news from db.
        /// </summary>
        /// <returns></returns>
        public List<NewsModel> GetAll()
        {
            var newsModelList = _uow.NewsRepository.GetAll().Select(x => new NewsModel()
            {

                Id = x.News_Id,
                TitleEnglish = x.Title_En,
                TitleArabic = x.Title_Ar,
                //CategoryID = x.News_Category_Id,
                DescriptionEnglish = x.Description_En,
                DescriptionArabic = x.Description_Ar,
                //CategoryName = x.News_Category.Name_En,
                RowStatusID = x.Row_Status_Id


            }).ToList();


            foreach (var item in newsModelList)
            {
                item.RowStatus = item.RowStatusID == null ? "N/A" : Enum.GetName(typeof(RowStatus), item.RowStatusID);
            }

            return newsModelList;
        }

        /// <summary>
        /// Get active news objects from db. Whose status are not delete.
        /// </summary>
        /// <returns></returns>
        public List<NewsModel> GetActiveNews(string dataFor = "")
        {
            var modelNews = _uow.NewsRepository.GetActiveNews(dataFor).Select(x => new NewsModel()
            {

                Id = x.News_Id,
                TitleEnglish = x.Title_En,
                TitleArabic = x.Title_Ar,
                //CategoryID = x.News_Category_Id,
                DescriptionEnglish = x.Description_En,
                DescriptionArabic = x.Description_Ar,
                //CategoryName = x.News_Category.Name_En,
                CreatedDate = x.Created_Date,
                RowStatusID = x.Row_Status_Id,
                RowStatus = Enum.GetName(typeof(RowStatus), x.Row_Status_Id),
                FileName = x.News_Documents != null ? ( x.News_Documents.Count > 0 ? x.News_Documents.First().Document.File_Name : "N/A") : "N/A"


            }).ToList();




            return modelNews;
        }

        /// <summary>
        /// Add new news obj into db . If user upload image then it also add its corresponding record for document.
        /// </summary>
        /// <param name="modelNews">News object to add</param>
        /// <returns></returns>
        public NewsModel Add(NewsModel modelNews)
        {
            if (modelNews == null)
                throw new ArgumentNullException(Constants.OBJECT_NULL_MESSAGE);

            try
            {
                News dbNews = new News()
                {
                    Title_En = modelNews.TitleEnglish,
                    Title_Ar = modelNews.TitleArabic,
                    Description_En = modelNews.DescriptionEnglish,
                    Description_Ar = modelNews.DescriptionArabic,
                    //News_Category_Id = modelNews.CategoryID,
                    Created_By = modelNews.CreatedBy,
                    Row_Status_Id = (long?)RowStatus.Active,
                    Created_Date = DateTime.Now

                };

                if (modelNews.Documents.Count > 0)
                {
                    foreach (DocumentModel item in modelNews.Documents)
                    {
                        News_Documents newsDoc = new News_Documents();
                        newsDoc.Created_Date = DateTime.Now;
                        newsDoc.Created_By = modelNews.CreatedBy;
                        newsDoc.Row_Status_Id = (long)RowStatus.Active;

                        newsDoc.Document = new Document();
                        newsDoc.Document.File_Type_Id = (long?)FileTypes.Picture;
                        newsDoc.Document.File_Name = item.FileName;
                        newsDoc.Document.Extenstion = item.Extenstion;
                        newsDoc.Document.Created_By = modelNews.CreatedBy;
                        newsDoc.Document.Row_Status_Id = (long?)RowStatus.Active;
                        newsDoc.Document.Created_Date = DateTime.Now;

                        dbNews.News_Documents.Add(newsDoc);

                    }
                }

                _uow.NewsRepository.Add(dbNews);
                _uow.Save();

                modelNews.Id = dbNews.News_Id;

                return modelNews;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Delete news object from data base
        /// </summary>
        /// <param name="menu">News object to delete</param>
        /// <returns></returns>
        public int Delete(int newsID)
        {
            News dbNews = _uow.NewsRepository.GetByID(newsID);
            dbNews.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Delete;

            return _uow.Save();
        }

        /// <summary>
        /// Update news status .
        /// </summary>
        /// <param name="menus"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int UpdateRowStatus(IEnumerable<long> newsID, RowStatus status)
        {
            foreach (var id in newsID)
            {
                News dbNews = _uow.NewsRepository.GetByID(id);
                dbNews.Row_Status_Id = (long?)status;
            }

            return _uow.Save();
        }

        /// <summary>
        /// Update news in data base
        /// </summary>
        /// <param name="modelNews">News object to update</param>
        /// <returns>Number of rows effected</returns>
        public int Update(NewsModel modelNews)
        {
            if (modelNews == null)
                throw new ArgumentNullException(Constants.OBJECT_NULL_MESSAGE);

            News dbNews = _uow.NewsRepository.GetByID(modelNews.Id);

            if (dbNews == null)
                throw new Exception(Constants.OBJECT_NOT_FOUND_MESSAGE + modelNews.Id.ToString());

            dbNews.Title_En = modelNews.TitleEnglish;
            dbNews.Title_Ar = modelNews.TitleArabic;
            //dbNews.News_Category_Id = modelNews.CategoryID;
            dbNews.Description_En = modelNews.DescriptionEnglish;
            dbNews.Description_Ar = modelNews.DescriptionArabic;
            dbNews.Updated_Date = System.DateTime.Now;

            if (modelNews.Documents.Count > 0)
            {
                foreach (var item in modelNews.Documents)
                {
                    News_Documents newsDoc = new News_Documents();
                    newsDoc.Created_Date = DateTime.Now;
                    newsDoc.Created_By = modelNews.CreatedBy;
                    newsDoc.Row_Status_Id = (long)RowStatus.Active;

                    newsDoc.Document = new Document();
                    newsDoc.Document.File_Type_Id = (long?)FileTypes.Picture;
                    newsDoc.Document.File_Name = item.FileName;
                    newsDoc.Document.Extenstion = item.Extenstion;
                    newsDoc.Document.Created_By = modelNews.CreatedBy;
                    newsDoc.Document.Row_Status_Id = (long?)RowStatus.Active;
                    newsDoc.Document.Created_Date = DateTime.Now;

                    dbNews.News_Documents.Add(newsDoc);

                }
            }
            return _uow.Save();
        }

    }
}
