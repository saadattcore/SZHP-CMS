using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Database;
using DataAccess.CommonRespository;

namespace DataAccess.Repository
{
    public interface ICareerRepository : IGenericRepository<Career_Job>
    {
        /// <summary>
        /// Get active jobs.
        /// </summary>
        /// <returns></returns>
        List<Career_Job> GetActiveJobs();

        /// <summary>
        /// Get career industries.
        /// </summary>
        /// <returns></returns>
        IList<Career_Industry> GetCareerIndustries();

        /// <summary>
        /// Get types of career types.
        /// </summary>
        /// <returns></returns>
        IList<Career_Type> GetCareerTypes();

        /// <summary>
        /// Get job applicants.
        /// </summary>
        /// <returns></returns>
        List<Career_Job_Applied> GetJobApplicants();

        /// <summary>
        /// Get applicant by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Career_Job_Applied GetApplicantByID(long id);

        /// <summary>
        /// Add Industry .
        /// </summary>
        /// <param name="industry"></param>
        /// <returns></returns>
        void AddIndustry(Career_Industry industry);
        
        /// <summary>
        /// Get Industry.
        /// </summary>
        /// <returns></returns>
        List<Career_Industry> GetIndustries();

        /// <summary>
        /// Get industry by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Career_Industry GetIndustryByID(long id);

    }
}
