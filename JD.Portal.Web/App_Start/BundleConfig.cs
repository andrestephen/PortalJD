﻿using System.Web.Optimization;
using WebHelpers.Mvc5;

namespace JD.Portal.Web.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Bundles/css")
                .Include("~/Content/css/bootstrap.min.css", new CssRewriteUrlTransformAbsolute())
                .Include("~/Content/css/bootstrap-select.css")
                .Include("~/Content/css/bootstrap-datepicker3.min.css")
                .Include("~/Content/css/font-awesome.min.css", new CssRewriteUrlTransformAbsolute())
                //.Include("~/Content/css/icheck/blue.min.css", new CssRewriteUrlTransformAbsolute())
                .Include("~/Content/css/AdminLTE.css", new CssRewriteUrlTransformAbsolute())
                .Include("~/Content/css/skins/skin-blue.css")
                .Include("~/Content/css/ionicons.min.css", new CssRewriteUrlTransformAbsolute())
                .Include("~/Content/css/sistema.css"));

            bundles.Add(new ScriptBundle("~/Bundles/js")
                .Include("~/Content/js/plugins/jquery/jquery-3.3.1.js")
                .Include("~/Content/js/plugins/bootstrap/bootstrap.js")
                .Include("~/Content/js/plugins/fastclick/fastclick.js")
                .Include("~/Content/js/plugins/slimscroll/jquery.slimscroll.js")
                .Include("~/Content/js/plugins/bootstrap-select/bootstrap-select.js")
                .Include("~/Content/js/plugins/moment/moment.js")
                .Include("~/Content/js/plugins/datepicker/bootstrap-datepicker.js")
                //.Include("~/Content/js/plugins/icheck/icheck.js")
                .Include("~/Content/js/plugins/validator.js")
                .Include("~/Content/js/plugins/inputmask/jquery.inputmask.bundle.js")
                .Include("~/Content/js/adminlte.js")
                .Include("~/Content/js/init.js"));

            bundles.Add(new StyleBundle("~/Bundles/datatablecss")
                .Include("~/Content/css/dataTables.bootstrap.min.css")
                .Include("~/Content/css/responsive.dataTables.min.css"));

            bundles.Add(new ScriptBundle("~/Bundles/datatablejs")
               .Include("~/Content/js/plugins/datatables.net/jquery.dataTables.min.js")
               .Include("~/Content/js/plugins/datatables.net-bs/dataTables.bootstrap.min.js")
               .Include("~/Content/js/plugins/datatables.net-rs/dataTables.responsive.min.js")
               .Include("~/Content/js/plugins/datatables.net-moment/datetime-moment.js"));

            bundles.Add(new StyleBundle("~/Bundles/chartcss")
                .Include("~/Content/css/Chart.min.css"));

            bundles.Add(new StyleBundle("~/Bundles/chartjs")
               .Include("~/Content/js/plugins/chart.js/Chart.min.js"));


            bundles.Add(new ScriptBundle("~/Bundles/angular")
                .Include("~/Content/js/plugins/angular/angular.min.js"));

            bundles.Add(new ScriptBundle("~/Bundles/angular-file-upload")
               .Include("~/Content/js/plugins/angular-file-upload/ng-file-upload-shim.min.js")
               .Include("~/Content/js/plugins/angular-file-upload/ng-file-upload.min.js"));

            bundles.Add(new ScriptBundle("~/Bundles/controllerprojetos")
                .Include("~/Content/js/controllers/projetos.js"));

            bundles.Add(new ScriptBundle("~/Bundles/controlleratendimentos")
           .Include("~/Content/js/controllers/atendimentos.js"));

            bundles.Add(new ScriptBundle("~/Bundles/controlleranexo")
           .Include("~/Content/js/controllers/anexo.js"));

#if DEBUG
            BundleTable.EnableOptimizations = false;
#else
            BundleTable.EnableOptimizations = true;
#endif
        }
    }
}
