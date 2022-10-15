using DataAccess.CommonRespository;
using DataAccess.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class CareerRepository : GenericRepository<Career_Job>, ICareerRepository
    {
        public CareerRepository(ITJCMSEntities context)
            : base(context)
        {

        }
        public List<Career_Job> GetActiveJobs()
        {
            return this._dbSet.Where(x => x.Row_Status_Id != (long?)SZHPCMS.Common.RowStatus.Delete).OrderByDescending(x => x.Created_Date).ToList();
        }
        public IList<Career_Industry> GetCareerIndustries()
        {
            return this._context.Career_Industry.Where(x => x.Row_Status_Id != (long?)SZHPCMS.Common.RowStatus.Delete).ToList();
        }
        public IList<Career_Type> GetCareerTypes()
        {
            return this._context.Career_Type.Where(x => x.Row_Status_Id != (long?)SZHPCMS.Common.RowStatus.Delete).ToList();
        }
        public List<Career_Job_Applied> GetJobApplicants()
        {
            return this._context.Career_Job_Applied.Where(x => x.Career_Job != null).OrderByDescending(x => x.Created_Date).ToList();
        }
        public Career_Job_Applied GetApplicantByID(long id)
        {
            return this._context.Career_Job_Applied.Find(id);
        }


        public void AddIndustry(Career_Industry industry)
        {
            _context.Career_Industry.Add(industry);
        }

        public List<Career_Industry> GetIndustries()
        {
            return _context.Career_Industry.Where(x => x.Row_Status_Id != (long?)SZHPCMS.Common.RowStatus.Delete).ToList();
        }

        public Career_Industry GetIndustryByID(long id)
        {
            return _context.Career_Industry.Find(id);
        }
    }
}
