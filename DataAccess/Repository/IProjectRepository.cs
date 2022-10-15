using DataAccess.CommonRespository;
using DataAccess.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IProjectRepository : IGenericRepository<Project>
    {
        /// <summary>
        /// Get active projects
        /// </summary>
        /// <returns></returns>
        List<Project> GetActiveProjects(string dataFor = "");

        /// <summary>
        /// Get active open data
        /// </summary>
        /// <returns></returns>
        List<Open_Data> GetActiveOpenData(string dataFor = "");

        /// <summary>
        /// Get open data by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Open_Data GetOpenDataById(long id);

        /// <summary>
        /// Add open  data.
        /// </summary>
        /// <param name="dbOpenData"></param>
        void AddOpenData(Open_Data dbOpenData);

        
    }
}
