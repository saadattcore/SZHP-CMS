using DataAccess.CommonRespository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Database;

namespace DataAccess.Repository
{
    public interface IGalleryRepository : IGenericRepository<Gallery> 
    {
        List<Gallery> GetActiveGalleries();
    }
}
