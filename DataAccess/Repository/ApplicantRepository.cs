using DataAccess.CommonRespository;
using DataAccess.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ApplicantRepository : GenericRepository<Applicant_Category>, IApplicantRepository
    {
        public ApplicantRepository(ITJCMSEntities context)
            : base(context)
        {

        }

        public void AddApplicantContentCategory(Applicant_Content_Category contentCategory)
        {
            _context.Applicant_Content_Category.Add(contentCategory);
        }

        public List<Applicant_Content_Category> GetActiveContentCategory(bool isAdmin = true)
        {
            return isAdmin ? _context.Applicant_Content_Category.Where(x => x.Row_Status_Id != (long?)(SZHPCMS.Common.RowStatus.Delete)).ToList() : _context.Applicant_Content_Category.Where(x => x.Row_Status_Id == (long?)(SZHPCMS.Common.RowStatus.Active)).ToList();
        }

        public List<Applicant_Content_Sub_Category> GetActiveContentSubCategory(bool isAdmin = true)
        {
            return isAdmin ? _context.Applicant_Content_Sub_Category.Where(x => x.Row_Status_Id != (long?)(SZHPCMS.Common.RowStatus.Delete)).ToList() : _context.Applicant_Content_Sub_Category.Where(x => x.Row_Status_Id == (long?)(SZHPCMS.Common.RowStatus.Active)).ToList(); 
        }

        public Applicant_Content_Category GetContentCategoryByID(long id)
        {
            return _context.Applicant_Content_Category.Find(id);
        }

        public void AddApplicantContentSubCategory(Applicant_Content_Sub_Category subCategory)
        {
            _context.Applicant_Content_Sub_Category.Add(subCategory);
        }

        //public List<Applicant_Content_Sub_Category> GetActiveContentSubCategory(bool isAdmin = true)
        //{
        //    return isAdmin ? _context.Applicant_Content_Sub_Category.Where(x => x.Row_Status_Id != (long?)(SZHPCMS.Common.RowStatus.Delete)).ToList() : _context.Applicant_Content_Sub_Category.Where(x => x.Row_Status_Id == (long?)(SZHPCMS.Common.RowStatus.Active)).ToList();
        //}

        public Applicant_Content_Sub_Category GetContentSubCategoryByID(long id)
        {
            return _context.Applicant_Content_Sub_Category.Find(id);
        }

        public void AddCityDescription(City_Description cityDescription)
        {
            _context.City_Description.Add(cityDescription);
        }

        public List<City_Description> GetActiveCityDescription(bool isAdmin = true)
        {
            return isAdmin ? _context.City_Description.Where(x => x.Row_Status_Id != (long?)(SZHPCMS.Common.RowStatus.Delete)).ToList() : _context.City_Description.Where(x => x.Row_Status_Id == (long?)(SZHPCMS.Common.RowStatus.Active)).ToList();
        }

        public City_Description GetCityDescriptionByID(long id)
        {
            return _context.City_Description.Find(id);
        }

        public List<Applicant_Category> GetApplicantSubCategories(bool isAdmin = true)
        {
            return isAdmin ? _dbSet.Where(x => x.Row_Status_Id != (long?)SZHPCMS.Common.RowStatus.Delete && x.Parent_Id != null).ToList() : _dbSet.Where(x => x.Row_Status_Id != (long?)SZHPCMS.Common.RowStatus.Delete && x.Parent_Id != null).ToList();
        }

        public void AddContentCategoryRange(List<Applicant_Content_Category> contentCategories)
        {
            _context.Applicant_Content_Category.AddRange(contentCategories);
        }


        public void AddContentSubCategoryRange(List<Applicant_Content_Sub_Category> contentSubCategories)
        {
            _context.Applicant_Content_Sub_Category.AddRange(contentSubCategories);
        }

        public List<City> GetActiveCities(bool isAdmin = true)
        {
            return isAdmin ? _context.Cities.Where(x => x.Row_Status_Id != (long)SZHPCMS.Common.RowStatus.Delete).ToList() : _context.Cities.Where(x => x.Row_Status_Id != (long)SZHPCMS.Common.RowStatus.Active).ToList();
        }

        public List<Applicant_Content_Category> GetContentCategoriesByApplicantSubCategoryID(long applicantSubCategory, bool isAdmin = true)
        {
            return isAdmin ? _context.Applicant_Content_Category.Where(x => x.Row_Status_Id != (long?)(SZHPCMS.Common.RowStatus.Delete) && x.Applicant_Category == applicantSubCategory).ToList() : _context.Applicant_Content_Category.Where(x => x.Row_Status_Id == (long?)(SZHPCMS.Common.RowStatus.Active) && x.Applicant_Category == applicantSubCategory).ToList();
        }

        public List<City_Description> GetCityDescriptionsByApplicantSubCategoryID(long applicantSubCategory, bool isAdmin = true)
        {
            return isAdmin ? _context.City_Description.Where(x => x.Row_Status_Id != (long?)(SZHPCMS.Common.RowStatus.Delete) && x.Applicant_Category_Id == applicantSubCategory).ToList() : _context.City_Description.Where(x => x.Row_Status_Id == (long?)(SZHPCMS.Common.RowStatus.Active) && x.Applicant_Category_Id == applicantSubCategory).ToList();
        }

        public City_Description GetCityDescription(long applicantCategoryID, long cityID)
        {
            return _context.City_Description.Where(x => x.City_Id == cityID && x.Applicant_Category_Id == applicantCategoryID && x.Row_Status_Id == (long)SZHPCMS.Common.RowStatus.Active).FirstOrDefault();
        }
    }
}
