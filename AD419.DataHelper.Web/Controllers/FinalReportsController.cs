using AD419.DataHelper.Web.Models;
using System.Web.Mvc;

namespace AD419.DataHelper.Web.Controllers
{
    public class FinalReportsController : SuperController
    {
        // GET: FinalReports
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NonAdminReport()
        {
            var reportName = @"AD-419 Non-Admin Report";
            var model = new ReportViewerModel(reportName);
        
            return View("Report", model);
        }

        public ActionResult AdminReport()
        {
            var reportName = @"AD-419 Admin Report - multi report";
            var model = new ReportViewerModel(reportName);

            return View("Report", model);
        }

        public ActionResult NonAdminReportWithProrateAmounts()
        {
            var reportName = @"AD-419 Non-Admin Report with Prorate Amounts - multi report";
            var model = new ReportViewerModel(reportName);

            return View("Report", model);
        }

        public ActionResult UnassociatedTotalsReport()
        {
            var reportName = @"AD-419 Unassociated Totals - multi report";
            var model = new ReportViewerModel(reportName);

            return View("Report", model);
        }
    }
}