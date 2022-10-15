using DataAccess.CommonRespository;
using DataAccess.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class TrainingPlanRepository : GenericRepository<Training_Plan>, ITrainingPlanRepositorycs
    {
        public TrainingPlanRepository(ITJCMSEntities context)
            : base(context)
        {

        }

        public List<Training_Plan> GetActivePlans()
        {
            return this._dbSet.Where(x => x.Row_Status_Id != (long?)SZHPCMS.Common.RowStatus.Delete).ToList();
        }

        public List<Training_Program> GetAllTrainingProgarms()
        {
            return this._context.Training_Program.Where(x => x.Row_Status_Id != (long?)SZHPCMS.Common.RowStatus.Delete).OrderByDescending(x => x.Created_Date).ToList();
        }
    }
}
