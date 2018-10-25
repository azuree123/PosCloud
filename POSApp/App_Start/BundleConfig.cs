using System.Web.Optimization;

namespace POSApp
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = false;
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Content/vendor/jquery/jquery.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/jquery.unobtrusive*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Content/vendor/modernizr/modernizr.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Content/vendor/bootstrap/js/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/vendor").Include(
                        //"~/Content/vendor/jquery/jquery.js",
                        "~/Content/vendor/jquery-browser-mobile/jquery.browser.mobile.js",
                        //"~/Content/vendor/bootstrap/js/bootstrap.js",
                        "~/Content/vendor/nanoscroller/nanoscroller.js",
                         "~/Content/vendor/bootstrap-datepicker/js/bootstrap-datepicker.js",
                         "~/Content/vendor/magnific-popup/jquery.magnific-popup.js",

                         "~/Content/vendor/jquery-ui/jquery-ui.js",
                         "~/Content/vendor/jqueryui-touch-punch/jqueryui-touch-punch.js",
                         "~/Content/vendor/jquery-appear/jquery-appear.js",

                         "~/Content/vendor/select2/js/select2.js",
                         "~/Content/vendor/bootstrap-multiselect/bootstrap-multiselect.js",
                         "~/Content/vendor/jquery.easy-pie-chart/jquery.easy-pie-chart.js",
                         "~/Content/vendor/flot/jquery.flot.js",
                         "~/Content/vendor/flot.tooltip/flot.tooltip.js",

                         "~/Content/vendor/flot/jquery.flot.pie.js",
                         "~/Content/vendor/flot/jquery.flot.categories.js",

                         "~/Content/vendor/flot/jquery.flot.resize.js",
                         "~/Content/vendor/jquery-sparkline/jquery-sparkline.js",

                         "~/Content/vendor/raphael/raphael.js",
                         "~/Content/vendor/morris.js/morris.js",
                         "~/Content/vendor/gauge/gauge.js",

                          "~/Content/vendor/snap.svg/snap.svg.js",
                          "~/Content/vendor/liquid-meter/liquid.meter.js",
                          "~/Content/vendor/jqvmap/jquery.vmap.js",
                          "~/Content/vendor/jqvmap/data/jquery.vmap.sampledata.js",

                          "~/Content/vendor/jqvmap/maps/jquery.vmap.world.js",
                          "~/Content/vendor/jqvmap/maps/continents/jquery.vmap.africa.js",
                          "~/Content/vendor/jqvmap/maps/continents/jquery.vmap.asia.js",
                          "~/Content/vendor/jqvmap/maps/continents/jquery.vmap.asia.js",
                          "~/Content/vendor/jqvmap/maps/continents/jquery.vmap.europe.js",
                          "~/Content/vendor/jqvmap/maps/continents/jquery.vmap.australia.js",
                          "~/Content/vendor/jqvmap/maps/continents/jquery.vmap.australia.js",
                          "~/Content/vendor/jqvmap/maps/continents/jquery.vmap.north-america.js",
                          "~/Content/vendor/jqvmap/maps/continents/jquery.vmap.south-america.js",
                          "~/Content/javascripts/theme.js",
                          "~/Content/javascripts/theme.custom.js",
                          "~/Content/javascripts/theme.init.js",
                          "~/Content/javascripts/dashboard/examples.dashboard.js",
                          "~/Content/vendor/summernote/summernote.js"



                ));
            bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
                        "~/Content/javascripts/tables/DataTables/datatables.min.js"
                        ));

            //bundles.Add(new ScriptBundle("~/bundles/js").Include(
            //           //"~/js/jquery-2.0.0.min.js",
            //           "~/Content/javascripts/theme.js",
            //           "~/Content/javascripts/theme.custom.js",
            //           "~/Content/javascripts/theme.init.js",
            //            //"~/js/bootstrap.js",
            //           "~/Content/javascripts/dashboard/examples.dashboard.js"

            //   ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      //"~/Content/bootstrap.css",
                      //"~/Content/site.css",
                      "~/Content/vendor/bootstrap/css/bootstrap.css",
                      "~/Content/vendor/font-awesome/css/font-awesome.css",
                       "~/Content/vendor/nanoscroller/nanoscroller.css",
                      "~/Content/vendor/magnific-popup/magnific-popup.css",
                      "~/Content/vendor/bootstrap-datepicker/css/bootstrap-datepicker3.css",
                      "~/Content/vendor/jquery-ui/jquery-ui.css",

                      "~/Content/vendor/select2/css/select2.css",
                      "~/Content/vendor/select2-bootstrap-theme/select2-bootstrap.css",

                      "~/Content/vendor/jquery-ui/jquery-ui.theme.css",
                      "~/Content/vendor/bootstrap-multiselect/bootstrap-multiselect.css",

                       "~/Content/vendor/morris.js/morris.css",
                       "~/Content/stylesheets/theme.css",
                       "~/Content/stylesheets/skins/default.css",
                        "~/Content/stylesheets/theme-custom.css",
                        "~/Content/vendor/summernote/summernote.css"
                        ));

            bundles.Add(new StyleBundle("~/Content/datatables").Include(
                        "~/Content/javascripts/tables/DataTables/datatables.min.css"));
        }
    }
}
