using DataAccess.CommonRespository;
using DataAccess.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IResearchStudyRepository : IGenericRepository<Research_Study>
    {
        /// <summary>
        /// Get active research study collection.
        /// </summary>
        /// <returns></returns>
         List<Research_Study> GetActiveResearchStudies();

        /// <summary>
        /// Get all research study categories which are not deleted
        /// </summary>
        /// <returns></returns>
         List<Research_Study_Category> GetResearchStudiesCategories();

    }
}
