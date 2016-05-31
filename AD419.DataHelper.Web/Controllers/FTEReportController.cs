using AD419.DataHelper.Web.Models;
using Microsoft.Reporting.WebForms;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace AD419.DataHelper.Web.Controllers
{
    public class FteReportController : SuperController
    {
        // GET: FTEReport
        public ActionResult Index()
        {
            var model = new FTEGreaterThanOneReportModel
            {
                ReportViewer = new ReportViewer
                {
                    SizeToReportContent = true,
                    Width = Unit.Percentage(100),
                    Height = Unit.Percentage(100),
                    ProcessingMode = ProcessingMode.Remote
                }
            };

            model.ReportViewer.ServerReport.ReportPath = "/AD419Reports/FTE Greater than 1";
            model.ReportViewer.ServerReport.ReportServerUrl =
                new Uri("http://testreports.caes.ucdavis.edu/ReportServer/");

            model.ConsolidationCodesForFTECalc = DbContext.ConsolidationCodesForFTECalc.ToList();
            model.DosCodesForFTECalc = DbContext.DosCodesForFTECalc.ToList();
            model.TransDocTypesForFTECalc = DbContext.TransDocTypesForFTECalc.ToList();

            //ViewBag.ReportViewer = model.ReportViewer;
            return View(model);
        }

        public ActionResult Report()
        {
            var reportViewer = new ReportViewer
            {
                SizeToReportContent = true,
                Width = Unit.Percentage(100),
                Height = Unit.Percentage(100),
                ProcessingMode = ProcessingMode.Remote
            };

            reportViewer.ServerReport.ReportPath = "/AD419Reports/FTE Greater than 1";
            reportViewer.ServerReport.ReportServerUrl =
                new Uri("http://testreports.caes.ucdavis.edu/ReportServer/");

            ViewBag.ReportViewer = reportViewer;
            return View();
        }
    }
}