using DataAccess.CommonRespository;
using DataAccess.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    class EventRepository : GenericRepository<Event>, IEventRepository
    {
        public EventRepository(ITJCMSEntities context)
            : base(context)
        {

        }

        public List<Event> GetActiveEvents(string dataFor = "")
        {

            return dataFor == "web" ? _dbSet.Where(x => x.Row_Status_Id == (long?)SZHPCMS.Common.RowStatus.Active).OrderByDescending(x => x.Created_Date).ToList().ToList() : this._dbSet.Where(e => e.Row_Status_Id != (long?)SZHPCMS.Common.RowStatus.Delete).OrderByDescending(x => x.Created_Date).ToList();

        }

        public IEnumerable<Event_Type> GetActiveEventTypes()
        {
            return this._context.Event_Type.Where(x => x.Row_Status_Id != (long?)SZHPCMS.Common.RowStatus.Delete);
        }


        public List<Event> GetUpcomingEvents()
        {
            return _context.Events.Where(x => x.Row_Status_Id == (long?)SZHPCMS.Common.RowStatus.Active &&  System.Data.Entity.DbFunctions.TruncateTime(x.StartDate.Value) >= DateTime.Today).ToList();
        }
    }
}
