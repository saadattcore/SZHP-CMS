using SZHPCMS.Common;
using DataAccess.CommonRespository;
using DataAccess.Database;
using DataAccess.Repository;
using DataContract.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.BusinessHandler
{
    public class EventBH
    {
        private readonly IUnitOfWork _uow;
        public EventBH(IUnitOfWork uow)
        {
            _uow = uow;
        }

        /// <summary>
        /// Get active events i.e events which are not deleted.
        /// </summary>
        /// <returns></returns>
        public List<EventModel> GetActiveEvents(string dataFor = "")
        {

            var activeEventsList = _uow.EventRepository.GetActiveEvents(dataFor).Select(x => new EventModel()
            {

                Id = x.Event_Id,
                EventTypeName = x.Event_Type != null ? x.Event_Type.Name_En : "",
                TitleEnglish = x.Title_En,
                TitleArabic = x.Title_Ar,
                DescriptionEn = x.Description_En,
                DescriptionAr = x.Description_Ar,
                LocationEn = x.Description_En,
                LocationAr = x.Description_Ar,
                RowStatusId = (long)x.Row_Status_Id,
                StartDate = (DateTime)x.StartDate,
                EndDate = (DateTime)x.EndDate,
                RowStatus = Enum.GetName(typeof(SZHPCMS.Common.RowStatus), x.Row_Status_Id),
                CreatedDate = x.Created_Date,
                FileName = x.Event_Document != null ? (x.Event_Document.Count > 0 ? x.Event_Document.ToList()[0].Document.File_Name : string.Empty) : string.Empty


            }).ToList();

            foreach (var item in activeEventsList)
            {
                item.RowStatus = Enum.GetName(typeof(SZHPCMS.Common.RowStatus), item.RowStatusId);
            }

            return activeEventsList;

        }

        /// <summary>
        /// Get active event types i.e event types which are not deleted.
        /// </summary>
        /// <returns></returns>
        public List<EventTypeModel> GetActiveEventTypes()
        {
            return _uow.EventRepository.GetActiveEventTypes().Select(x => new EventTypeModel()
            {

                EventTypeID = x.Event_Type_Id,
                NameEn = x.Name_En,
                NameAr = x.Name_Ar

            }).ToList();
        }

        /// <summary>
        /// Get event by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public EventModel GetByID(long id)
        {
            var dbEvent = _uow.EventRepository.GetByID(id);

            if (dbEvent == null)
                throw new Exception(Constants.OBJECT_NOT_FOUND_MESSAGE + id.ToString());

            EventModel objEvent = new EventModel();

            objEvent.Id = dbEvent.Event_Id;
            objEvent.EventTypeName = dbEvent.Event_Type != null ? dbEvent.Event_Type.Name_En : "";
            objEvent.TitleEnglish = dbEvent.Title_En;
            objEvent.TitleArabic = dbEvent.Title_Ar;
            objEvent.DescriptionEn = dbEvent.Description_En;
            objEvent.DescriptionAr = dbEvent.Description_Ar;
            objEvent.LocationEn = dbEvent.Description_En;
            objEvent.LocationAr = dbEvent.Description_Ar;
            objEvent.RowStatusId = (long)dbEvent.Row_Status_Id;
            objEvent.StartDate = (DateTime)dbEvent.StartDate;
            objEvent.EndDate = (DateTime)dbEvent.EndDate;
            objEvent.EventTypeID = (long)dbEvent.Event_Type_Id;
            objEvent.RowStatus = Enum.GetName(typeof(RowStatus), dbEvent.Row_Status_Id);
            objEvent.CreatedDate = dbEvent.Created_Date;

            if (dbEvent.Event_Document.Count > 0)
            {
                objEvent.Documents = new List<DocumentModel>();

                foreach (var item in dbEvent.Event_Document)
                {
                    if (item.Document != null)
                    {
                        if (item.Document.Row_Status_Id != (long?)RowStatus.Delete)
                        {
                            objEvent.Documents.Add(new DocumentModel() { FileName = item.Document.File_Name, Extenstion = item.Document.Extenstion, DocumentId = item.Document.Document_Id });
                        }
                    }
                }

            }

            return objEvent;

        }


        /// <summary>
        /// Add new event obj into db.
        /// </summary>
        /// <param name="news">Event object to add</param>
        /// <returns></returns>
        public EventModel Add(EventModel eventModel)
        {
            try
            {
                Event dbEvent = new Event();

                dbEvent.Event_Id = eventModel.Id;
                dbEvent.Title_En = eventModel.TitleEnglish;
                dbEvent.Title_Ar = eventModel.TitleArabic;
                dbEvent.Description_En = eventModel.DescriptionEn;
                dbEvent.Description_Ar = eventModel.DescriptionAr;
                dbEvent.Row_Status_Id = (long?)eventModel.RowStatusId;
                dbEvent.StartDate = (DateTime)eventModel.StartDate;
                dbEvent.EndDate = (DateTime)eventModel.EndDate;
                dbEvent.Created_By = eventModel.CreatedBy;
                dbEvent.Event_Type_Id = (long?)eventModel.EventTypeID;
                dbEvent.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Active;
                dbEvent.Created_Date = DateTime.Now;


                if (eventModel.Documents != null)
                {
                    if (eventModel.Documents.Count > 0)
                    {
                        foreach (var item in eventModel.Documents)
                        {

                            Event_Document dbEventDoc = new Event_Document();
                            dbEventDoc.Created_By = (long?)eventModel.CreatedBy;
                            dbEventDoc.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Active;
                            dbEvent.Created_Date = DateTime.Now;

                            dbEventDoc.Document = new Document();
                            dbEventDoc.Document.Created_By = eventModel.CreatedBy;
                            dbEventDoc.Document.Extenstion = item.Extenstion;
                            dbEventDoc.Document.File_Name = item.FileName;
                            dbEventDoc.Document.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Active;
                            dbEventDoc.Document.File_Type_Id = (long?)SZHPCMS.Common.FileTypes.Picture;
                            dbEventDoc.Document.Created_Date = DateTime.Now;

                            dbEvent.Event_Document.Add(dbEventDoc);
                        }
                    }
                }

                _uow.EventRepository.Add(dbEvent);
                _uow.Save();

                eventModel.Id = dbEvent.Event_Id;

                return eventModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Update event in data base
        /// </summary>
        /// <param name="news">event object to update</param>
        /// <returns>Number of rows effected</returns>
        public int Update(EventModel eventModel)
        {
            try
            {
                Event dbEvent = _uow.EventRepository.GetByID(eventModel.Id);

                dbEvent.Event_Id = eventModel.Id;
                dbEvent.Title_En = eventModel.TitleEnglish;
                dbEvent.Title_Ar = eventModel.TitleArabic;
                dbEvent.Description_En = eventModel.DescriptionEn;
                dbEvent.Description_Ar = eventModel.DescriptionAr;
                dbEvent.StartDate = (DateTime)eventModel.StartDate;
                dbEvent.EndDate = (DateTime)eventModel.EndDate;
                dbEvent.Updated_By = eventModel.UpdatedBy;
                dbEvent.Updated_Date = eventModel.UpdatedDate;
                dbEvent.Event_Type_Id = eventModel.EventTypeID;

                if (eventModel.Documents != null)
                {
                    if (eventModel.Documents.Count > 0)
                    {
                        foreach (var item in eventModel.Documents)
                        {
                            Event_Document dbEventDoc = new Event_Document();
                            dbEventDoc.Created_By = (long?)eventModel.CreatedBy;
                            dbEventDoc.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Active;

                            dbEventDoc.Document = new Document();
                            dbEventDoc.Document.Created_By = eventModel.CreatedBy;
                            dbEventDoc.Document.Extenstion = item.Extenstion;
                            dbEventDoc.Document.File_Name = item.FileName;
                            dbEventDoc.Document.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Active;
                            dbEventDoc.Document.File_Type_Id = (long?)SZHPCMS.Common.FileTypes.Picture;

                            dbEvent.Event_Document.Add(dbEventDoc);
                        }
                    }
                }

                int rowsEffected = _uow.Save();

                return rowsEffected;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Delete event object from data base . It should be soft delete i.e just update row status to delete
        /// </summary>
        /// <param name="menu">Event object to delete</param>
        /// <returns></returns>
        public int Delete(int id)
        {
            Event dbEvent = _uow.EventRepository.GetByID(id);
            dbEvent.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Delete;

            foreach (var item in dbEvent.Event_Document)
            {
                if (item.Document != null)
                {
                    item.Document.Row_Status_Id = (long?)RowStatus.Delete;
                }
            }


            return _uow.Save();
        }

        /// <summary>
        /// Update Event's status .
        /// </summary>
        /// <param name="menus"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int UpdateRowStatus(IEnumerable<long> eventIDList, RowStatus status)
        {
            foreach (var id in eventIDList)
            {
                Event dbEvent = _uow.EventRepository.GetByID(id);

                foreach (var item in dbEvent.Event_Document)
                {
                    if (item.Document != null)
                    {
                        item.Row_Status_Id = (long?)status;
                    }
                }



                dbEvent.Row_Status_Id = (long?)status;
            }

            return _uow.Save();
        }


        /// <summary>
        /// Get upcoming events
        /// </summary>
        /// <returns></returns>
        public List<EventModel> GetUpcomingEvents()
        {
            var activeEventsList = _uow.EventRepository.GetUpcomingEvents().Select(x => new EventModel()
            {

                Id = x.Event_Id,
                EventTypeName = x.Event_Type != null ? x.Event_Type.Name_En : "",
                TitleEnglish = x.Title_En,
                TitleArabic = x.Title_Ar,
                DescriptionEn = x.Description_En,
                DescriptionAr = x.Description_Ar,
                LocationEn = x.Description_En,
                LocationAr = x.Description_Ar,
                RowStatusId = (long)x.Row_Status_Id,
                StartDate = (DateTime)x.StartDate,
                EndDate = (DateTime)x.EndDate,
                RowStatus = Enum.GetName(typeof(SZHPCMS.Common.RowStatus), x.Row_Status_Id),
                CreatedDate = x.Created_Date,
                FileName = x.Event_Document != null ? (x.Event_Document.Count > 0 ? x.Event_Document.ToList()[0].Document.File_Name : string.Empty) : string.Empty


            }).ToList();

            foreach (var item in activeEventsList)
            {
                item.RowStatus = Enum.GetName(typeof(SZHPCMS.Common.RowStatus), item.RowStatusId);
            }

            return activeEventsList;
           
        }


    }
}
