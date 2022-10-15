using SZHPCMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using System.Globalization;
using SZHPCMS.Common;

namespace SZHPCMS.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult DashBoard()
        {           

            return View();
        }

        public ActionResult SetCulture(string lang, string returnUrl)
        {

            Session.Remove(SZHPCMS.Common.Constants.SELECTED_LANGUAGE);

            Session.Add(SZHPCMS.Common.Constants.SELECTED_LANGUAGE, lang);

            //Session["Culture"] = new CultureInfo(lang);

            return Redirect(returnUrl);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }



        public ActionResult Create()
        {
            return View();
        }


     
    }
}