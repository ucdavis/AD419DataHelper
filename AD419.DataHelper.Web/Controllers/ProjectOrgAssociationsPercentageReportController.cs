using AD419.DataHelper.Web.Models;
using System.Web.Mvc;

namespace AD419.DataHelper.Web.Controllers
{
    public class ProjectOrgAssociationsPercentageReportController : SuperController
    {
        // GET: CurrentProjectsReport
        public ActionResult Index()
        {
            var reportName = @"Project-Org Associations Percentage Report";
            var model = new ReportViewerModel(reportName);

            var endingYear = FiscalYearService.FiscalYear;
            var startingYear = endingYear - 1;
            var reportPeriodString = startingYear.ToString() + "-" + endingYear.ToString();
            ViewBag.ReportingPeriod = reportPeriodString;

            return View(model);
        }
    }
}