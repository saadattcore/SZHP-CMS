using DataAccess.CommonRespository;
using DataAccess.Database;
using DataContract.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ContactRepository : GenericRepository<Contact_Us>, IContactRepository
    {
        public ContactRepository(ITJCMSEntities context)
            : base(context)
        {

        }
        public List<Contact_Us> GetActiveContactUs()
        {
            return _dbSet.Where(x => x.Row_Status_Id != (long?)SZHPCMS.Common.RowStatus.Delete).OrderByDescending(x => x.Created_Date).ToList();
        }


        public List<Contact_Chairman> GetActiveContactChairman()
        {
            return _context.Contact_Chairman.Where(x => x.Row_Status_Id != (long?)(SZHPCMS.Common.RowStatus.Delete)).OrderByDescending(x => x.Created_Date).ToList();
        }


        public Contact_Chairman GetContactChairmanByID(long id)
        {
            return _context.Contact_Chairman.Find(id);
        }

        public void AddContactChairman(Contact_Chairman contactChairman)
        {
            _context.Contact_Chairman.Add(contactChairman);
        }

        public List<Technical_Support_Request> GetActiveSupportRequests()
        {
            var requests = _context.Technical_Support_Request.Where(x => x.Row_Status_Id != (long?)SZHPCMS.Common.RowStatus.Delete).OrderByDescending(x => x.Created_Date).ToList();
            return requests;
        }

        public Technical_Support_Request GetSupportRequest(long id)
        {
            return _context.Technical_Support_Request.Find(id);
        }


        public List<Contact> GetActiveContacts(string dataFor = "")
        {

            return dataFor == "web" ? _context.Contacts.Where(x => x.Row_Status_Id == (long?) SZHPCMS.Common.RowStatus.Active).OrderByDescending(x => x.Created_Date).ToList() :_context.Contacts.Where(x => x.Row_Status_Id != (long?)SZHPCMS.Common.RowStatus.Delete).OrderByDescending(x => x.Created_Date).ToList();
                        
        }

        public Contact GetContactByID(long id)
        {
            return _context.Contacts.Find(id);
        }

        public void AddContact(Contact dbContact)
        {
            _context.Contacts.Add(dbContact);
        }
    }
}
