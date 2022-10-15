using DataAccess.CommonRespository;
using DataAccess.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IEServiceRepository : IGenericRepository<E_Services>
    {
        /// <summary>
        /// Get all active e services objects
        /// </summary>
        /// <returns></returns>
        List<E_Services> GetActiveEServices();

        /// <summary>
        /// Get active participations
        /// </summary>
        /// <returns></returns>
        List<E_Participation> GetActiveEParticipations(bool isAdmin = true);

        /// <summary>
        /// Get active e-participation by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        E_Participation GetEParticipationById(long id);

        /// <summary>
        /// Add new e-participation record to data base.
        /// </summary>
        /// <param name="participation"></param>
        void AddEParticipation(E_Participation participation);

        /// <summary>
        /// Get service categories.
        /// </summary>
        /// <returns></returns>
        List<E_Service_Category> GetServiceCategories(string dataFor = "");

        /// <summary>
        /// Get e-service category by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        E_Service_Category GetServiceCategoryById(long id);

        /// <summary>
        /// Add e-service category .
        /// </summary>
        /// <param name="dbCat"></param>
        void AddServiceCategory(E_Service_Category dbCat);




    }
}
