using System.Web;
using System.Web.Optimization;

namespace SZHPCMS
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/script").Include(
                        "~/Scripts/Jquery/jquery-{version}.js",
                        "~/Scripts/Jquery/jquery.metisMenu.js",
                        "~/Scripts/Jquery/jquery.dataTables.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/customscript").Include(
                "~/Scripts/custom-scripts.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/Jquery/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/chart").Include(
                "~/Scripts/raphael-2.1.0.min.js", "~/Scripts/morris.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/Bootstrap/bootstrap.min.js",
                      "~/Scripts/respond.js",                      
                      "~/Scripts/Bootstrap/bootstrap-select.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/Bootstrap/bootstrap.css",
                      "~/Content/font-awesome.css"));

            bundles.Add(new StyleBundle("~/Content/chartcss").Include(
                "~/Content/morris-0.4.3.min.css"));

            bundles.Add(new StyleBundle("~/Content/StyleEnglish").Include(
                "~/Content/Style_En.css"));

            bundles.Add(new StyleBundle("~/Content/StyleArabic").Include(
                "~/Content/Style_Ar.css"));

            bundles.Add(new StyleBundle("~/Content/customcss").Include(
                "~/Content/custom-styles.css"));

            bundles.Add(new ScriptBundle("~/bundles/CMSCore").Include(
                        "~/Scripts/CMS/core/cms.js",
                        "~/Scripts/CMS/core/cms.ui.enums.js",
                        "~/Scripts/CMS/core/cms.globals.js",
                        "~/Scripts/CMS/core/cms.resources.js",
                        "~/Scripts/CMS/core/cms.debugger.js",
                        "~/Scripts/CMS/core/cms.ui.js",
                        "~/Scripts/CMS/core/cms.ui.common.js"
                        ));

            bundles.Add(new ScriptBundle("~/Content/CMSListing").Include(
                "~/Content/assets/js/bootstrap-select.js",
                "~/Content/assets/js/dataTables/dataTables.bootstrap.js"
               
                
                
                ));

            BundleTable.EnableOptimizations = true;
        }
    }
}
