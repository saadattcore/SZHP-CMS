using DataAccess.CommonRespository;
using DataAccess.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IHomeSettingRepository : IGenericRepository<Home_Setting>
    {
        /// <summary>
        /// Get home setting object . It is the only object should reside in table
        /// </summary>
        /// <returns></returns>
        Home_Setting GetHomeSetting();

        /// <summary>
        /// Get mobile apps .
        /// </summary>
        /// <returns></returns>
        List<Mobile_App> GetMobileApps(string dataFor = "");

        /// <summary>
        /// Get mobile app by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        Mobile_App GetAppById(long id);

        /// <summary>
        /// Add mobile app
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        void AddMobileApp(Mobile_App app);

        /// <summary>
        /// Get partner services
        /// </summary>
        /// <returns></returns>
        List<Partner_Service> GetPartnerServices(string dataFor = "");

        /// <summary>
        /// Get partner service by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        Partner_Service GetPartnerServiceById(long id);

        /// <summary>
        /// Add new partner service .
        /// </summary>
        /// <param name="dbPartnerService"></param>

        void AddPartnerService(Partner_Service dbPartnerService);
        

    }
}
