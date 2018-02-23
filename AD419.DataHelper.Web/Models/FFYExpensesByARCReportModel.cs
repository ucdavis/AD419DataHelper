using System.Collections.Generic;

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

        public bool UseStateFiscalYear { get; set; }

        public string YearNameTitle
        {
            get
            {
                if (UseStateFiscalYear)
                    return "SFY";

                return "FFY";
            }
        }

        public List<ArcCodeSelections> ArcCodeSelections { get; set; }

        public List<ArcCodeAccountExclusionSelections> ArcCodeAccountExclusionSelections { get; set; }
    }
}