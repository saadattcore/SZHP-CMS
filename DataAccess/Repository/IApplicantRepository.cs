using DataAccess.CommonRespository;
using DataAccess.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IApplicantRepository : IGenericRepository<Applicant_Category>
    {
        /// <summary>
        /// Add Applicant Content Category
        /// </summary>
        /// <param name="contentCategory"></param>
        void AddApplicantContentCategory(Applicant_Content_Category contentCategory);

        /// <summary>
        /// Get Active Content Category List
        /// </summary>
        /// <param name="isAdmin"></param>
        /// <returns></returns>
        List<Applicant_Content_Category> GetActiveContentCategory(bool isAdmin = true);


        /// <summary>
        /// Get Active Content Sub Categories.
        /// </summary>
        /// <param name="isAdmin"></param>
        /// <returns></returns>
        //List<Applicant_Content_Sub_Category> GetActiveContentSubCategory(bool isAdmin = true);


        /// <summary>
        /// Get Content Category By ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Applicant_Content_Category GetContentCategoryByID(long id);

        /// <summary>
        /// Add Applicant Content Category
        /// </summary>
        /// <param name="contentCategory"></param>
        void AddApplicantContentSubCategory(Applicant_Content_Sub_Category contentCategory);

        /// <summary>
        /// Get Active Content Sub Category
        /// </summary>
        /// <param name="isAdmin"></param>
        /// <returns></returns>
        List<Applicant_Content_Sub_Category> GetActiveContentSubCategory(bool isAdmin = true);

        /// <summary>
        /// Get Content Sub Category By ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Applicant_Content_Sub_Category GetContentSubCategoryByID(long id);

        /// <summary>
        /// Add City Description
        /// </summary>
        /// <param name="cityDescription"></param>
        void AddCityDescription(City_Description cityDescription);

        /// <summary>
        /// Get Active Description
        /// </summary>
        /// <param name="isAdmin"></param>
        /// <returns></returns>
        List<City_Description> GetActiveCityDescription(bool isAdmin = true);

        /// <summary>
        /// Get City Description By ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        City_Description GetCityDescriptionByID(long id);

        /// <summary>
        /// Get Active Cities
        /// </summary>
        /// <param name="isAdmin"></param>
        /// <returns></returns>
        List<City> GetActiveCities(bool isAdmin = true);

        /// <summary>
        /// Get Applicant Sub Categories To Populate In DropDown in  Applicant Content Category Create View .
        /// </summary>
        /// <returns></returns>
        List<Applicant_Category> GetApplicantSubCategories(bool isAdmin = true);


        /// <summary>
        /// Add Content Category Range
        /// </summary>
        /// <param name="contentCategories"></param>

        void AddContentCategoryRange(List<Applicant_Content_Category> contentCategories);


        /// <summary>
        /// Add Content Sub Category Range
        /// </summary>
        /// <param name="contentSubCategories"></param>
        void AddContentSubCategoryRange(List<Applicant_Content_Sub_Category> contentSubCategories);

        /// <summary>
        /// Get Content Category By Applicant Category Id.
        /// </summary>
        /// <param name="applicantCategoryId"></param>
        /// <returns></returns>
        List<Applicant_Content_Category> GetContentCategoriesByApplicantSubCategoryID(long applicantSubCategory, bool isAdmin = true);

        /// <summary>
        /// Get City Descriptions By Applicant Sub Category Id.
        /// </summary>
        /// <param name="applicantSubCategory"></param>
        /// <returns></returns>

        List<City_Description> GetCityDescriptionsByApplicantSubCategoryID(long applicantSubCategor, bool isAdmin = true);

        /// <summary>
        /// Get City Description by applicant category id and city id
        /// </summary>
        /// <param name="applicantCategoryID"></param>
        /// <param name="cityID"></param>
        /// <returns></returns>
        City_Description GetCityDescription(long applicantCategoryID, long cityID);

    }
}
