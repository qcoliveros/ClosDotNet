namespace ClosDotNet
{
    using System.Web.Optimization;

    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Common Includes
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Resources/Content/bootstrap.css",
                      "~/Resources/Content/bootstrap-datepicker.css",
                      "~/Resources/Content/bootstrap-touch-carousel.css",
                      "~/Resources/Content/Site.css"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Resources/Scripts/jquery-{version}.js",
                        "~/Resources/Scripts/jquery.blockUI.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Resources/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Resources/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Resources/Scripts/bootstrap.js",
                      "~/Resources/Scripts/bootstrap-datepicker.js",
                      "~/Resources/Scripts/bootstrap-touch-carousel.js",
                      "~/Resources/Scripts/respond.js"));

            // Grid MVC
            bundles.Add(new StyleBundle("~/Content/gridmvc").Include(
                      "~/Resources/Content/Gridmvc.css"));

            bundles.Add(new ScriptBundle("~/bundles/gridmvc").Include(
                      "~/Resources/Scripts/gridmvc.js"));

            // Conditional Validation
            bundles.Add(new ScriptBundle("~/bundles/foolproof").Include(
                      "~/Resources/Scripts/mvcfoolproof.unobtrusive.min.js"));

            // CLEditor
            bundles.Add(new StyleBundle("~/Content/cleditor").Include(
                      "~/Resources/Content/jquery.cleditor.css"));

            bundles.Add(new ScriptBundle("~/bundles/cleditor").Include(
                      "~/Resources/Scripts/jquery.cleditor.min.js"));

            // SmartLender
            bundles.Add(new ScriptBundle("~/bundles/smartlender").Include(
                      "~/Resources/Scripts/sml_common.js"));
        }
    }
}
