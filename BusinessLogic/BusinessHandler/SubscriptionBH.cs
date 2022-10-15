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
    public class SubscriptionBH
    {
        private readonly IUnitOfWork _uow;
        public SubscriptionBH(IUnitOfWork uow)
        {
            _uow = uow;
        }

        /// <summary>
        /// Get subscribers
        /// </summary>
        /// <returns></returns>
        public List<SubscriberModel> GetActiveSubscriber()
        {
            return _uow.SubscriptionRepository.GetActiveSubscribers().Select(x => new SubscriberModel()
            {
                SubscriberId = x.Subscriber_Id,
                NameAr = x.Name_Ar,
                NameEn = x.Name_En,
                Email = x.Email,
                SubscriptionType = x.Subscription_Type != null ? x.Subscription_Type.Name_En : string.Empty,
                RowStatus = Enum.GetName(typeof(RowStatus), x.Row_Status_Id)

            }).ToList();
        }

        public bool Add(SubscriberModel model , out string errorMessage)
        {
            if (model == null)
                throw new Exception(SZHPCMS.Common.Constants.OBJECT_NULL_MESSAGE);

            try
            {
                 var objSubscriber = _uow.SubscriptionRepository.GetAll().Where(x => x.Email == model.Email).SingleOrDefault();

                 if (objSubscriber != null)
                 {

                     errorMessage = "already subscribed";

                     return false;
                 }


                Subscriber dbSubscriber = new Subscriber();

                dbSubscriber.Email = model.Email;
                dbSubscriber.Name_Ar = model.NameAr;
                dbSubscriber.Name_En = model.NameEn;
                dbSubscriber.Created_By = model.CreatedBy;
                dbSubscriber.Created_Date = DateTime.Now;
                dbSubscriber.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Active;

                _uow.SubscriptionRepository.Add(dbSubscriber);

                _uow.Save();
                model.SubscriberId = dbSubscriber.Subscriber_Id;


                errorMessage = string.Empty;
                return true;
                
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;

                return false;
            }
            
        }

        /// <summary>
        /// Delete subscriber object from data base
        /// </summary>
        /// <param name="menu">Subscriber object to delete</param>
        /// <returns></returns>
        public int Delete(int id)
        {
            Subscriber dbObj = _uow.SubscriptionRepository.GetByID(id);
            dbObj.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Delete;

            return _uow.Save();
        }

        /// <summary>
        /// Update menus status .
        /// </summary>
        /// <param name="menus"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int UpdateRowStatus(IEnumerable<long> idList, RowStatus status)
        {
            foreach (var id in idList)
            {
                Subscriber dbObj = _uow.SubscriptionRepository.GetByID(id);
                dbObj.Row_Status_Id = (long?)status;
            }

            return _uow.Save();
        }
    }
}
