using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;
using BusinessLogic.BusinessHandler;
using SZHPCMS.Common;
using SZHPCMS.Models;
using System.Reflection;

namespace SZHPCMS.Utilities
{
    public class Utility
    {
        public static void UploadFile(HttpPostedFileBase postedFile, string path, long itemID)
        {

            try
            {
                if (string.IsNullOrEmpty(path) || postedFile.ContentLength == 0)
                    throw new ArgumentNullException("Arguments are in correct");

                string filePath = Path.Combine(path, itemID + "_" + postedFile.FileName);

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                if (File.Exists(filePath))
                    File.Delete(filePath);

                postedFile.SaveAs(filePath);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static void UploadFile(HttpPostedFileBase postedFile, string path)
        {

            try
            {
                if (string.IsNullOrEmpty(path) || postedFile.ContentLength == 0)
                    throw new ArgumentNullException("Arguments are in correct");

                string filePath = Path.Combine(path, postedFile.FileName);

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                if (File.Exists(filePath))
                    File.Delete(filePath);

                postedFile.SaveAs(filePath);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static void DeleteFile(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(Constants.PARAMETER_NULL_MESSAGE);

            try
            {
                if (File.Exists(path))
                    File.Delete(path);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static List<SelectListItem> GetActionOptions()
        {
            var rowStatusList = new GenericBH().GetRowStatusList();

            List<SelectListItem> dropDownMenus = new List<SelectListItem>();
            dropDownMenus.Add(new SelectListItem() { Text = "--Select An Action--", Value = "-1", Selected = true });

            foreach (var item in rowStatusList)
            {
                dropDownMenus.Add(new SelectListItem() { Text = item.TitleEnglish, Value = item.RowStatusId.ToString() });
            }

            return dropDownMenus;
        }
        public static string DocumentUploadFolder
        {
            get
            {
                return Path.Combine(HttpContext.Current.Server.MapPath("~"), "Documents");
            }
        }
        public static string FormatDateTime(DateTime? dateTime)
        {
            return dateTime.HasValue ? dateTime.Value.ToString("MM/dd/yyyy") : "N/A";
        }

        public static string GetFileName(string fileName)
        {
            return fileName.Contains('.') ? fileName.Substring(0, fileName.IndexOf('.')) : fileName;
        }

        public static long GetUserID(string userName)
        {
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                var user = ctx.Users.Where(x => x.UserName.Equals(userName)).FirstOrDefault();

                return user.Id;
            }

        }

        public static string GetUrl()
        {
            HttpRequest request = HttpContext.Current.Request;



            string url = string.Empty;

            //User click for english
            if (!string.IsNullOrEmpty(request.QueryString["lang"]))
            {
                if (request.QueryString["lang"].Equals("ar"))
                {

                    //url = RemoveQueryStringByKey(request.Url.ToString(), "lang");
                    //url = url.Substring(0, url.Length - 1);


                    //  url = request.Url.ToString();

                    url = "/Home/SetCulture";

                }

            }
            // User click for arabic
            else
            {
                url = "/Home/SetCulture/?lang=ar";

                //if (request.Url.AbsolutePath.ToString().Equals("/"))
                //    url = request.Url.AbsolutePath + "?lang=ar";

                //else
                //    url = request.Url.AbsolutePath + "/?lang=ar";

                //   url += request.Url.AbsolutePath == "/" ? "?lang=ar" : "/?lang=ar";

                //url = request.Url.AbsolutePath == "/" ? "?lang=ar" : "/?lang=ar";       
            }

            return url;
        }

        public static string RemoveQueryStringByKey(string url, string key)
        {
            var uri = new Uri(url);

            // this gets all the query string key value pairs as a collection
            var newQueryString = HttpUtility.ParseQueryString(uri.Query);

            // this removes the key if exists
            newQueryString.Remove(key);

            // this gets the page path from root without QueryString
            string pagePathWithoutQueryString = uri.GetLeftPart(UriPartial.Path);

            return newQueryString.Count > 0
                ? String.Format("{0}?{1}", pagePathWithoutQueryString, newQueryString)
                : pagePathWithoutQueryString;
        }

        public static string GetCSS()
        {
            string lang = HttpContext.Current.Session[SZHPCMS.Common.Constants.SELECTED_LANGUAGE] as string;

            if (!string.IsNullOrEmpty(lang))
            {
                lang = lang == "ar" ? "/Content/Style_Ar.css" : "/Content/Style_En.css";
            }

            return lang;
        }


        //public static Dictionary<string, int> GetDays()
        //{
        //    Dictionary<string, int> days = new Dictionary<string, int>();

        //    for (int i = 1; i <= 7; i++)
        //    {
        //        days.Add(Enum.GetName(typeof(Days), i), i);

        //    }

        //    return days;
        //}

    }
}