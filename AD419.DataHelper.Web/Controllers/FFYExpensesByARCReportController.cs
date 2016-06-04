using AD419.DataHelper.Web.Models;
using Microsoft.Reporting.WebForms;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace AD419.DataHelper.Web.Controllers
{
    public class FFYExpensesByARCReportController : SuperController
    {
        // GET: FFYExpensesByARCReport
        public ActionResult Index()
        {
            var model = new FFYExpensesByARCReportModel(2015)
            {
                ArcCodeSelections = FisDbContext.ArcCodes.ToList()
            };

            model.ArcCodeAccountExclusionSelections =
                DbContext.ArcCodeAccountExclusions.Where(a => a.Year == model.FiscalYear)
                    .Select(a => new ArcCodeAccountExclusionSelections
                    {
                        AnnualReportCode = a.AnnualReportCode,
                        Chart = a.Chart,
                        Account = a.Account,
                        Comments = a.Comments

                    }).ToList();            

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

            reportViewer.ServerReport.ReportPath = "/AD419Reports/Direct and Indirect FFY Expenses by ARC";
            reportViewer.ServerReport.ReportServerUrl =
                new Uri("http://testreports.caes.ucdavis.edu/ReportServer/");

            ViewBag.ReportViewer = reportViewer;
            return View();
        }
    }
}