using SZHPCMS.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using SZHPCMS.Models;
using SZHPCMS.Utilities;


namespace SZHPCMS
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            if (Application[Constants.SESSION_CMS_TITLE] == null)
            {

                var modelSetting = new BusinessLogic.BusinessHandler.SettingBH().GetSiteSetting();

                if (modelSetting != null)
                {
                    string cmsTitle = modelSetting.CMSTitleEnglish;
                    Application.Add(Constants.SESSION_CMS_TITLE, cmsTitle);
                }
            }
        }

        void Session_Start(object sender, EventArgs e)
        {
            if (User.Identity.IsAuthenticated && Session[SZHPCMS.Common.Constants.LOGGED_IN_USER_ID] == null)
            {               
                long id = Utility.GetUserID(User.Identity.Name);

                Session.Add(Constants.LOGGED_IN_USER_ID, id);
            }

        }

        protected void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {


        }

        //protected void Application_AcquireRequestState(object sender, EventArgs e)
        //{
        //    //It's important to check whether session object is ready
        //    if (HttpContext.Current.Session != null)
        //    {
        //        CultureInfo ci = (CultureInfo)this.Session["Culture"];
        //        //Checking first if there is no value in session 
        //        //and set default language 
        //        //this can happen for first user's request
        //        if (ci == null)
        //        {
        //            //Sets default culture to english invariant
        //            string langName = "en";

        //            Session.Add(Common.Constants.SELECTED_LANGUAGE, langName);

        //            //Try to get values from Accept lang HTTP header
        //            if (HttpContext.Current.Request.UserLanguages != null && HttpContext.Current.Request.UserLanguages.Length != 0)
        //            {
        //                //Gets accepted list 
        //                langName = HttpContext.Current.Request.UserLanguages[0].Substring(0, 2);
        //            }
        //            ci = new CultureInfo(langName);
        //            this.Session["Culture"] = ci;
        //        }
        //        //Finally setting culture for each request


        //        Thread.CurrentThread.CurrentUICulture = ci;
        //        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ci.Name);
        //    }
        //}
    }
}
