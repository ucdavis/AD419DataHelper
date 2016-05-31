using Microsoft.Reporting.WebForms;
using System.Collections.Generic;

namespace AD419.DataHelper.Web.Models
{
    public class FTEGreaterThanOneReportModel
    {
        public List<ConsolidationCodesForFTECalc> ConsolidationCodesForFTECalc { get; set; }

        public List<DosCodesForFTECalc> DosCodesForFTECalc { get; set; }

        public List<TransDocTypesForFTECalc> TransDocTypesForFTECalc { get; set; }

        public ReportViewer ReportViewer { get; set; }
    }
}