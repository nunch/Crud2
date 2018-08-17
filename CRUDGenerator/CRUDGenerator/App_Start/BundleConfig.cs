using System.Web;
using System.Web.Optimization;

namespace CRUDGenerator
{
    public class BundleConfig
    {
        // Pour plus d'informations sur Bundling, accédez à l'adresse http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            // Utilisez la version de développement de Modernizr pour développer et apprendre. Puis, lorsque vous êtes
            // prêt pour la production, utilisez l'outil de génération sur http://modernizr.com pour sélectionner uniquement les tests dont vous avez besoin.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                //"~/lib/bootstrap/css/bootstrap.css",
				"~/Content/bootstrap-slate.css",
				"~/lib/font-awesome-4.6.1/css/font-awesome.css",
                "~/lib/sweetalert2/dist/sweetalert2.css",
                "~/Content/datatables.css",
                "~/Content/Site.css"
                )
            );
            bundles.Add(new ScriptBundle("~/Scripts/js").Include(
                    "~/Scripts/jquery-1.12.1.js",
                    "~/lib/bootstrap/js/bootstrap.js",
                    "~/lib/sweetalert2/dist/sweetalert2.js",
                    "~/Scripts/jquery.dataTables.js",
                    "~/Scripts/common.js"
                )
            );

            bundles.Add(new StyleBundle("~/Content/head/index/css").Include(
            ));
            bundles.Add(new ScriptBundle("~/Scripts/footer/index/js").Include(
                "~/Scripts/index.js"
            ));

            bundles.Add(new StyleBundle("~/Content/head/showTable/css").Include(
            ));
            bundles.Add(new ScriptBundle("~/Scripts/footer/showTable/js").Include(
                "~/Scripts/showTable.js"
            ));


			bundles.Add(new StyleBundle("~/Content/head/addDatabase/css"));
			bundles.Add(new ScriptBundle("~/Scripts/footer/addDatabase/js").Include(
				"~/Scripts/addDatabase.js"
            ));

            bundles.Add(new StyleBundle("~/Content/head/removeDatabase/css").Include(
                "~/lib/chosen_v1.5.1/chosen.css"
            ));
            bundles.Add(new ScriptBundle("~/Scripts/footer/removeDatabase/js").Include(
                "~/lib/chosen_v1.5.1/chosen.jquery.js",
                "~/Scripts/removeDatabase.js"
            ));

            bundles.Add(new StyleBundle("~/Content/head/extractDatabase/css").Include(
                "~/lib/chosen_v1.5.1/chosen.css"
            ));
            bundles.Add(new ScriptBundle("~/Scripts/footer/extractDatabase/js").Include(
                "~/lib/chosen_v1.5.1/chosen.jquery.js",
                "~/Scripts/extractDatabase.js"
            ));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));
        }
    }
}