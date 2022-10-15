using DataAccess.CommonRespository;
using DataAccess.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IArchiveRepository : IGenericRepository<Form> 
    {

        /// <summary>
        /// Get active forms
        /// </summary>
        /// <returns></returns>
        List<Form> GetActiveForms(string dataFor = "");

        /// <summary>
        /// Get active rules and regulation
        /// </summary>
        /// <returns></returns>
        List<Rules_And_Regulation> GetActiveRulesAndRegulation(bool isAdmin = true);

        /// <summary>
        /// Get rules and regulation by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Rules_And_Regulation GetRulesById(long id);

        /// <summary>
        /// add new rules and regulation record in data base.
        /// </summary>
        /// <param name="dbRule"></param>
        void AddRulesAndRegulation(Rules_And_Regulation dbRule);

        /// <summary>
        /// Get active magzines.
        /// </summary>
        /// <returns></returns>
        List<Magzine> GetActiveMagzines(bool isAdmin = true);

        /// <summary>
        /// Get magzine by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Magzine GetMagzineById(long id);

        /// <summary>
        /// Add magzine 
        /// </summary>
        /// <param name="dbMagzine"></param>
        void AddMagzine(Magzine dbMagzine);

        /// <summary>
        /// Get active conditions
        /// </summary>
        /// <returns></returns>
        List<Conditions_And_Requirements> GetConditions(bool isAdmin = true);

        /// <summary>
        /// Get condition by id .
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        Conditions_And_Requirements GetCondition(long id);

        /// <summary>
        /// Add condition 
        /// </summary>
        /// <param name="dbCondition"></param>

        void AddCondition(Conditions_And_Requirements dbCondition);

        /// <summary>
        /// Get form Doc by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Form_Documents GetFormDoc(long id);

        /// <summary>
        /// Add form doc 
        /// </summary>
        /// <param name="dbFormDoc"></param>
        void AddFormDoc(Form_Documents dbFormDoc);
    }
}
