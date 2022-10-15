using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Repository;
using DataAccess.CommonRespository;
using DataAccess.Database;
using SZHPCMS.Common;

namespace DataAccess.Repository
{
    public class CommonRepository : GenericRepository<Row_Statuses>, ICommonRepository
    {
        public CommonRepository(ITJCMSEntities context)
            : base(context)
        {

        }



        #region Call Center Polling Region
        public List<Call_Center_Polling> GetActiveCallCenterPolls()
        {
            return _context.Call_Center_Polling.Where(x => x.Row_Status_Id != (long?)SZHPCMS.Common.RowStatus.Delete).OrderByDescending(x => x.Created_Date).ToList();
        }

        public Call_Center_Polling GetPollById(long id)
        {
            return _context.Call_Center_Polling.Find(id);
        }
        #endregion

        #region Feed Back Region
        public List<Feed_Back> GetActiveFeedBacks()
        {
            return _context.Feed_Back.Where(x => x.Row_Status_Id != (long?)SZHPCMS.Common.RowStatus.Delete).OrderByDescending(x => x.Created_Date).ToList();
        }

        public Feed_Back GetFeedBackById(long id)
        {
            return _context.Feed_Back.Find(id);
        }
    
        #endregion

        #region Poll Region 
        public List<Poll> GetActivePolls()
        {
            return _context.Polls.Where(x => x.Row_Status_Id != (long?)RowStatus.Delete).ToList();
        }

        public void AddPoll(Poll poll)
        {
            _context.Polls.Add(poll);
        }

        public Poll GetPollByID(long id)
        {
            return _context.Polls.Find(id);
            
        }

        public List<PollOption> GetPollingResult(long pollID)
        {
            //var result = (from uv in _context.User_Votes
            //              join po in _context.PollOptions
            //              on uv.Poll_Option_Id equals po.Poll_Option_Id into ps
            //              from po in ps.DefaultIfEmpty()
            //              join poll in _context.Polls
            //              on po.Poll_Id equals poll.Poll_Id
            //              where poll.Poll_Id == pollID
            //              select po).ToList();

           var result =  _context.PollOptions.Where(x => x.Poll_Id == pollID).ToList();

            return result;
        }

        #endregion

        #region Poll Option Region
        public List<PollOption> GetActivePollOptions(string dataFor = "")
        {
            return  dataFor == "web"?  _context.PollOptions.Where(x => x.Row_Status_Id == (long?)RowStatus.Active).ToList() :  _context.PollOptions.Where(x => x.Row_Status_Id != (long?)RowStatus.Delete).ToList();
        }

        public List<PollOption> GetPollOptionsByPoll(long pollID)
        {
            return _context.PollOptions.Where(x => x.Row_Status_Id != (long?)SZHPCMS.Common.RowStatus.Delete && x.Poll_Id == pollID).ToList();
        }

        public void AddPollOption(PollOption poll)
        {
            _context.PollOptions.Add(poll);
        }

        public PollOption GetPollOptionByID(long id)
        {
            return _context.PollOptions.Find(id);
        }

        public void AddUserVote(User_Votes vote)
        {
            _context.User_Votes.Add(vote);
        }

        #endregion
                      

        #region Happiness Meter Region
        public List<Happiness_Meter> GetHappinessMeters()
        {

            return _context.Happiness_Meter.ToList();
        }

        public void AddHappinessMeter(Happiness_Meter meter)
        {
            _context.Happiness_Meter.Add(meter);
        }      

        #endregion


       
    }
}
