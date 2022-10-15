using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.CommonRespository;
using DataAccess.Database;

namespace DataAccess.Repository
{
   public interface ICannedEmailRepository : IGenericRepository<Canned_Email>
    {
       /// <summary>
       /// Get email templates from data base.
       /// </summary>
       /// <returns></returns>
       List<Canned_Email> GetActiveEmailTemplates();

    }
}
