using DataAccess.Database;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.CommonRespository
{
    public interface IUnitOfWork
    {

        /// <summary>
        /// Saves this instance
        /// </summary>
        int Save();

        /// <summary>
        /// DB context
        /// </summary>
        /// <returns></returns>
        ITJCMSEntities DataContext();

        /// <summary>
        /// Begins transaction
        /// </summary>
        /// <returns></returns>
        void BeginTransaction();

        /// <summary>
        /// Commits transaction
        /// </summary>
        void CommitTransaction();

        /// <summary>
        /// Rolls back transaction
        /// </summary>
        void RollBackTransaction();

        /// <summary>
        /// Get Menu Repository Instance
        /// </summary>
        IMenuRepository MenuRepository { get; }

        /// <summary>
        /// Row status repository
        /// </summary>
        ICommonRepository CommonRepository { get; }

        /// <summary>
        /// Get setting repository
        /// </summary>
        ISettingRepository SettingRepository { get; }

        /// <summary>
        /// Get news repoitory
        /// </summary>
        INewsRepository NewsRepository { get; }

        /// <summary>
        /// Get document repository
        /// </summary>
        IDocumentRepository DocumentRepsitory { get; }

        /// <summary>
        /// Get FAQ repository
        /// </summary>
        IFAQRepository FAQRepository { get; }

        /// <summary>
        /// Get page repository
        /// </summary>
        IPageRepository PageRepository { get; }

        /// <summary>
        /// Get event repository
        /// </summary>
        IEventRepository EventRepository { get; }

        /// <summary>
        /// Get service repository
        /// </summary>
        IEServiceRepository EServiceRepository { get; }

        /// <summary>
        /// Get trianing repository
        /// </summary>
        ITrainingPlanRepositorycs TraningPlanRepository { get; }

        /// <summary>
        /// Get home setting repository.
        /// </summary>
        IHomeSettingRepository HomeSettingRepository { get; }

        /// <summary>
        /// Get banner repository
        /// </summary>
        IBannerRepository BannerRepository { get; }

        /// <summary>
        /// Get research study repository.
        /// </summary>
        IResearchStudyRepository ResearchStudyRepository { get; }

        /// <summary>
        /// Get subscription repository.
        /// </summary>
        ISubscriptionRepository SubscriptionRepository { get; }

        /// <summary>
        /// Get gallery repository.
        /// </summary>
        IGalleryRepository GalleryRepository { get; }

        /// <summary>
        /// Get link repository.
        /// </summary>
        ILinkRepository LinkRepository { get; }

        /// <summary>
        /// Get canned email repository
        /// </summary>
        ICannedEmailRepository CannedEmailRepository { get; }

        /// <summary>
        /// Get career repository.
        /// </summary>
        ICareerRepository CareerRepository { get; }

        /// <summary>
        /// Get contact us repository
        /// </summary>
        IContactRepository ContactUsRepository { get; }

        /// <summary>
        /// Get site map  repository
        /// </summary>
        ISiteMapRepository SiteMapRepository { get; }

        /// <summary>
        /// Get archive repository
        /// </summary>
        IArchiveRepository ArchiveRepository { get; }

        /// <summary>
        /// Get project repository
        /// </summary>
        IProjectRepository ProjectRepository { get; }

         /// <summary>
        /// Get Feedback repository
        /// </summary>
        IFeed_BackRepository Feed_BackRepository { get; }

        /// <summary>
        /// Get Social Media Respository
        /// </summary>
        ISocialMediaRepository SocialRepository { get; }

        /// <summary>
        /// Get applicant repository 
        /// </summary>
        IApplicantRepository ApplicantRepository { get; }
    }
    
}
