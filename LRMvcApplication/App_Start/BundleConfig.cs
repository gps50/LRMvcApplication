using System.Web;
using System.Web.Optimization;

namespace LRMvcApplication
{
	public class BundleConfig
	{
		// For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
		public static void RegisterBundles(BundleCollection bundles)
		{
#if DEBUG
			BundleTable.EnableOptimizations = false;
			bundles.UseCdn = false;
#else
			BundleTable.EnableOptimizations = true;
			bundles.UseCdn = true;
#endif
			//
			// scripts:
			//

			bundles.Add(
				new ScriptBundle(
					"~/bundles/js/jquery",
					"//ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js")
					.Include(
						"~/Scripts/jquery/2.1.1/jquery.min.js"));

			bundles.Add(
				new ScriptBundle(
					"~/bundles/js/jquery-ui",
					"//ajax.googleapis.com/ajax/libs/jqueryui/1.11.1/jquery-ui.min.js")
					.Include(
						"~/Scripts/jquery-ui/1.11.1/jquery-ui.min.js"));

			bundles.Add(
				new ScriptBundle("~/bundles/js/univer")
					.IncludeDirectory(
						"~/Scripts/univer",
						"*.js",
						true)
					);

			//
			// css:
			//
			bundles.Add(
				new StyleBundle(
					"~/bundles/css/jquery-ui",
					"//ajax.googleapis.com/ajax/libs/jqueryui/1.11.1/themes/smoothness/jquery-ui.min.css")
				.Include(
					"~/Content/jquery-ui/1.11.1/smoothness/jquery-ui.min.css",
					new CssRewriteUrlTransform()
				));


			bundles.Add(
				new StyleBundle("~/bundles/css/univer")
					.Include(
						"~/Content/univer/*.css",
						new CssRewriteUrlTransform()
					));

		}
	}
}