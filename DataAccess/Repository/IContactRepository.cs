using DataAccess.CommonRespository;
using DataAccess.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IContactRepository : IGenericRepository<Contact_Us>
    {
        /// <summary>
        /// Get active contact us list.
        /// </summary>
        /// <returns></returns>
        List<Contact_Us> GetActiveContactUs();

        /// <summary>
        /// Get users who contacted to chairman.
        /// </summary>
        /// <returns></returns>
        List<Contact_Chairman> GetActiveContactChairman();

        
        /// <summary>
        /// Get person who has contacted chariman by id .
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Contact_Chairman GetContactChairmanByID(long id);

        /// <summary>
        /// Add contact chairman to table
        /// </summary>
        /// <param name="contactChairman"></param>
        void AddContactChairman(Contact_Chairman contactChairman);

        /// <summary>
        /// Get all active support requests.
        /// </summary>
        /// <returns></returns>
        List<Technical_Support_Request> GetActiveSupportRequests();

        /// <summary>
        /// Get support request by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Technical_Support_Request GetSupportRequest(long id);

        /// <summary>
        /// Get active contacts.
        /// </summary>
        /// <returns></returns>
        List<Contact> GetActiveContacts(string dataFor);

        /// <summary>
        /// Get contact by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Contact GetContactByID(long id);
        
        /// <summary>
        /// Add contact to data base.
        /// </summary>
        /// <param name="dbContact"></param>
        void AddContact(Contact dbContact);

    }
}
