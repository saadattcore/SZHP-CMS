using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Database;
using DataAccess.CommonRespository;

namespace DataAccess.Repository
{
    public interface ICommonRepository : IGenericRepository<Row_Statuses>
    {
        List<Call_Center_Polling> GetActiveCallCenterPolls();

        Call_Center_Polling GetPollById(long id);

        /// <summary>
        /// Get active feed backs.
        /// </summary>
        /// <returns></returns>
        List<Feed_Back> GetActiveFeedBacks();

        /// <summary>
        /// Get feed back by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Feed_Back GetFeedBackById(long id);

        /// <summary>
        /// Get active polls
        /// </summary>
        /// <returns></returns>
        List<Poll> GetActivePolls();

        /// <summary>
        /// Add Poll
        /// </summary>
        /// <param name="poll"></param>

         void AddPoll(Poll poll);

        /// <summary>
        /// Get poll by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
         Poll GetPollByID(long id);

        /// <summary>
        /// Get active polls options
        /// </summary>
        /// <returns></returns>
         List<PollOption> GetActivePollOptions(string dataFor = "");

         /// <summary>
         /// Add Poll
         /// </summary>
         /// <param name="poll"></param>

         void AddPollOption(PollOption poll);

         /// <summary>
         /// Get poll by id
         /// </summary>
         /// <param name="id"></param>
         /// <returns></returns>
         PollOption GetPollOptionByID(long id);

        /// <summary>
        /// Get happiness meter
        /// </summary>
        /// <returns></returns>
         List<Happiness_Meter> GetHappinessMeters();

        /// <summary>
        /// Add happiness meter
        /// </summary>
        /// <param name="meter"></param>
         void AddHappinessMeter(Happiness_Meter meter);

        /// <summary>
        /// Add user vote for polling
        /// </summary>
        /// <param name="vote"></param>
         void AddUserVote(User_Votes vote);
          
         /// <summary>
         /// get polling results
         /// </summary>
         /// <param name="pollID"></param>
         /// <returns></returns>

         List<PollOption> GetPollingResult(long pollID);

        /// <summary>
        /// get polloptions by pollid 
        /// </summary>
        /// <param name="pollID"></param>
        /// <returns></returns>
         List<PollOption> GetPollOptionsByPoll(long pollID);


    }
}
