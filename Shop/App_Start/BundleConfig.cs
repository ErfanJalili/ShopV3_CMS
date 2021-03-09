using System.Web;
using System.Web.Optimization;

namespace Shop
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/DataTables/jquery.dataTables.js",
                        "~/scripts/typeahead.bundle.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/DataTables/responsive.bootstrap.js",
                      "~/Scripts/bootbox.js",
                      "~/Scripts/DataTables/dataTables.bootstrap.js"


                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                       "~/Content/DataTables/css/dataTables.bootstrap.css",
                       "~/Content/typeahead.css"

                       ));

           

           //bundles.Add(new ScriptBundle("~/bundles/Lib").Include(
           //             "~/Content/bower_components/jquery/dist/jquery.min.js",
           //             "~/Content/bower_components/jquery-ui/jquery-ui.min.js",
           //             "~/Content/bower_components/bootstrap/dist/js/bootstrap.js",
           //             "~/Content/bower_components/raphael/raphael.js",
           //             "~/Content/bower_components/morris.js/morris.js",
           //             "~/Content/bower_components/jquery-sparkline/dist/jquery.sparkline.js",
           //             "~/Content/plugins/jvectormap/jquery-jvectormap-1.2.2.js",
           //             "~/Content/plugins/jvectormap/jquery-jvectormap-world-mill-en.js",
           //             "~/Content/bower_components/jquery-knob/dist/jquery.knob.js",
           //             "~/Content/bower_components/moment/min/moment.js",
           //             "~/Content/bower_components/bootstrap-daterangepicker/daterangepicker.js",
           //             "~/Content/bower_components/bootstrap-datepicker/dist/js/bootstrap-datepicker.js",
           //             "~/Content/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.all.js",
           //             "~/Content/bower_components/jquery-slimscroll/jquery.slimscroll.js",
           //             "~/Content/bower_components/fastclick/lib/fastclick.js",
           //             "~/Content/dist/js/adminlte.js",
           //             "~/Content/dist/js/pages/dashboard.js",
           //             "~/Content/dist/js/demo.js",
           //             "~/Scripts/Scripts.js", 
           //             "~/Scripts/jquery-{version}.js",
           //             "~/Scripts/DataTables/jquery.dataTables.js",
           //             "~/Scripts/bootstrap.js",
           //           "~/Scripts/DataTables/responsive.bootstrap.js",
           //           "~/Scripts/bootbox.js",
           //           "~/Scripts/DataTables/dataTables.bootstrap.js"));

        }
    }
}
