using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls.Expressions;

namespace AD419.DataHelper.Web.Models
{
    public class FFYExpensesByARCReportModel
    {
        public FFYExpensesByARCReportModel()
        {
        }
        public FFYExpensesByARCReportModel(int fiscalYear)
        {
            FiscalYear = fiscalYear;
        }

        public int FiscalYear { get; set; }
        public List<ArcCodeSelections> ArcCodeSelections { get; set; }

        public List<ArcCodeAccountExclusionSelections> ArcCodeAccountExclusionSelections { get; set; }
    }
}