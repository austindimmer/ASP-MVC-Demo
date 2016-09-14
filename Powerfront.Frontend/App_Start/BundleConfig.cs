﻿using System.Web;
using System.Web.Optimization;

namespace Powerfront.Frontend
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
                        "~/Scripts/knockout-*"));

            bundles.Add(new ScriptBundle("~/bundles/pnotify").Include(
            "~/Scripts/pnotify.custom.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/cereal").Include(
            "~/Scripts/cereal*"));

            bundles.Add(new ScriptBundle("~/bundles/editaggregatecustomer").Include(
            "~/Scripts/App/EditAggregateCustomer.js"));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.cyborg.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Scripts/PNotify").Include(
          "~/Scripts/pnotify.custom.min.css"));
        }
    }
}
