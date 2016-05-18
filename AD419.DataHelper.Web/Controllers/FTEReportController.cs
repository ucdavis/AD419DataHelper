using System;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

namespace AD419.DataHelper.Web.Controllers
{
    public class FteReportController : Controller
    {
        // GET: FTEReport
        public ActionResult Index()
        {
            var reportViewer = new ReportViewer
            {
                SizeToReportContent = true,
                Width = Unit.Percentage(100),
                Height = Unit.Percentage(100),
                ProcessingMode = ProcessingMode.Remote
            };

            reportViewer.ServerReport.ReportPath = "/AD419Reports/FTE Greater than 1";
            reportViewer.ServerReport.ReportServerUrl = new Uri("http://testreports.caes.ucdavis.edu/ReportServer/");

            ViewBag.ReportViewer = reportViewer;
            return View();
        }
    }
}