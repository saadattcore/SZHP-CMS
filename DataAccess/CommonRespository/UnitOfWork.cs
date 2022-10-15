using DataAccess.Database;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Validation;
using Db = DataAccess.Database;

namespace DataAccess.CommonRespository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        #region Generic Section
        protected readonly ITJCMSEntities _dbContext;

        /// <summary>
        /// _dbTransaction
        /// </summary>
        private DbContextTransaction _dbTransaction = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        public UnitOfWork()
        {
            _dbContext = new ITJCMSEntities();
        }

        /// <summary>
        /// Data Context.
        /// </summary>
        /// <returns></returns>
        public ITJCMSEntities DataContext()
        {
            return _dbContext ?? new ITJCMSEntities();
        }
        public int Save()
        {
            int rowsEffected = 0;

            try
            {
                rowsEffected = _dbContext.SaveChanges();
            }
            catch (OptimisticConcurrencyException)
            {
                RollBackTransaction();
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder validationErros = new StringBuilder();

                foreach (var item in ex.EntityValidationErrors)
                {
                    foreach (var error in item.ValidationErrors)
                    {
                        validationErros.Append(string.Format("Property Name ={0} Validation Error ={1}", error.PropertyName, error.ErrorMessage));
                    }
                }

                throw new Exception(validationErros.ToString());
            }

            return rowsEffected;
        }

        public void BeginTransaction()
        {
            _dbContext.Database.Connection.Open();
            _dbTransaction = _dbContext.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            if (_dbTransaction != null)
                _dbTransaction.Commit();

            if (_dbContext.Database.Connection.State == System.Data.ConnectionState.Open)
                _dbContext.Database.Connection.Close();
        }

        public void RollBackTransaction()
        {
            if (_dbTransaction != null)
                _dbTransaction.Rollback();
            if (_dbContext.Database.Connection.State == System.Data.ConnectionState.Open)
                _dbContext.Database.Connection.Close();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    if (_dbTransaction != null)
                    {
                        _dbTransaction.Dispose();
                    }
                    if (_dbContext != null)
                    {
                        _dbContext.Dispose();
                    }
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        private GenericRepository<Db.Setting> _setting;
        public GenericRepository<Db.Setting> PropSetting
        {
            get
            {
                if (_setting == null)
                {
                    _setting = new GenericRepository<Db.Setting>(_dbContext);
                }
                return _setting;
            }
        }

        private GenericRepository<Db.Document> _document;
        public GenericRepository<Db.Document> PropDocument
        {
            get
            {
                if (_document == null)
                {
                    _document = new GenericRepository<Document>(_dbContext);
                }
                return _document;
            }
        }

        private IMenuRepository _menuRepository;
        public IMenuRepository MenuRepository
        {
            get
            {
                if (_menuRepository == null)
                {
                    _menuRepository = new MenuRepository(_dbContext);
                }
                return _menuRepository;
            }
        }

        private ICommonRepository _commonRepository;
        public ICommonRepository CommonRepository
        {
            get
            {

                if (_commonRepository == null)
                {
                    _commonRepository = new CommonRepository(_dbContext);
                }

                return _commonRepository;
            }
        }

        private ISettingRepository _settingRepository;
        public ISettingRepository SettingRepository
        {
            get
            {
                if (_settingRepository == null)
                {
                    _settingRepository = new SettingRepository(_dbContext);
                }
                return _settingRepository;
            }
        }

        private INewsRepository _newsRepository;
        public INewsRepository NewsRepository
        {
            get
            {
                if (_newsRepository == null)
                {
                    _newsRepository = new NewsRepository(_dbContext);
                }

                return _newsRepository;
            }
        }

        private IDocumentRepository _documentRepository;
        public IDocumentRepository DocumentRepsitory
        {
            get
            {
                if (_documentRepository == null)
                {
                    _documentRepository = new DocumentRepository(_dbContext);
                }
                return _documentRepository;
            }
        }

        private IFAQRepository _faqRepository;
        public IFAQRepository FAQRepository
        {
            get
            {
                if (_faqRepository == null)
                {
                    _faqRepository = new FAQRepository(_dbContext);
                }
                return _faqRepository;
            }
        }

        private IPageRepository _pageRepository;
        public IPageRepository PageRepository
        {
            get
            {

                if (_pageRepository == null)
                {
                    _pageRepository = new PageRepository(_dbContext);
                }
                return _pageRepository;
            }
        }

        private IEventRepository _eventRepository;
        public IEventRepository EventRepository
        {
            get
            {
                if (_eventRepository == null)
                {
                    _eventRepository = new EventRepository(_dbContext);
                }

                return _eventRepository;
            }
        }

        private IEServiceRepository _eServiceRepository;
        public IEServiceRepository EServiceRepository
        {
            get
            {
                if (_eServiceRepository == null)
                {
                    _eServiceRepository = new EServiceRepository(_dbContext);

                }
                return _eServiceRepository;
            }

        }

        private ITrainingPlanRepositorycs _trainingPlanRepository;
        public ITrainingPlanRepositorycs TraningPlanRepository
        {
            get
            {
                if (_trainingPlanRepository == null)
                {
                    _trainingPlanRepository = new TrainingPlanRepository(_dbContext);
                }
                return _trainingPlanRepository;
            }
        }

        private IHomeSettingRepository _homeSettingRepository;
        public IHomeSettingRepository HomeSettingRepository
        {
            get
            {
                if (_homeSettingRepository == null)
                {
                    _homeSettingRepository = new HomeSettingRepository(_dbContext);

                }
                return _homeSettingRepository;
            }
        }

        private IBannerRepository _bannerRepository;
        public IBannerRepository BannerRepository
        {
            get
            {
                if (_bannerRepository == null)
                {
                    _bannerRepository = new BannerRepository(_dbContext);
                }
                return _bannerRepository;
            }
        }

        private IResearchStudyRepository _researchStudyRepsitory;
        public IResearchStudyRepository ResearchStudyRepository
        {
            get
            {

                if (_researchStudyRepsitory == null)
                {
                    _researchStudyRepsitory = new ResearchStudyRepository(_dbContext);
                }
                return _researchStudyRepsitory;
            }
        }


        private ISubscriptionRepository _subscriptionRepository;
        public ISubscriptionRepository SubscriptionRepository
        {
            get
            {

                if (_subscriptionRepository == null)
                {
                    _subscriptionRepository = new SubscriptionRepository(_dbContext);
                }
                return _subscriptionRepository;
            }
        }

        private IGalleryRepository _galleryRepository;
        public IGalleryRepository GalleryRepository
        {
            get
            {
                if (_galleryRepository == null)
                {
                    _galleryRepository = new GalleryRepository(_dbContext);
                }
                return _galleryRepository;
            }
        }

        private ILinkRepository _linkRepository;
        public ILinkRepository LinkRepository
        {
            get
            {
                if (_linkRepository == null)
                {
                    _linkRepository = new LinkRepository(_dbContext);
                }
                return _linkRepository;
            }
        }

        private ICannedEmailRepository _cannedEmailRepository;
        public ICannedEmailRepository CannedEmailRepository
        {
            get
            {
                if (_cannedEmailRepository == null)
                {
                    _cannedEmailRepository = new CannedEmailRepository(_dbContext);
                }
                return _cannedEmailRepository;
            }
        }

        private ICareerRepository _careerRepository;
        public ICareerRepository CareerRepository
        {
            get
            {
                if (_careerRepository == null)
                {
                    _careerRepository = new CareerRepository(_dbContext);
                }
                return _careerRepository;
            }

        }

        private IContactRepository _contactUsRepository;
        public IContactRepository ContactUsRepository
        {
            get
            {
                if (_contactUsRepository == null)
                {
                    _contactUsRepository = new ContactRepository(_dbContext);
                }
                return _contactUsRepository;
            }
        }


        private SiteMapRepository _siteMapRepository;
        public ISiteMapRepository SiteMapRepository
        {
            get
            {
                if (_siteMapRepository == null)
                {

                    _siteMapRepository = new SiteMapRepository(_dbContext);
                }
                return _siteMapRepository;
            }

        }


        private IArchiveRepository _archiveRepository;
        public IArchiveRepository ArchiveRepository
        {
            get
            {
                if (_archiveRepository == null)
                    _archiveRepository = new ArchiveRepository(_dbContext);
                return _archiveRepository;

            }
        }


        private IProjectRepository _projectRepository;
        public IProjectRepository ProjectRepository
        {
            get
            {

                if (_projectRepository == null)
                    _projectRepository = new ProjectRepository(_dbContext);
                return _projectRepository;
            }
        }

        private IFeed_BackRepository _Feed_BackRepository;
        public IFeed_BackRepository Feed_BackRepository
        {
            get
            {

                if (_Feed_BackRepository == null)
                    _Feed_BackRepository = new Feed_BackRepository(_dbContext);
                return _Feed_BackRepository;
            }
        }

        private ISocialMediaRepository _socialMedia;
        public ISocialMediaRepository SocialRepository
        {
            get 
            {
                if (_socialMedia == null)
                {
                    _socialMedia = new SocialMediaRepository(_dbContext);
                }
                return _socialMedia;
            }
        }

        private IApplicantRepository _applicantRepository;

        public IApplicantRepository ApplicantRepository
        {
            get
            {
                if (_applicantRepository == null)
                {
                    _applicantRepository = new ApplicantRepository(_dbContext);
                }
                return _applicantRepository;
            }
        
        }
    }
}
