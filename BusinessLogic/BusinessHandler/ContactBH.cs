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
    public class ContactBH
    {
        private readonly IUnitOfWork _uow;
        public ContactBH(IUnitOfWork uow)
        {
            _uow = uow;
        }

        #region Contact Us Region
        /// <summary>
        /// Get contact us list .
        /// </summary>
        /// <returns></returns>
        public List<ContactUsModel> GetActiveContactUs()
        {
            return _uow.ContactUsRepository.GetActiveContactUs().Select(x => new ContactUsModel()
            {

                ContactUsId = x.Contact_Us_Id,
                FullName = x.Full_Name,
                Email = x.Email,
                MobilePhoneNumber = x.Mobile_Phone_Number,
                Subject = x.Subject,
                Inquiry = x.Inquiry,
                RowStatus = Enum.GetName(typeof(SZHPCMS.Common.RowStatus), x.Row_Status_Id),
                CreatedDate = x.Created_Date

            }).ToList();



        }

        /// <summary>
        /// Get contact us record by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ContactUsModel GetContactUsById(long id)
        {
            var dbContactUs = _uow.ContactUsRepository.GetByID(id);

            if (dbContactUs == null)
                throw new Exception(SZHPCMS.Common.Constants.OBJECT_NOT_FOUND_MESSAGE + id.ToString());

            return new ContactUsModel()
              {
                  ContactUsId = dbContactUs.Contact_Us_Id,
                  FullName = dbContactUs.Full_Name,
                  Email = dbContactUs.Email,
                  MobilePhoneNumber = dbContactUs.Mobile_Phone_Number,
                  Inquiry = dbContactUs.Inquiry,
                  Subject = dbContactUs.Subject,
                  RowStatus = Enum.GetName(typeof(SZHPCMS.Common.RowStatus), dbContactUs.Row_Status_Id),
                  CreatedDate = dbContactUs.Created_Date
              };
        }

        public void AddContactUs(ContactUsModel model)
        {
            if (model == null)
                throw new ArgumentNullException(SZHPCMS.Common.Constants.OBJECT_NULL_MESSAGE);

            Contact_Us contactUs = new Contact_Us();

            contactUs.Full_Name = model.FullName;
            contactUs.Email = model.Email;
            contactUs.Subject = model.Subject;
            contactUs.Inquiry = model.Inquiry;
            contactUs.Created_Date = DateTime.Now;
            contactUs.Created_By = model.CreatedBy;
            contactUs.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Active;

            try
            {
                _uow.ContactUsRepository.Add(contactUs);

                _uow.Save();

                model.ContactUsId = contactUs.Contact_Us_Id;
               
            }
            catch (Exception)
            {
                
                throw;
            }


        }

        #endregion

        #region Contact Chairman Region
        /// <summary>
        /// Get active contact chairman 
        /// </summary>
        /// <returns></returns>
        public List<ContactChairmanModel> GetActiveContactChairman()
        {
            return _uow.ContactUsRepository.GetActiveContactChairman().Select(x => new ContactChairmanModel()
            {

                ContactChairmanId = x.Contact_Chairman_Id,
                SenderName = x.Sender_Name,
                SenderEmail = x.Sender_Email,
                EmailSubject = x.Email_Subject,
                EmailBody = x.Email_Subject,
                CreatedDate = x.Created_Date,


            }).ToList();
        }

        /// <summary>
        /// Get contact chairman by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ContactChairmanModel GetContactChairmanById(long id)
        {
            var dbContactChaiman = _uow.ContactUsRepository.GetContactChairmanByID(id);

            return new ContactChairmanModel()
            {
                ContactChairmanId = dbContactChaiman.Contact_Chairman_Id,
                SenderName = dbContactChaiman.Sender_Name,
                SenderEmail = dbContactChaiman.Sender_Email,
                EmailBody = dbContactChaiman.Email_Body,
                EmailSubject = dbContactChaiman.Email_Subject,
                CreatedDate = dbContactChaiman.Created_Date
            };


        }

        /// <summary>
        /// Add contact to chairman info into table
        /// </summary>
        /// <returns></returns>
        public ContactChairmanModel AddChairmanContact(ContactChairmanModel modelContactChairman)
        {
            if (modelContactChairman == null)
                throw new ArgumentNullException(Constants.OBJECT_NULL_MESSAGE);

            Contact_Chairman dbContactChairman = new Contact_Chairman();

            dbContactChairman.Sender_Email = modelContactChairman.SenderEmail;
            dbContactChairman.Sender_Name = modelContactChairman.SenderName;
            dbContactChairman.Email_Subject = modelContactChairman.EmailSubject;
            dbContactChairman.Email_Body = modelContactChairman.EmailBody;
            dbContactChairman.Created_Date = DateTime.Now;
            dbContactChairman.Created_By = modelContactChairman.CreatedBy;
            dbContactChairman.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Active;

            _uow.ContactUsRepository.AddContactChairman(dbContactChairman);

            try
            {
                _uow.Save();
            }
            catch (Exception)
            {                
                throw;
            }

            return modelContactChairman;
        }


        #endregion

        #region Contact Technical Support Request
        /// <summary>
        /// Get active support requests.
        /// </summary>
        /// <returns></returns>
        public List<TechnicalSupportModel> GetActiveSupportRequests()
        {


            var supportRequests = _uow.ContactUsRepository.GetActiveSupportRequests().Select(x => new TechnicalSupportModel()
            {

                TechnicalSupportId = x.Technical_Support_Id,
                PersonName = x.Person_Name,
                PersonEmailId = x.Person_Email_Id,
                PersonMobileNo = x.Person_Mobile_No,
                ReferenceNo = x.Reference_No,
                Summary = x.Summary,
                Description = x.Description,
                UserType = x.User_Type != null ? x.User_Type.Name_En : string.Empty,
                Category = x.Technical_Support_Category != null ? x.Technical_Support_Category.Name_En : string.Empty,
                CreatedDate = x.Created_Date,
                DocumentName = x.Document != null ? x.Document.File_Name : string.Empty



            }).ToList();

            return supportRequests;

        }

        /// <summary>
        /// Get support request by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TechnicalSupportModel GetSupportRequest(long id)
        {
            var dbSupport = _uow.ContactUsRepository.GetSupportRequest(id);

            return new TechnicalSupportModel()
            {

                TechnicalSupportId = dbSupport.Technical_Support_Id,
                PersonName = dbSupport.Person_Name,
                PersonEmailId = dbSupport.Person_Email_Id,
                PersonMobileNo = dbSupport.Person_Mobile_No,
                ReferenceNo = dbSupport.Reference_No,
                Summary = dbSupport.Summary,
                Description = dbSupport.Description,
                UserType = dbSupport.User_Type != null ? dbSupport.User_Type.Name_En : string.Empty,
                Category = dbSupport.Technical_Support_Category != null ? dbSupport.Technical_Support_Category.Name_En : string.Empty,
                CreatedDate = dbSupport.Created_Date,
                DocumentName = dbSupport.Document != null ? dbSupport.Document.File_Name : string.Empty

            };
        }
        #endregion

        #region Contact Region

        /// <summary>
        /// Get active contacts.
        /// </summary>
        /// <returns></returns>
        public List<ContactModel> GetActiveContacts(string dataFor = "")
        {


            var contacts = _uow.ContactUsRepository.GetActiveContacts(dataFor).Select(x => new ContactModel()
            {

                ContactId = x.Contact_Id,
                LocationEn = x.Location_En,
                LocationAr = x.Location_Ar,
                CityAr = x.City_Ar,
                CityEn = x.City_En,
                Email = x.Email,
                Fax = x.Fax,
                Phone = x.Phone,
                POBox = x.PO_Box,
                WorkingHoursEn = x.Working_Hours_En,
                WorkingHoursAr = x.Working_Hours_Ar,
                CreatedDate = x.Created_Date,
                Latitude = x.Latitude,
                Longitude = x.Longitude,
                DisplayOrder = x.DisplayOrder,
                RowStatus = Enum.GetName(typeof(RowStatus), x.Row_Status_Id)

            }).ToList();

            return contacts;

        }

        public List<ContactModel> GetSortedContacts(string dataFor = "")
        {
            return GetActiveContacts(dataFor).OrderBy(x => x.DisplayOrder).ToList();
        }


        /// <summary>
        /// Get contact by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ContactModel GetContactById(long id)
        {
            var dbContact = _uow.ContactUsRepository.GetContactByID(id);

            return new ContactModel()
            {

                ContactId = dbContact.Contact_Id,
                CityAr = dbContact.City_Ar,
                CityEn = dbContact.City_En,
                Email = dbContact.Email,
                Fax = dbContact.Fax,
                Phone = dbContact.Phone,
                POBox = dbContact.PO_Box,
                WorkingHoursEn = dbContact.Working_Hours_En,
                WorkingHoursAr = dbContact.Working_Hours_Ar,
                CreatedDate = dbContact.Created_Date,
                Latitude = dbContact.Latitude,
                Longitude = dbContact.Longitude,
                DisplayOrder = dbContact.DisplayOrder,
                RowStatus = Enum.GetName(typeof(SZHPCMS.Common.RowStatus), dbContact.Row_Status_Id)

            };
        }

        /// <summary>
        /// Add new contact obj into db.
        /// </summary>
        /// <param name="news">Contact object to add</param>
        /// <returns></returns>
        public ContactModel AddContact(ContactModel model)
        {
            if (model == null)
                throw new ArgumentNullException(Constants.OBJECT_NULL_MESSAGE);

            try
            {
                Contact dbContact = new Contact();

                dbContact.Contact_Id = model.ContactId;
                dbContact.City_Ar = model.CityAr;
                dbContact.City_En = model.CityEn;
                dbContact.Email = model.Email;
                dbContact.Fax = model.Fax;
                dbContact.Phone = model.Phone;
                dbContact.PO_Box = model.POBox;
                dbContact.Working_Hours_En = model.WorkingHoursEn;
                dbContact.Working_Hours_Ar = model.WorkingHoursAr;
                dbContact.Latitude = model.Latitude;
                dbContact.Longitude = model.Longitude;
                dbContact.DisplayOrder = model.DisplayOrder;

                dbContact.Created_By = model.CreatedBy;
                dbContact.Row_Status_Id = (long?)RowStatus.Active;
                dbContact.Created_Date = DateTime.Now;

                _uow.ContactUsRepository.AddContact(dbContact);
                _uow.Save();

                model.ContactId = dbContact.Contact_Id;

                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Update contact in data base
        /// </summary>
        /// <param name="news">contact object to update</param>
        /// <returns>Number of rows effected</returns>
        public int UpdateContact(ContactModel model)
        {
            if (model == null)
                throw new ArgumentNullException(Constants.OBJECT_NULL_MESSAGE);

            try
            {
                Contact dbContact = _uow.ContactUsRepository.GetContactByID(model.ContactId);

                if (dbContact == null)
                    throw new Exception(Constants.OBJECT_NOT_FOUND_MESSAGE + model.ContactId);

                dbContact.City_Ar = model.CityAr;
                dbContact.City_En = model.CityEn;
                dbContact.Email = model.Email;
                dbContact.Fax = model.Fax;
                dbContact.Phone = model.Phone;
                dbContact.PO_Box = model.POBox;
                dbContact.Working_Hours_En = model.WorkingHoursEn;
                dbContact.Working_Hours_Ar = model.WorkingHoursAr;
                dbContact.Latitude = model.Latitude;
                dbContact.Longitude = model.Longitude;
                dbContact.DisplayOrder = model.DisplayOrder;

                dbContact.Updated_By = model.UpdatedBy;
                dbContact.Updated_Date = DateTime.Now;

                return _uow.Save(); ;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Delete gallary object from data base . It should be soft delete i.e just update row status to delete
        /// </summary>
        /// <param name="menu">Gallary object to delete</param>
        /// <returns></returns>
        public int DeleteContact(int id)
        {
            try
            {
                Contact dbContact = _uow.ContactUsRepository.GetContactByID(id);

                if (dbContact == null)
                    throw new Exception(Constants.OBJECT_NOT_FOUND_MESSAGE + id as string);

                dbContact.Row_Status_Id = (long?)SZHPCMS.Common.RowStatus.Delete;

                return _uow.Save();

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        /// <summary>
        /// Update Galleries status .
        /// </summary>
        /// <param name="menus"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int UpdateRowStatusContact(IEnumerable<long> idList, RowStatus status)
        {
            try
            {

                foreach (var id in idList)
                {
                    Contact dbContact = _uow.ContactUsRepository.GetContactByID(id);

                    dbContact.Row_Status_Id = (long?)status;

                }

                return _uow.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion

    }
}
