using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLogic.BusinessHandler;
using DataAccess.CommonRespository;
using DataContract.Implementation;
using System.Collections.Generic;
using DataAccess.Database;

namespace UniteTest
{
    [TestClass]
    public class NewsBLTest
    {
        [TestMethod]
        public void GetActiveNews()
        {
            NewsBH newsBL = new NewsBH(new UnitOfWork());

           var news =  newsBL.GetActiveNews();

           Assert.IsTrue(news.Count > 0, "Collection cannot be empty");


        }

        [TestMethod]
        public void AddFeedBack()
        {



            Feed_BackModel ob = new Feed_BackModel();

            IUnitOfWork uow = new UnitOfWork();



            Feed_BackBH bl = new Feed_BackBH(uow);

            ob.Name = "ali";
            ob.Email = "saad_pucit24@yahoo.com";
            ob.Section = "events";
            ob.Comments = "comments";
            ob.Row_Status_Id = 2;

            bl.Add(ob);
            

            
            
           
        
        }

        [TestMethod]
        public void GetMenu()
        {

          //  DataAccess.Repository.PageRepository rep = new DataAccess.Repository.PageRepository(new DataAccess.Database.ITJCMSEntities());

            //List<Page> pages = rep.GetMenus("main");


            PageBH pageBL = new PageBH(new UnitOfWork());

            pageBL.GetMenu("main");


         var list =    new HomeSettingBH(new UnitOfWork()).GetPartnerServices("web");

         Assert.IsTrue(list.Count > 0);


        }

        [TestMethod]
        public void GetVotingStats()
        {
            CommonBH cBL = new CommonBH(new UnitOfWork());

            cBL.GetPollingResults(1);

        
        }

        [TestMethod]
        public void GetPages()
        {
            PageBH cBL = new PageBH(new UnitOfWork());

         var collection =    cBL.GetActivePages(0,true);


        }


    }
}
