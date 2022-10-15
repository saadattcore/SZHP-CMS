using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLogic.BusinessHandler;
using DataAccess.CommonRespository;
using DataContract.Implementation;
using System.Collections.Generic;

namespace UniteTest
{
    [TestClass]
    public class PageBLTest
    {
        [TestMethod]
        public void GetMenu()
        {
            PageBH pageBL = new PageBH(new UnitOfWork());

            List<PageModel> mainMenus = pageBL.GetMenu("Main Menu");
            
            Assert.IsTrue(mainMenus.Count > 0, "Main Menus list cannot be empty");
        }

        [TestMethod]
        public void GetCityDescription()
        {
            PageBH pageBL = new PageBH(new UnitOfWork());


              ApplicantBH applicantBL  = new ApplicantBH(new UnitOfWork());

              CityDescriptionModel cityDesc = applicantBL.GetCityDescription(2, 1);

            Assert.IsNotNull(cityDesc);
        }
    }
}
