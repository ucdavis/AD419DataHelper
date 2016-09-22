using AD419.DataHelper.Web.Models;
using Microsoft.Reporting.WebForms;
using System.Linq;
using System.Web.Mvc;

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

            model.UseStateFiscalYear = false;

            return View(model);
        }

        public ActionResult Report(int id)
        {
            var useStateFiscalYear = id;
            var fiscalYearTitleSegment = useStateFiscalYear == 0 ? "FFY" : "SFY";

            const string reportName = @"Direct and Indirect FFY Expenses by ARC w Account";
            
            var year = FiscalYearService.FiscalYear;
            var yearParameter = new ReportParameter("FiscalYear", year.ToString());
            var reportParameters = new ReportParameterCollection() { yearParameter };

            var useStateFiscalYearParameter = new ReportParameter("UseStateFiscalYear", useStateFiscalYear.ToString());
            reportParameters.Add(useStateFiscalYearParameter);

            var model = new ReportViewerModel(reportName);
            model.ReportViewer.ServerReport.SetParameters(reportParameters);
            model.ReportTitle = fiscalYearTitleSegment + " Expenses by ARC w/Account Report";

            return View(model);
        }
    }
}