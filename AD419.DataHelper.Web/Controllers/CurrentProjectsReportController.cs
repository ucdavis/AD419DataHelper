using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

namespace AD419.DataHelper.Web.Controllers
{
    public class CurrentProjectsReportController : SuperController
    {
        // GET: CurrentProjectsReport
        public ActionResult Index()
        {
            var reportViewer = new ReportViewer
            {
                SizeToReportContent = true,
                Width = Unit.Percentage(100),
                Height = Unit.Percentage(100),
                ProcessingMode = ProcessingMode.Remote
            };

            reportViewer.ServerReport.ReportPath = "/AD419Reports/Current AD419 Projects";
            reportViewer.ServerReport.ReportServerUrl =
                new Uri(ReportServerUrl);
            
            ViewBag.ReportViewer = reportViewer;
            return View();
        }
    }
}