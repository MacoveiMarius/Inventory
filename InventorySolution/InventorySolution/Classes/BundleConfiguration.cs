using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace InventorySolution.Classes
{
    public class BundleConfiguration
    {
        public static class ResourcePaths
        {
            //xEditable
            public static string XEditable =
                "~/scripts/bootstrap3-editable-1.5.1/bootstrap3-editable/js/bootstrap-editable.min.js";

            public static string XEditableWysihtml5 =
                "~/scripts/bootstrap-editable-1.5.1/inputs-ext/wysihtml5/bootstrap-wysihtml5-0.0.2/wysihtml5-0.3.0.min.js";

            public static string XEditableWysihtml5BS =
                "~/scripts/bootstrap-editable-1.5.1/inputs-ext/wysihtml5/bootstrap-wysihtml5-0.0.2/bootstrap-wysihtml5-0.0.2.min.js";

            public static string XEditableWysihtml5Config =
                "~/scripts/bootstrap-editable-1.5.1/inputs-ext/wysihtml5/wysihtml5.js";

        }

        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.UseCdn = true; //enable CDN support

            #region CSS

            //Bootstrap CSS
            var bootstrapCSSCdnPath = "//maxcdn.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap.min.css";
            bundles.Add(new StyleBundle("~/content/bootstrap", bootstrapCSSCdnPath)
                .Include("~/content/bootstrap/bootstrap.min.css")
                .Include("~/content/bootstrap/bootstrap-theme.min.css")
                ); //local fallback file

            //xEditable CSS
            bundles.Add(new Bundle("~/xeditable")
                .Include("~/scripts/bootstrap3-editable-1.5.1/bootstrap3-editable/css/bootstrap-editable.css")
                //.Include("~/scripts/bootstrap3-editable-1.5.1/inputs-ext/wysihtml5/bootstrap-wysihtml5-0.0.2/bootstrap-wysihtml5-0.0.2.css")
                );
            #endregion

            #region JS

            //jQuery
            var jqueryCdnPath = "https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.8.2.min.js";
            bundles.Add(new ScriptBundle("~/scripts/jquery", jqueryCdnPath) {CdnFallbackExpression = "window.jQuery"}
                .Include("~/scripts/jquery-1.9.1.min.js")); //local fallback file

            //Twitter Bootstrap
            var bootstrapCdnPath = "//netdna.bootstrapcdn.com/twitter-bootstrap/2.3.2/js/bootstrap.min.js";
            bundles.Add(new ScriptBundle("~/scripts/bootstrap", bootstrapCdnPath) {CdnFallbackExpression = "$.fn.modal"}
                .Include("~/scripts/bootstrap.min.js")); //local fallback file

            //xEditable JS Bundle
            bundles.Add(new ScriptBundle("~/scripts/xeditable").Include(ResourcePaths.XEditable,
                ResourcePaths.XEditableWysihtml5, ResourcePaths.XEditableWysihtml5BS,
                ResourcePaths.XEditableWysihtml5Config));

            bundles.Add(new ScriptBundle("~/scripts/surse")
                .Include("~/scripts/ui-scripts/surse.js"));

            #endregion

            //enables minification
            //BundleTable.EnableOptimizations = true;
        }
    }
}