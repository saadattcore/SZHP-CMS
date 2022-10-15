using DataAccess.CommonRespository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Database;

namespace DataAccess.Repository
{
    public interface ITrainingPlanRepositorycs : IGenericRepository<Training_Plan>
    {
        /// <summary>
        /// Get active training plans
        /// </summary>
        /// <returns></returns>
        List<Training_Plan> GetActivePlans();

        /// <summary>
        /// Get active training programs
        /// </summary>
        /// <returns></returns>
        List<Training_Program> GetAllTrainingProgarms();
    }
}
