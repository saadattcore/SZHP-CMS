using DataAccess.CommonRespository;
using DataAccess.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    class EServiceRepository : GenericRepository<E_Services>, IEServiceRepository
    {
        public EServiceRepository(ITJCMSEntities context)
            : base(context)
        {

        }

        public List<E_Services> GetActiveEServices()
        {
            return this._dbSet.Where(x => x.Row_Status_Id != (long?)SZHPCMS.Common.RowStatus.Delete).OrderByDescending(x => x.Created_Date).ToList();
        }

        public List<E_Participation> GetActiveEParticipations(bool isAdmin = true)
        {
            return isAdmin ?  _context.E_Participation.Where(x => x.Row_Status_Id != (long?)SZHPCMS.Common.RowStatus.Delete).OrderByDescending(x => x.Created_Date).ToList() : _context.E_Participation.Where(x => x.Row_Status_Id == (long?)SZHPCMS.Common.RowStatus.Active).OrderByDescending(x => x.Created_Date).ToList();
        }

        public E_Participation GetEParticipationById(long id)
        {
            return _context.E_Participation.Find(id);
        }

        public void AddEParticipation(E_Participation dbParticipation)
        {
            _context.E_Participation.Add(dbParticipation);
        }

        public List<E_Service_Category> GetServiceCategories(string dataFor ="")
        {
            return string.IsNullOrEmpty(dataFor) ? _context.E_Service_Category.Where(x => x.Row_Status_Id != (long?)SZHPCMS.Common.RowStatus.Delete).OrderByDescending(x => x.Created_Date).ToList() :  _context.E_Service_Category.Where(x => x.Row_Status_Id == (long?)SZHPCMS.Common.RowStatus.Active).OrderByDescending(x => x.Created_Date).ToList();

        }


        public E_Service_Category GetServiceCategoryById(long id)
        {
            return _context.E_Service_Category.Find(id);
        }

        public void AddServiceCategory(E_Service_Category dbCat)
        {
            _context.E_Service_Category.Add(dbCat);
        }
    }
}
