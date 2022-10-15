using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using BusinessLogic.BusinessHandler;
using SZHPCMS.Common;

namespace SZHPCMS.Controllers
{
    public class BaseController : Controller
    {

        public BaseController()
        {

        }
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {           
             Response.Cache.SetNoStore();

            if (Session[SZHPCMS.Common.Constants.SELECTED_LANGUAGE] == null)
            {
                Session.Add(SZHPCMS.Common.Constants.SELECTED_LANGUAGE, "en");
            }
            else
            {
                string lang = Session[SZHPCMS.Common.Constants.SELECTED_LANGUAGE] as string;

                switch (lang)
                {
                    case "ar":
                        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("ar");
                        Thread.CurrentThread.CurrentUICulture = new CultureInfo("ar");
                        break;
                    case "en":
                        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en");
                        Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
                        break;
                    default:
                        break;
                }

            }

            return base.BeginExecuteCore(callback, state);
        }

        //private string getEnumName(Constants.Language language)
        //{
        //    foreach (System.Reflection.FieldInfo objFI in typeof(Constants.Language).GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static))
        //    {
        //        object[] objCustomAtts = objFI.GetCustomAttributes(true);
        //        if (language.ToString() == objFI.Name && objCustomAtts.Length > 0)
        //        {
        //            System.Xml.Serialization.XmlEnumAttribute xAtt = (System.Xml.Serialization.XmlEnumAttribute)objCustomAtts[0];
        //            return xAtt.Name;
        //        }
        //    }
        //    return null;
        //}
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // this.SetCMSTitle();

            base.OnActionExecuting(filterContext);
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //string controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            //string actionName = filterContext.ActionDescriptor.ActionName;

            //switch (controller)
            //{
            //    case "Career":

            //        if (actionName == "Index")
            //            ViewBag.MenuID = "Career";
            //        else if (actionName == "JobsApplicants")
            //            ViewBag.MenuID = "JobApplicants";
            //        break;

            //    case "HomeSetting":
            //        if (actionName.Contains("Mobile") || actionName.Contains("Partner"))
            //            ViewBag.MenuID = actionName;
            //        else
            //            ViewBag.MenuID = controller;
            //        break;

            //    case "SiteMap":
            //        if (actionName.Equals("Index"))
            //            ViewBag.MenuID = "SiteMap";
            //        else
            //            ViewBag.MenuID = actionName;

            //        break;

            //    default:
            //        ViewBag.MenuID = controller;
            //        break;
            //}


            base.OnActionExecuted(filterContext);
        }

        private void SetCMSTitle()
        {
            if (Session[Constants.SESSION_CMS_TITLE] == null)
            {
                string cmsTitle = new BusinessLogic.BusinessHandler.SettingBH().GetSiteSetting().CMSTitleEnglish;
                Session.Add(Constants.SESSION_CMS_TITLE, cmsTitle);
            }
        }

        protected long UserID
        {
            get
            {
                return User.Identity.IsAuthenticated ? Convert.ToInt64(Session[Constants.LOGGED_IN_USER_ID]) : -1;
            }

        }
    }
}