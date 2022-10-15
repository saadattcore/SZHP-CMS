using DataAccess.CommonRespository;
using DataAccess.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class HomeSettingRepository : GenericRepository<Home_Setting>, IHomeSettingRepository
    {
        public HomeSettingRepository(ITJCMSEntities context)
            : base(context)
        {

        }

        public Home_Setting GetHomeSetting()
        {
            return this._dbSet.FirstOrDefault();
        }

        #region Mobile App Region
        public List<Mobile_App> GetMobileApps(string dataFor = "")
        {
            var apps = dataFor == "web" ?  this._context.Mobile_App.Where(x => x.Row_Status_Id == (long?)SZHPCMS.Common.RowStatus.Active).OrderByDescending(x => x.Created_Date).ToList(): this._context.Mobile_App.Where(x => x.Row_Status_Id != (long?)SZHPCMS.Common.RowStatus.Delete).OrderByDescending(x => x.Created_Date).ToList();
            return apps;
        }

        public Mobile_App GetAppById(long id)
        {
            return _context.Mobile_App.Where(x => x.Mobile_Application_Id == id).FirstOrDefault();
        }

        public void AddMobileApp(Mobile_App app)
        {
            _context.Mobile_App.Add(app);
        }
        #endregion

        #region Partner Service Region
        public List<Partner_Service> GetPartnerServices(string dataFor = "")
        {
            return dataFor == "web" ? _context.Partner_Service.Where(x => x.Row_Status_Id == (long)SZHPCMS.Common.RowStatus.Active).OrderByDescending(x => x.Created_Date).ToList() : _context.Partner_Service.Where(x => x.Row_Status_Id != (long)SZHPCMS.Common.RowStatus.Delete).OrderByDescending(x => x.Created_Date).ToList();
        }

        public Partner_Service GetPartnerServiceById(long id)
        {
            return _context.Partner_Service.Find(id);
        }

        public void AddPartnerService(Partner_Service dbPartnerService)
        {
            _context.Partner_Service.Add(dbPartnerService);
        }
        #endregion

    }
}
