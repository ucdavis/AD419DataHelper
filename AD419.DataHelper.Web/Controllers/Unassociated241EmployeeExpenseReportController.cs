using AD419.DataHelper.Web.Models;
using System.Linq;
using System.Web.Mvc;

namespace AD419.DataHelper.Web.Controllers
{
    public class Unassociated241EmployeeExpenseReportController : SuperController
    {
        // GET: FTEReport
        public ActionResult Index()
        {
            var reportName = @"Unassociated241EmployeeExpenses";
            var model = new ReportViewerModel(reportName);

            return View(model);
        }
    }
}