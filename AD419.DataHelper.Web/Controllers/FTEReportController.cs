using AD419.DataHelper.Web.Models;
using Microsoft.Reporting.WebForms;
using System;
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
                ConsolidationCodesForFTECalc = DbContext.ConsolidationCodesForFTECalc.ToList(),
                DosCodesForFTECalc = DbContext.DosCodesForFTECalc.ToList(),
                TransDocTypesForFTECalc = DbContext.TransDocTypesForFTECalc.ToList()
            };

            return View(model);
        }

        public ActionResult Report()
        {
            var reportName = @"FTE Greater than 1";
            var model = new ReportViewerModel(reportName);

            return View(model);
        }
    }
}