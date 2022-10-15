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
    public class Feed_BackBH
    {
        private readonly IUnitOfWork _uow;

        public Feed_BackBH(IUnitOfWork uow)
        {
            _uow = uow;
        }


        /// <summary>
        /// Add new Feed Back obj into db.
        /// </summary>
        /// <param name="news">Feed Back object to add</param>
        /// <returns></returns>
        public Feed_BackModel Add(Feed_BackModel fb)
        {
            try
            {
                Feed_Back objfb = new Feed_Back()
                {
                    Comments = fb.Comments,
                    Email = fb.Email,
                    Name = fb.Name,
                    Page_Id = fb.Page_Id,
                    Row_Status_Id = (long?)RowStatus.Active,
                    Created_Date = DateTime.Now,
                    Section = fb.Section,
                    Created_By = fb.Created_By,
                    IPAddress = fb.IPAddress

                };

                _uow.Feed_BackRepository.Add(objfb);
                _uow.Save();

                fb.Feed_Back_Id = objfb.Feed_Back_Id;

                return fb;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Get feed back by pageID
        /// </summary>
        /// <param name="pageID"></param>
        /// <returns></returns>
        public List<Feed_BackModel> GetByPageId(long? pageID)
        {
            return _uow.Feed_BackRepository.GetAll().Where(x => x.Page_Id == pageID).Select(x => new Feed_BackModel(){
            
                    Name = x.Name,
                    Comments = x.Comments,
                    Email = x.Email,
                    Created_Date = x.Created_Date,
                    IPAddress = x.IPAddress,
                    Created_By = (long)x.Created_By,
                    Section = x.Section
                
            
            }).ToList();
        }

        /// <summary>
        /// Get feedback by page name
        /// </summary>
        /// <param name="pageName"></param>
        /// <returns></returns>
        public List<Feed_BackModel> GetByPageName(string pageName)
        {
            return _uow.Feed_BackRepository.GetAll().Where(x => x.Section == pageName).Select(x => new Feed_BackModel()
            {

                Name = x.Name,
                Comments = x.Comments,
                Email = x.Email,
                Created_Date = x.Created_Date,
                IPAddress = x.IPAddress,
                Created_By = (long)x.Created_By,
                Section = x.Section


            }).ToList();
        }
    }
}
