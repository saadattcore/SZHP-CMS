using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using global::System.Resources;
using System.Web;
using System.Globalization;

namespace SZHPCMS.Utilities
{
    public class GlobalDisplayNameAttribute : DisplayNameAttribute
    {
        public GlobalDisplayNameAttribute(string resourcesName)
            : base(GetMessageFromResource(resourcesName))
        {

        }

        private static string GetMessageFromResource(string resourcesName)
        {
            //strResources = HttpContext.GetGlobalResourceObject("Resources", resourcesName) as String;
            return LocalizedString.T(resourcesName);

        }
    }

    public static class LocalizedString
    {

        public static string T(string key)
        {
           // string language = HttpContext.Current.Session[Common.Constants.SELECTED_LANGUAGE] as string;

          //  ResourceManager rm = language == "ar" ? new ResourceManager("SZHPCMS.App_GlobalResources.Resources.ar", Assembly.GetExecutingAssembly()) : new ResourceManager("SZHPCMS.App_GlobalResources.Resources", Assembly.GetExecutingAssembly());

            ResourceManager rm = new ResourceManager("SZHPCMS.App_GlobalResources.Resources", Assembly.GetExecutingAssembly());

            String strResources = string.Empty;

            try
            {
                strResources = rm.GetString(key);
            }
            catch (Exception)
            {
                strResources = "Not Found";
            }
            return strResources;
        }
    }
}