using DataAccess.CommonRespository;
using DataAccess.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface ISocialMediaRepository : IGenericRepository<Social_Media>
    {
        List<Social_Media> GetActiveSocialMedia(bool isAdmin = true);
    }
}
