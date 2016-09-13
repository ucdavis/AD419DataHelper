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
            var year = FiscalYearService.FiscalYear;
            var model = new FFYExpensesByARCReportModel(year)
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
            var reportName = @"Direct and Indirect FFY Expenses by ARC w Account";
            var year = FiscalYearService.FiscalYear;
            var yearParameter = new ReportParameter("FiscalYear", year.ToString());
            var reportParameters = new ReportParameterCollection() { yearParameter };

            var model = new ReportViewerModel(reportName);
            model.ReportViewer.ServerReport.SetParameters(reportParameters);

            return View(model);
        }
    }
}