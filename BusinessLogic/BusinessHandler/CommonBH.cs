using SZHPCMS.Common;
using DataAccess.CommonRespository;
using DataAccess.Database;
using DataContract.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.BusinessHandler
{
    public class CommonBH
    {
        private readonly IUnitOfWork _uow;
        public CommonBH(UnitOfWork uow)
        {
            _uow = uow;
        }


        #region Call Center Region

        /// <summary>
        /// Get active polls for call center .
        /// </summary>
        /// <returns></returns>
        public List<CallCenterPollModel> GetActiveCallCenterPolls()
        {
            return _uow.CommonRepository.GetActiveCallCenterPolls().Select(x => new CallCenterPollModel()
            {
                CallCenterRatingId = x.Call_Center_Rating_Id,
                RatingName = x.Rating == (int?)SZHPCMS.Common.Rating.VerySatisfied ? "Very Satisfied" : Enum.GetName(typeof(SZHPCMS.Common.Rating), x.Rating),
                Rating = x.Rating,
                RowStatus = Enum.GetName(typeof(SZHPCMS.Common.RowStatus), x.Row_Status_Id),
                CreatedDate = x.Created_Date
            }).ToList();
        }

        /// <summary>
        /// Delete poll object from data base . It should be soft delete i.e just update row status to delete
        /// </summary>
        /// <param name="menu">FAQ object to delete</param>
        /// <returns></returns>
        public int Delete(int id)
        {
            Call_Center_Polling dbPoll = _uow.CommonRepository.GetPollById(id);
            dbPoll.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Delete;

            return _uow.Save();
        }

        /// <summary>
        /// Update Poll's status .
        /// </summary>
        /// <param name="menus"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int UpdateRowStatus(IEnumerable<long> idList, RowStatus status)
        {
            foreach (var id in idList)
            {
                Call_Center_Polling dbPoll = _uow.CommonRepository.GetPollById(id);
                dbPoll.Row_Status_Id = (long?)status;
            }

            return _uow.Save();
        }

        #endregion

        #region Feed Back Region

        /// <summary>
        /// Get active feed backs .
        /// </summary>
        /// <returns></returns>
        public List<FeedBackModel> GetFeedBacks()
        {
            return _uow.CommonRepository.GetActiveFeedBacks().Select(x => new FeedBackModel()
            {
                FeedBackId = x.Feed_Back_Id,
                Name = x.Name,
                Email = x.Email,
                Comments = x.Comments,
                PageName = x.Section,
                RowStatus = Enum.GetName(typeof(SZHPCMS.Common.RowStatus), x.Row_Status_Id),
                CreatedDate = x.Created_Date
            }).ToList();
        }

        /// <summary>
        /// Delete feed back object from data base . It should be soft delete i.e just update row status to delete
        /// </summary>
        /// <param name="menu">Feed back object to delete</param>
        /// <returns></returns>
        public int DeleteFeedBack(int id)
        {
            Feed_Back dbFeedBack = _uow.CommonRepository.GetFeedBackById(id);
            dbFeedBack.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Delete;

            return _uow.Save();
        }

        /// <summary>
        /// Update Poll's status .
        /// </summary>
        /// <param name="menus"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int UpdateRowStatusFeedBack(IEnumerable<long> idList, RowStatus status)
        {
            foreach (var id in idList)
            {
                Feed_Back dbFeedBack = _uow.CommonRepository.GetFeedBackById(id);
                dbFeedBack.Row_Status_Id = (long?)status;
            }

            return _uow.Save();
        }

        #endregion

        #region Poll Region
        public List<PollModel> GetActivePolls()
        {
            List<Poll> dbPolls = _uow.CommonRepository.GetActivePolls().OrderByDescending(x=>x.Created_Date).ToList();

            List<PollModel> modelPolls = new List<PollModel>();

            foreach (var x in dbPolls)
            {
                PollModel modelPoll = new PollModel();

                modelPoll.Poll_Id = x.Poll_Id;
                modelPoll.Poll_Ar = x.Poll_Ar;
                modelPoll.Poll_En = x.Poll_En;
                modelPoll.Created_Date = x.Created_Date;
                modelPoll.Created_By = x.Created_By;
                modelPoll.RowStatus = Enum.GetName(typeof(SZHPCMS.Common.RowStatus), x.Row_Status_Id);
                modelPoll.Updated_Date = x.Updated_Date;
                modelPoll.Updated_By = x.Updated_By;
                modelPoll.Row_Status_Id = x.Row_Status_Id;

                if (x.PollOptions != null)
                {
                    if (x.PollOptions.Count > 0)
                    {
                        modelPoll.Options = new List<PollOptionModel>();

                        foreach (var item in x.PollOptions)
                        {
                            PollOptionModel pollOptionModel = new PollOptionModel();

                            pollOptionModel.Poll_Option_Id = item.Poll_Option_Id;
                            pollOptionModel.Option_En = item.Option_En;
                            pollOptionModel.Option_Ar = item.Option_Ar;
                            pollOptionModel.Created_Date = item.Created_Date;
                            pollOptionModel.Created_By = item.Created_By;
                            pollOptionModel.Updated_Date = item.Updated_Date;
                            pollOptionModel.Updated_By = item.Updated_By;
                            pollOptionModel.Row_Status_Id = item.Row_Status_Id;
                            modelPoll.Options.Add(pollOptionModel);

                        }

                    }

                }

                modelPolls.Add(modelPoll);
            }

            return modelPolls;
        }

        public PollModel AddPoll(PollModel pollModel)
        {
            if (pollModel == null)
                throw new ArgumentNullException(Constants.OBJECT_NULL_MESSAGE);

            try
            {
                Poll dbPoll = new Poll();

                dbPoll.Poll_Ar = pollModel.Poll_Ar;
                dbPoll.Poll_En = pollModel.Poll_En;
                dbPoll.Created_By = pollModel.Created_By;
                dbPoll.Created_Date = DateTime.Now;
                dbPoll.Row_Status_Id = (long?)RowStatus.Active;

                _uow.CommonRepository.AddPoll(dbPoll);
                _uow.Save();

                pollModel.Poll_Id = dbPoll.Poll_Id;

                return pollModel;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public int UpdatePoll(PollModel model)
        {
            if (model == null)
                throw new ArgumentNullException("Banner model cannot be null .");

            Poll dbPoll = _uow.CommonRepository.GetPollByID(model.Poll_Id);

            if (dbPoll == null)
                throw new Exception("Banner with id = " + model.Poll_Id + " was not found");


            dbPoll.Poll_Ar = model.Poll_Ar;
            dbPoll.Poll_En = model.Poll_En;
            dbPoll.Updated_By = model.Updated_By;
            dbPoll.Updated_Date = DateTime.Now;

            return _uow.Save();

        }

        /// <summary>
        /// Get poll by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PollModel GetPollByID(long id)
        {
            // var obj = _uow.BannerRepository.GetBanner(1, 2);

            var dbPoll = _uow.CommonRepository.GetPollByID(id);

            if (dbPoll == null)
                throw new Exception("Poll Not Found.");

            PollModel modelPoll = new PollModel();

            modelPoll.Poll_Id = dbPoll.Poll_Id;
            modelPoll.Poll_En = dbPoll.Poll_En;
            modelPoll.Poll_Ar = dbPoll.Poll_Ar;
            modelPoll.Created_Date = dbPoll.Created_Date;
            modelPoll.RowStatus = Enum.GetName(typeof(SZHPCMS.Common.RowStatus), dbPoll.Row_Status_Id);

            if (dbPoll.PollOptions.Count > 0)
            {
                modelPoll.Options = new List<PollOptionModel>();

                //   dbBanner.Banner_Documents = dbBanner.Banner_Documents.Where(x => x.Row_Status_Id != (long?)Common.RowStatus.Delete).ToList();

                foreach (var item in dbPoll.PollOptions)
                {
                    if (item.Row_Status_Id != (long?)SZHPCMS.Common.RowStatus.Delete)
                    {
                        modelPoll.Options.Add(new PollOptionModel() { Poll_Id = item.Poll_Id, Option_En = item.Option_En, Option_Ar = item.Option_Ar, Created_Date = item.Created_Date });
                    }

                }
            }

            return modelPoll;
        }

        /// <summary>
        /// Delete Poll from db . It will be a soft delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeletePoll(int id)
        {
            Poll dbPoll = _uow.CommonRepository.GetPollByID(id);

            dbPoll.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Delete;

            // Delete related images also .
            if (dbPoll.PollOptions.Count > 0)
            {
                foreach (var item in dbPoll.PollOptions)
                {
                    item.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Delete;
                }
            }

            return _uow.Save();
        }

        /// <summary>
        /// Bulk update Poll row status
        /// </summary>
        /// <param name="idList"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int UpdateRowStatusPoll(IEnumerable<long> idList, RowStatus status)
        {
            foreach (var id in idList)
            {
                Poll dbPoll = _uow.CommonRepository.GetPollByID(id);
                dbPoll.Row_Status_Id = (long?)status;
            }

            return _uow.Save();
        }


        #endregion
        
        #region Polls Options
        public List<PollOptionModel> GetActivePollOptions(string dataFor = "")
        {
            List<PollOption> dbPolls = _uow.CommonRepository.GetActivePollOptions();

            List<PollOptionModel> modelPolls = new List<PollOptionModel>();

            foreach (var x in dbPolls)
            {
                PollOptionModel modelPoll = new PollOptionModel();

                modelPoll.Poll_Option_Id = x.Poll_Option_Id;
                modelPoll.Option_En = x.Option_En;
                modelPoll.Option_Ar = x.Option_Ar;
                modelPoll.Created_Date = x.Created_Date;
                modelPoll.Created_By = x.Created_By;
                modelPoll.RowStatus = Enum.GetName(typeof(SZHPCMS.Common.RowStatus), x.Row_Status_Id);
                modelPoll.Updated_Date = x.Updated_Date;
                modelPoll.Updated_By = x.Updated_By;
                modelPoll.Row_Status_Id = x.Row_Status_Id;

                modelPolls.Add(modelPoll);
            }

            return modelPolls;
        }

        public List<PollOptionModel> GetPollsByPoll(long pollID)
        {
            List<PollOption> dbPolls = _uow.CommonRepository.GetPollOptionsByPoll(pollID);

            List<PollOptionModel> modelPolls = new List<PollOptionModel>();

            foreach (var x in dbPolls)
            {
                PollOptionModel modelPoll = new PollOptionModel();

                modelPoll.Poll_Option_Id = x.Poll_Option_Id;
                modelPoll.Option_En = x.Option_En;
                modelPoll.Option_Ar = x.Option_Ar;
                modelPoll.Created_Date = x.Created_Date;
                modelPoll.Created_By = x.Created_By;
                modelPoll.RowStatus = Enum.GetName(typeof(SZHPCMS.Common.RowStatus), x.Row_Status_Id);
                modelPoll.Updated_Date = x.Updated_Date;
                modelPoll.Updated_By = x.Updated_By;
                modelPoll.Row_Status_Id = x.Row_Status_Id;
                modelPoll.Poll_En = x.Poll.Poll_En;
                modelPoll.Poll_Ar = x.Poll.Poll_Ar;
                modelPolls.Add(modelPoll);
            }

            return modelPolls;
        }

        public PollOptionModel AddPollOptions(PollOptionModel pollModel)
        {
            if (pollModel == null)
                throw new ArgumentNullException(Constants.OBJECT_NULL_MESSAGE);

            try
            {
                PollOption dbPoll = new PollOption();

                dbPoll.Option_Ar = pollModel.Option_Ar;
                dbPoll.Option_En = pollModel.Option_En;
                dbPoll.Created_By = pollModel.Created_By;
                dbPoll.Created_Date = DateTime.Now;
                dbPoll.Row_Status_Id = (long?)RowStatus.Active;
                dbPoll.Poll_Id = pollModel.Poll_Id;

                _uow.CommonRepository.AddPollOption(dbPoll);
                _uow.Save();

                pollModel.Poll_Id = dbPoll.Poll_Id;

                return pollModel;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public int UpdatePollOption(PollOptionModel model)
        {
            if (model == null)
                throw new ArgumentNullException("Banner model cannot be null .");

            PollOption dbPollOption = _uow.CommonRepository.GetPollOptionByID(model.Poll_Option_Id);

            if (dbPollOption == null)
                throw new Exception("Banner with id = " + model.Poll_Id + " was not found");


            dbPollOption.Option_Ar = model.Option_Ar;
            dbPollOption.Option_En = model.Option_En;
            dbPollOption.Updated_By = model.Updated_By;
            dbPollOption.Updated_Date = DateTime.Now;

            return _uow.Save();

        }

        /// <summary>
        /// Get poll by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PollOptionModel GetPollOptionByID(long id)
        {
            // var obj = _uow.BannerRepository.GetBanner(1, 2);

            var dbPoll = _uow.CommonRepository.GetPollOptionByID(id);

            if (dbPoll == null)
                throw new Exception("Poll Not Found.");

            PollOptionModel modelPoll = new PollOptionModel();

            modelPoll.Poll_Option_Id = dbPoll.Poll_Option_Id;
            modelPoll.Option_Ar = dbPoll.Option_Ar;
            modelPoll.Option_En = dbPoll.Option_En;
            modelPoll.Created_Date = dbPoll.Created_Date;
            modelPoll.RowStatus = Enum.GetName(typeof(SZHPCMS.Common.RowStatus), dbPoll.Row_Status_Id);
            modelPoll.Poll_Id = dbPoll.Poll_Id;

            return modelPoll;
        }

        /// <summary>
        /// Delete Poll Option from db . It will be a soft delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeletePollOption(int id)
        {
            PollOption dbPollOption = _uow.CommonRepository.GetPollOptionByID(id);

            dbPollOption.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Delete;

            return _uow.Save();
        }

        /// <summary>
        /// Bulk update Poll Option row status
        /// </summary>
        /// <param name="idList"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int UpdateRowStatusPollOption(IEnumerable<long> idList, RowStatus status)
        {
            foreach (var id in idList)
            {
                PollOption dbPoll = _uow.CommonRepository.GetPollOptionByID(id);
                dbPoll.Row_Status_Id = (long?)status;
            }

            return _uow.Save();
        } 
        #endregion
             
        #region Happiness Meter Region
        public List<HappinessMeterModel> GetHappinessMeters()
        {
            return _uow.CommonRepository.GetHappinessMeters().Select(x => new HappinessMeterModel()
            {

                Rating = x.Rating,
                Comments = x.Comments,
                Created_By = x.Created_By,
                Created_Date = x.Created_Date,
                Happiness_Meter_Id = x.Happiness_Meter_Id,
                IpAddress = x.IPAddress


            }).ToList();
        }

        public HappinessMeterModel AddHappinessMeter(HappinessMeterModel meter)
        {
            if (meter == null)
                throw new ArgumentNullException(Constants.OBJECT_NULL_MESSAGE);

            try
            {
                Happiness_Meter dbMeter = new Happiness_Meter();

                dbMeter.Rating = meter.Rating;
                dbMeter.Comments = meter.Comments;
                dbMeter.Created_Date = DateTime.Now;
                dbMeter.IPAddress = meter.IpAddress;
                dbMeter.Created_By = meter.Created_By;

                _uow.CommonRepository.AddHappinessMeter(dbMeter);

                _uow.Save();

                meter.Happiness_Meter_Id = dbMeter.Happiness_Meter_Id;

                return meter;

            }
            catch (Exception)
            {

                throw;
            }

        }
        #endregion

        public UserVoteModel AddVote(UserVoteModel vote)
        {
            if (vote == null)
                throw new ArgumentNullException(Constants.OBJECT_NULL_MESSAGE);

            try
            {
                User_Votes dbVote = new User_Votes();

                dbVote.IpAddress = vote.IpAddress;
                dbVote.Poll_Option_Id = vote.Poll_Option_Id;
                dbVote.Created_By = vote.Created_By;
                dbVote.Created_Date = DateTime.Now;
               
               
                _uow.CommonRepository.AddUserVote(dbVote);
                _uow.Save();

                vote.User_Vote_Id = dbVote.User_Vote_Id;

                return vote;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        
        }

        public List<VoteStatsModel> GetPollingResults(long pollID)
        {
           List<PollOption> userVotes =  _uow.CommonRepository.GetPollingResult(pollID);

           
      //     List<VoteStatsModel> voteStats =  userVotes.GroupBy(x => x.Option_En).Select(grp => new VoteStatsModel() { PollOption = grp.Key , Count = grp.Count() , Percent = ( (grp.Count() * 100 )/userVotes.Count)  }).ToList();

           List<VoteStatsModel> voteStats = new List<VoteStatsModel>();
           long totalVote = userVotes.Sum(x => x.User_Votes.Count);
           if (totalVote ==0)
           {
               totalVote = 1;
           }
           foreach (var item in userVotes)
           {
               VoteStatsModel obj = new VoteStatsModel();

               obj.PollOptionEn = item.Option_En;
               obj.PollOptionAr = item.Option_Ar;
               obj.Count = item.User_Votes.Count;
               obj.Percent = (int)((item.User_Votes.Count * 100) / totalVote);
               obj.Poll = item.Poll != null ? item.Poll.Poll_En : "";

               voteStats.Add(obj);
               
           }


           return voteStats;

        }

        public List<HappinessStatsModel> GetHappinessStats()
        {
            var result = _uow.CommonRepository.GetHappinessMeters();

            List<HappinessStatsModel> happinessStats = new List<HappinessStatsModel>();

            if (result.Count > 0)
            {
               happinessStats = result.GroupBy(x => x.Rating).Select(grp => new HappinessStatsModel() {Option = grp.Key , Count = grp.Count() , Percent = ( (grp.Count() * 100 )/result.Count)  }).ToList();
            }

           return happinessStats;
        
        }

        
    }

    public static class UtilityBH
    {
        public static long GetFileTypeID(string extension)
        {
            long fileTypeID = -1;

            if (extension.Contains("doc"))
            {
                fileTypeID = (long)SZHPCMS.Common.FileTypes.Word;
            }
            else if (extension.Contains("pdf"))
            {
                fileTypeID = (long)SZHPCMS.Common.FileTypes.Pdf;
            }

            else if (extension.Contains("sheet") || extension.Contains("xls"))
            {
                fileTypeID = (long)SZHPCMS.Common.FileTypes.Excel;
            }
            else if (extension.Contains("ppt"))
            {
                fileTypeID = (long)SZHPCMS.Common.FileTypes.Excel;
            }
            else if (extension.Contains("jpg") || extension.Contains("png") || extension.Contains("gif"))
            {
                fileTypeID = (long)SZHPCMS.Common.FileTypes.Picture;
            }

            return fileTypeID;
        }

    }
}
