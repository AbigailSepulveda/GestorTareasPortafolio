using System.Web.Optimization;

namespace web_portafolio {
    public class BundleConfig {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles) {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Content/assets/vendor/jquery/jquery-3.3.1.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Content/assets/vendor/bootstrap/js/bootstrap.bundle.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/assets/vendor/bootstrap/css/bootstrap.min.css",
                      "~/Content/assets/vendor/fonts/circular-std/style.css",
                      "~/Content/assets/libs/css/style.css",
                      "~/Content/assets/vendor/fonts/fontawesome/css/fontawesome-all.css"
                      ));

            // HIGHCHARTS
            bundles.Add(new ScriptBundle("~/bundles/Highcharts").Include(
           "~/Content/Highcharts/highcharts.js",
           "~/Content/Highcharts/exporting.js"
           ));
            /* SELECT SEARCH */
            bundles.Add(new StyleBundle("~/Content/selectSearchCSS").Include(
                      "~/Content/SelectSearch/css/bootstrap-select.min.css"
                      ));
            bundles.Add(new ScriptBundle("~/bundles/selectSearchJS").Include(
                      "~/Content/SelectSearch/js/bootstrap-select.min.js",
                      "~/Content/SelectSearch/js/i18n/defaults-es_CL.min.js"
                      ));

            /* FILE UPLOAD */
            bundles.Add(new StyleBundle("~/Content/fileUploadCCS").Include(
                      "~/Content/FileUpload/dist/css/file-upload.css"
                      ));
            bundles.Add(new ScriptBundle("~/bundles/fileUploadJS").Include(
                      "~/Content/FileUpload/dist/js/file-upload.js",
                      "~/Content/FileUpload/run_prettify.js"
                      ));

            /* DatePicker */
            bundles.Add(new StyleBundle("~/Content/DatePickerCSS").Include(
                      "~/Content/DatePicker/css/bootstrap-datepicker.min.css"
                      ));
            bundles.Add(new ScriptBundle("~/bundles/DatePickerJS").Include(
                      "~/Content/DatePicker/js/bootstrap-datepicker.min.js",
                      "~/Content/DatePicker/locales/bootstrap-datepicker.es.min.js"
                      ));
            /* InputMask */
            bundles.Add(new StyleBundle("~/Content/InputMaskCSS").Include(
                      "~/Content/assets/vendor/inputmask/css/inputmask.css"
                      ));
            bundles.Add(new ScriptBundle("~/bundles/InputMaskJS").Include(
                      "~/Content/assets/vendor/inputmask/js/jquery.inputmask.bundle.js"
                      ));
            /* SWEETALERT2 */
            bundles.Add(new ScriptBundle("~/bundles/sweetAlert2JS").Include(
                      "~/Content/sweetalert2/sweetalert2.all.min.js"
                      ));
            /* SLIM SCROLL */
            bundles.Add(new ScriptBundle("~/bundles/slimScrollJS").Include(
                      "~/Content/assets/vendor/slimscroll/jquery.slimscroll.js"
                      ));
            /* MAIN */
            bundles.Add(new ScriptBundle("~/bundles/mainJS").Include(
                      "~/Content/assets/libs/js/main-js.js"
                      ));
            /* SELECT BOOTSTRAP */
            bundles.Add(new StyleBundle("~/Content/bootstrapSelectCSS").Include(
                      "~/Content/assets/vendor/bootstrap-select/css/bootstrap-select.css"
                      ));
            bundles.Add(new ScriptBundle("~/bundles/bootstrapSelectJS").Include(
                      "~/Content/assets/vendor/bootstrap-select/js/bootstrap-select.js"
                      ));
            /* DATATABLES */
            bundles.Add(new StyleBundle("~/Content/DataTables").Include(
                      "~/Content/DataTables/datatables.css"
                      //"~/Content/DataTables/datatables.min.css",
                      //"~/Content/DataTables/css/dataTables.bootstrap.min.css",
                      //"~/Content/DataTables/css/dataTables.bootstrap4.min.css",
                      //"~/Content/DataTables/css/dataTables.foundation.min.css",
                      //"~/Content/DataTables/css/dataTables.jqueryui.min.css",
                      //"~/Content/DataTables/css/dataTables.semanticui.min.css",
                      //"~/Content/assets/vendor/datatables/css/fixedHeader.bootstrap4.css"
                      ));
            bundles.Add(new ScriptBundle("~/bundles/DataTables").Include(
                      "~/Content/DataTables/datatables.js"
                      //"~/Content/DataTables/datatables.min.js",
                      //"~/Content/DataTables/js/dataTables.bootstrap.min.js",
                      //"~/Content/DataTables/js/dataTables.bootstrap4.min.js",
                      //"~/Content/DataTables/js/dataTables.foundation.min.js",
                      //"~/Content/DataTables/js/dataTables.jqueryui.min.js",
                      //"~/Content/DataTables/js/dataTables.semanticui.min.js",
                      //"~/Content/DataTables/js/jquery.dataTables.min.js",
                      //"~/Content/assets/vendor/datatables/js/dataTables.fixedHeader.min.js"
                      ));



            // Tasks
            bundles.Add(new ScriptBundle("~/bundles/Tasks/Index").Include(
            "~/JS/Tasks.js"
            ));
            bundles.Add(new ScriptBundle("~/bundles/Tasks/Create").Include(
            "~/JS/CreateTask.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/Tasks/Workload").Include(
            "~/JS/Workload.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/Tasks/Rejection").Include(
            "~/JS/Rejection.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/Process/Create").Include(
            "~/JS/CreateProcess.js"
            ));
            bundles.Add(new ScriptBundle("~/bundles/Process/Backlog").Include(
            "~/JS/Backlog.js"
            ));
            bundles.Add(new ScriptBundle("~/bundles/Process/Alerts").Include(
            "~/JS/Alerts.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/Template/TemplateCreate").Include(
            "~/JS/TemplateCreate.js"
            ));
            bundles.Add(new ScriptBundle("~/bundles/Template/TemplateShow").Include(
            "~/JS/TemplateShow.js"
            ));
        }
    }
}
