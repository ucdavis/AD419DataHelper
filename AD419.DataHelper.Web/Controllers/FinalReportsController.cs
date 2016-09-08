using System;
using System.Configuration;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

namespace AD419.DataHelper.Web.Controllers
{
    public class FinalReportsController : Controller
    {
        // GET: FinalReports
        private static string _reportServerUrl = ConfigurationManager.AppSettings["ReportServerUrl"];

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NonAdminReport()
        {
            var reportViewer = new ReportViewer
            {
                SizeToReportContent = true,
                Width = Unit.Percentage(100),
                Height = Unit.Percentage(100),
                ProcessingMode = ProcessingMode.Remote
            };

            reportViewer.ServerReport.ReportPath = "/AD419Reports/AD-419 Non-Admin Report";
            reportViewer.ServerReport.ReportServerUrl =
                new Uri(_reportServerUrl);

            ViewBag.ReportViewer = reportViewer;
            return View();
        }

        public ActionResult AdminReport()
        {
            var reportViewer = new ReportViewer
            {
                SizeToReportContent = true,
                Width = Unit.Percentage(100),
                Height = Unit.Percentage(100),
                ProcessingMode = ProcessingMode.Remote
            };

            reportViewer.ServerReport.ReportPath = "/AD419Reports/AD-419 Admin Report - multi report";
            reportViewer.ServerReport.ReportServerUrl =
                new Uri(_reportServerUrl);

            ViewBag.ReportViewer = reportViewer;
            return View();
        }

        public ActionResult NonAdminReportWithProrateAmounts()
        {
            var reportViewer = new ReportViewer
            {
                SizeToReportContent = true,
                Width = Unit.Percentage(100),
                Height = Unit.Percentage(100),
                ProcessingMode = ProcessingMode.Remote
            };

            reportViewer.ServerReport.ReportPath = "/AD419Reports/AD-419 Non-Admin Report with Prorate Amounts - multi report";
            reportViewer.ServerReport.ReportServerUrl =
                new Uri(_reportServerUrl);

            ViewBag.ReportViewer = reportViewer;
            return View();
        }

        public ActionResult UnassociatedTotalsReport()
        {
            var reportViewer = new ReportViewer
            {
                SizeToReportContent = true,
                Width = Unit.Percentage(100),
                Height = Unit.Percentage(100),
                ProcessingMode = ProcessingMode.Remote
            };

            reportViewer.ServerReport.ReportPath = "/AD419Reports/AD-419 Unassociated Totals - multi report";
            reportViewer.ServerReport.ReportServerUrl =
                new Uri(_reportServerUrl);

            ViewBag.ReportViewer = reportViewer;
            return View();
        }
    }
}