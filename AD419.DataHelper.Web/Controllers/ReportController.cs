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
            var reportName = @"KFS Conversion and Project Comparisons";
            var model = new ReportViewerModel(reportName);

            return View("Report", model);
        }

    }
}
