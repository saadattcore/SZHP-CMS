using DataAccess.CommonRespository;
using DataAccess.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IEventRepository : IGenericRepository<Event>
    {
        /// <summary>
        /// Get all events which are not deleted
        /// </summary>
        /// <returns></returns>
        List<Event> GetActiveEvents(string dataFor = "");

        /// <summary>
        /// Get all event types which are not deleted
        /// </summary>
        /// <returns></returns>
        IEnumerable<Event_Type> GetActiveEventTypes();

        /// <summary>
        /// Get upcoming events . Events whose start date >= todays
        /// </summary>
        /// <returns></returns>
        List<Event> GetUpcomingEvents();

    }
}
