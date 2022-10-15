using DataAccess.CommonRespository;
using DataAccess.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ProjectRepository : GenericRepository<Project>, IProjectRepository
    {
        public ProjectRepository(ITJCMSEntities context) : base(context)
        {

        }

        public List<Project> GetActiveProjects(string dataFor = "")
        {
            return string.IsNullOrEmpty(dataFor) ? _dbSet.Where(x => x.Row_Status_Id != (long?)SZHPCMS.Common.RowStatus.Delete).OrderByDescending(x => x.Created_Date).ToList() : _dbSet.Where(x => x.Row_Status_Id == (long?)SZHPCMS.Common.RowStatus.Active).OrderByDescending(x => x.Created_Date).ToList();        
        }
        public List<Open_Data> GetActiveOpenData(string dataFor = "")
        {
            return string.IsNullOrEmpty(dataFor) ? _context.Open_Data.Where(x => x.Row_Status_Id != (long?)SZHPCMS.Common.RowStatus.Delete).ToList() : _context.Open_Data.Where(x => x.Row_Status_Id == (long?)SZHPCMS.Common.RowStatus.Active).ToList();
            
        }
        public Open_Data GetOpenDataById(long id)
        {
            return _context.Open_Data.Find(id);
        }
        public void AddOpenData(Open_Data dbOpenData)
        {
            _context.Open_Data.Add(dbOpenData);
        }
    }
}
