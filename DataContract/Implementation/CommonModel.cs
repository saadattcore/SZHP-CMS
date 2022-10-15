using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataContract.Implementation
{
    public class CallCenterPollModel
    {
        public long CallCenterRatingId { get; set; }
        public Nullable<int> Rating { get; set; }
        public string RatingName { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> RowStatusId { get; set; }
        public string RowStatus { get; set; }
    }

    public class FeedBackModel
    {
        public long FeedBackId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Comments { get; set; }
        public string IPAddress { get; set; }
        public Nullable<long> PageId { get; set; }
        public string PageName { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string RowStatus { get; set; }
    }
    public class PollModel
    {
        public long Poll_Id { get; set; }
        public string Poll_En { get; set; }
        public string Poll_Ar { get; set; }
        public Nullable<System.DateTime> Created_Date { get; set; }
        public Nullable<long> Created_By { get; set; }
        public Nullable<System.DateTime> Updated_Date { get; set; }
        public Nullable<long> Updated_By { get; set; }
        public Nullable<long> Row_Status_Id { get; set; }
        public string RowStatus { get; set; }
        public List<PollOptionModel> Options { get; set; }
    }

    public class PollOptionModel
    {
        public long Poll_Option_Id { get; set; }
        public string Option_En { get; set; }
        public string Option_Ar { get; set; }

        public string Poll_En { get; set; }
        public string Poll_Ar { get; set; }
        public Nullable<long> Poll_Id { get; set; }
        public Nullable<System.DateTime> Created_Date { get; set; }
        public Nullable<long> Created_By { get; set; }
        public Nullable<System.DateTime> Updated_Date { get; set; }
        public Nullable<long> Updated_By { get; set; }
        public Nullable<long> Row_Status_Id { get; set; }
        public string RowStatus { get; set; }

    }

    public class HappinessMeterModel
    {
        public long Happiness_Meter_Id { get; set; }
        public string Rating { get; set; }
        public string Comments { get; set; }
        public string IpAddress { get; set; }
        public Nullable<System.DateTime> Created_Date { get; set; }
        public Nullable<long> Created_By { get; set; }
        public Nullable<long> Row_Status_Id { get; set; }

    }

    public class UserVoteModel
    {

        public long User_Vote_Id { get; set; }
        public long Poll_Option_Id { get; set; }
        public string IpAddress { get; set; }
        public Nullable<System.DateTime> Created_Date { get; set; }
        public Nullable<long> Created_By { get; set; }
        public Nullable<System.DateTime> Updated_Date { get; set; }
        public Nullable<long> Updated_By { get; set; }

    }

    public class VoteStatsModel
    {
        public string PollOptionEn { get; set; }
        public string PollOptionAr { get; set; }

        public long Count { get; set; }
        public int Percent { get; set; }

        public string Poll { get; set; }
    }

    public class HappinessStatsModel
    {
        public string Option { get; set; }
        public long Count { get; set; }
        public int Percent { get; set; }
              
    }
}
