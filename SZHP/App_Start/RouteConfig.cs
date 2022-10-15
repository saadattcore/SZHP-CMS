using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SZHPCMS
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {


            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(name: "HelpIndex", url: "Help/Index", defaults: new { controller = "SiteMap", action = "HelpIndex", id = UrlParameter.Optional });
            routes.MapRoute(name: "HelpCreateUpdate", url: "Help/CreateUpdate", defaults: new { controller = "SiteMap", action = "HelpCreateUpdate" });
            routes.MapRoute(name: "MobileAppIndex", url: "MobileApplication/Index", defaults: new { controller = "HomeSetting", action = "MobileApplication" });
            routes.MapRoute(name: "MobileAppDetail", url: "MobileApplication/CreateUpdate", defaults: new { controller = "HomeSetting", action = "CreateUpdateApp" });

            routes.MapRoute(name: "PartnerServiceIndex", url: "PartnerService/Index", defaults: new { controller = "HomeSetting", action = "PartnerService" });
            routes.MapRoute(name: "PartnerServiceDetail", url: "PartnerService/CreateUpdate", defaults: new { controller = "HomeSetting", action = "CreateUpdatePartnerService" });

            routes.MapRoute(name: "RulesIndex", url: "RulesAndRegulations/Index", defaults: new { controller = "Archive", action = "IndexRules" });
            routes.MapRoute(name: "RulesDetail", url: "RulesAndRegulations/CreateUpdate", defaults: new { controller = "Archive", action = "CreateUpdateRule" });

            routes.MapRoute(name: "MagzineIndex", url: "Magzine/Index", defaults: new { controller = "Archive", action = "IndexMagzine" });
            routes.MapRoute(name: "MagzineDetail", url: "Magzine/CreateUpdate", defaults: new { controller = "Archive", action = "CreateUpdateMagzine" });

            routes.MapRoute(name: "FormIndex", url: "Form/Index", defaults: new { controller = "Archive", action = "FormIndex" });
            routes.MapRoute(name: "FormeDetail", url: "Form/CreateUpdate", defaults: new { controller = "Archive", action = "FormCreateUpdate" });


            routes.MapRoute(name: "ContactIndex", url: "Contact/Index", defaults: new { controller = "Contact", action = "IndexContact" });
            routes.MapRoute(name: "ContactDetail", url: "Contact/CreateUpdate", defaults: new { controller = "Contact", action = "CreateUpdateContact" });

            routes.MapRoute(name: "ContactUsIndex", url: "ContactUs/Index", defaults: new { controller = "Contact", action = "IndexContactUs" });
            routes.MapRoute(name: "ContactUsDetail", url: "ContactUs/CreateUpdate", defaults: new { controller = "Contact", action = "CreateUpdateContactUs" });

            routes.MapRoute(name: "ContactChairmanIndex", url: "ChairmanContact/Index", defaults: new { controller = "Contact", action = "IndexChairmanContact" });
            routes.MapRoute(name: "ContactChairmanDetail", url: "ChairmanContact/Details", defaults: new { controller = "Contact", action = "CreateUpdateContactChairman" });

            routes.MapRoute(name: "SupportRequestIndex", url: "Support/Index", defaults: new { controller = "Contact", action = "IndexSupportRequest" });
            routes.MapRoute(name: "SupportRequestDetail", url: "Support/Details", defaults: new { controller = "Contact", action = "CreateUpdateSupportRequest" });

            routes.MapRoute(name: "ConditionsIndex", url: "Conditions/Index", defaults: new { controller = "Archive", action = "IndexConditions" });
            routes.MapRoute(name: "ConditionDetail", url: "Conditions/CreateUpdate", defaults: new { controller = "Archive", action = "CreateUpdateCondition" });

            routes.MapRoute(name: "OpenDataIndex", url: "OpenData/Index", defaults: new { controller = "Project", action = "IndexOpenData" });
            routes.MapRoute(name: "OpenDataDetail", url: "OpenData/CreateUpdate", defaults: new { controller = "Project", action = "CreateUpdateOpenData" });

            routes.MapRoute(name: "EParticipationIndex", url: "EParticipation/Index", defaults: new { controller = "EServices", action = "IndexParticipation" });
            routes.MapRoute(name: "EParticipationDetail", url: "EParticipation/CreateUpdate", defaults: new { controller = "EServices", action = "CreateUpdateParticipation" });

            routes.MapRoute(name: "CareerIndustry", url: "CareerIndustry/Index", defaults: new { controller = "Career", action = "IndexCareerIndustry" });
            routes.MapRoute(name: "CareerIndustryDetails", url: "CareerIndustry/CreateUpdate", defaults: new { controller = "Career", action = "CreateUpdateIndustry" });

            //routes.MapRoute(name: "Home", url: "/", defaults: new { controller = "Home", action = "Dashboard" });
            
            //routes.MapRoute(name: "Dashboard", url: "Home/DashBoard", defaults: new { controller = "Home", action = "DashBoard" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Dashboard", id = UrlParameter.Optional }
            );
        }
    }
}
