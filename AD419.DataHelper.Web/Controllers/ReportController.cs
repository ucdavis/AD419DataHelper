using AD419.DataHelper.Web.Models;
using System.Web.Mvc;

namespace AD419.DataHelper.Web.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AeComparisonReport()
        {
            if (User.Identity.IsAuthenticated) {
                var reportName = @"KFS Conversion and Project Comparisons";
                var model = new ReportViewerModel(reportName);

                return View("Report", model);
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult GlPpmReconciliationReport()
        {
            if (User.Identity.IsAuthenticated)
            {
                var reportName = @"GL PPM Reconciliation Report";
                var model = new ReportViewerModel(reportName);

                return View("Report", model);
            }

            return RedirectToAction("Index", "Home");
        }

    }
}
