using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.CommonRespository;
using DataAccess.Database;

namespace DataAccess.Repository
{
    public class NewsRepository : GenericRepository<News>, INewsRepository
    {
        public NewsRepository(ITJCMSEntities context)
            : base(context)
        {

        }

        public List<News_Category> GetNewsCategories()
        {
            return this._context.News_Category.ToList();
        }

        public News_Documents AddNewsDoc(News_Documents newsDoc)
        {
           return  this._context.News_Documents.Add(newsDoc);
        }

        public List<News> GetActiveNews(string dataFor = "")
        {
            return dataFor == "web" ? this._dbSet.Where(x => x.Row_Status_Id == (long?)SZHPCMS.Common.RowStatus.Active).OrderByDescending(x => x.Created_Date).ToList() :  this._dbSet.Where(x => x.Row_Status_Id != (long?)SZHPCMS.Common.RowStatus.Delete).OrderByDescending(x => x.Created_Date).ToList();
        }
    }
}
