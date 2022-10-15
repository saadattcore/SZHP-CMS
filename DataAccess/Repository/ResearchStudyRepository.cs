using DataAccess.CommonRespository;
using DataAccess.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    class ResearchStudyRepository : GenericRepository<Research_Study>, IResearchStudyRepository
    {
        public ResearchStudyRepository(ITJCMSEntities context)
            : base(context)
        {

        }
   
        public List<Research_Study> GetActiveResearchStudies()
        {

            return this._dbSet.Where(x => x.Row_Status_Id != (long?)SZHPCMS.Common.RowStatus.Delete).OrderByDescending(x => x.Created_Date).ToList();
        }


        public List<Research_Study_Category> GetResearchStudiesCategories()
        {
            return this._context.Research_Study_Category.Where(x => x.Row_Status_Id != (long?)SZHPCMS.Common.RowStatus.Delete).ToList();
        }
    }
}
