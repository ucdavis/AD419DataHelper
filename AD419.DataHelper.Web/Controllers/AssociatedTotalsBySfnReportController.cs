using AD419.DataHelper.Web.Models;
using System.Linq;
using System.Web.Mvc;

namespace AD419.DataHelper.Web.Controllers
{
    public class AssociatedTotalsBySfnReportController : SuperController
    {
        // GET: FTEReport
        public ActionResult Index()
        {
            var reportName = @"AD-419 Associated Totals By SFN Report";
            var model = new ReportViewerModel(reportName);

            return View(model);
        }
    }
}