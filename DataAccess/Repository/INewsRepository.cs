using DataAccess.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.CommonRespository;

namespace DataAccess.Repository
{
    public interface INewsRepository : IGenericRepository<News>
    {
        List<News_Category> GetNewsCategories();
        News_Documents AddNewsDoc(News_Documents newsDoc);

        /// <summary>
        /// Get active news.
        /// </summary>
        /// <returns></returns>
        List<News> GetActiveNews(string dataFor = "");
    }
}
