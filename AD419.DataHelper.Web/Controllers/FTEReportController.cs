using AD419.DataHelper.Web.Models;
using System.Linq;
using System.Web.Mvc;

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