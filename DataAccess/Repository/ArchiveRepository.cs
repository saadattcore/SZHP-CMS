using DataAccess.CommonRespository;
using DataAccess.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ArchiveRepository : GenericRepository<Form>, IArchiveRepository
    {
        public ArchiveRepository(ITJCMSEntities context)
            : base(context)
        {
            
        }

        public List<Form> GetActiveForms(string dataFor = "")
        {
            return string.IsNullOrEmpty(dataFor) ? _context.Forms.Where(x => x.Row_Status_Id != (long?)SZHPCMS.Common.RowStatus.Delete).OrderByDescending(x => x.Created_Date).ToList() :  _context.Forms.Where(x => x.Row_Status_Id == (long?)SZHPCMS.Common.RowStatus.Active).OrderByDescending(x => x.Created_Date).ToList();
        }

        #region Rules & Regulation Region
        public List<Rules_And_Regulation> GetActiveRulesAndRegulation(bool isAdmin = true)
        {
            return isAdmin ? _context.Rules_And_Regulation.Where(x => x.Row_Status_Id != (long?)SZHPCMS.Common.RowStatus.Delete).OrderByDescending(x => x.Created_Date).ToList(): _context.Rules_And_Regulation.Where(x => x.Row_Status_Id == (long?)SZHPCMS.Common.RowStatus.Active).OrderByDescending(x => x.Created_Date).ToList();
        }

        public Rules_And_Regulation GetRulesById(long id)
        {
            return _context.Rules_And_Regulation.Find(id);
        }

        public void AddRulesAndRegulation(Rules_And_Regulation dbRule)
        {
            _context.Rules_And_Regulation.Add(dbRule);
        }
        #endregion

        #region Magzine Region
        public List<Magzine> GetActiveMagzines(bool isAdmin = true)
        {
            return isAdmin ? _context.Magzines.Where(x => x.Row_Status_Id != (long?)(SZHPCMS.Common.RowStatus.Delete)).OrderByDescending(x => x.Created_Date).ToList() : _context.Magzines.Where(x => x.Row_Status_Id == (long?)(SZHPCMS.Common.RowStatus.Active)).OrderByDescending(x => x.Date).ToList();
        }

        public Magzine GetMagzineById(long id)
        {
            return _context.Magzines.Find(id);
        }

        public void AddMagzine(Magzine dbMagzine)
        {
            _context.Magzines.Add(dbMagzine);
        }

        #endregion

        #region Conditions & Requirements
        public List<Conditions_And_Requirements> GetConditions(bool isAdmin = true)
        {
            return isAdmin ? _context.Conditions_And_Requirements.Where(x => x.Row_Status_Id != (long?)SZHPCMS.Common.RowStatus.Delete).OrderByDescending(x => x.Created_Date).ToList() : _context.Conditions_And_Requirements.Where(x => x.Row_Status_Id == (long?)SZHPCMS.Common.RowStatus.Active).OrderBy(x=> x.Condition_Id).ToList(); 
        }

        public Conditions_And_Requirements GetCondition(long id)
        {
            return _context.Conditions_And_Requirements.Find(id);
        }

        public void AddCondition(Conditions_And_Requirements dbCondition)
        {
            _context.Conditions_And_Requirements.Add(dbCondition);
        }

        #endregion

        #region Form Region
        public Form_Documents GetFormDoc(long id)
        {
            return _context.Form_Documents.Find(id);
        }

        public void AddFormDoc(Form_Documents dbFormDoc)
        {
            _context.Form_Documents.Add(dbFormDoc);
        } 
        #endregion
    }
}
