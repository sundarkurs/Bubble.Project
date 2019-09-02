﻿using System.Web;
using System.Web.Optimization;

namespace NG.Website
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

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/angularjs").Include(
                "~/Scripts/angular.min.js",
                "~/Scripts/angular-route.min.js",
                "~/Scripts/angular-cookies.min.js"));


            bundles.Add(new ScriptBundle("~/bundles/portalApp").Include(
                "~/App/app.js",
                "~/App/app.constant.js",
                "~/App/app.config.js",
                "~/App/app.factory.js"));

            bundles.Add(new ScriptBundle("~/bundles/dashboard").Include(
                "~/App/Dashboard/Dashboard.Controller.js",
                "~/App/Dashboard/Dashboard.Services.js"));


        }
    }
}
