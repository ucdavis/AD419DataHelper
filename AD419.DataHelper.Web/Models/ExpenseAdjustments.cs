using System.ComponentModel.DataAnnotations;

namespace AD419.DataHelper.Web.Models
{
    /// <summary>
    /// Contains the various adjusts needed to be able to match the FFY totals and the AD-419 UI totals.
    /// </summary>
    public class ExpenseAdjustments
    {
    
        [Display(Name = "Field Station Expenses")]
        [DisplayFormat(DataFormatString = "{0:0,0.00}")]
        public decimal FieldStationExpenses { get; set; }

        [Display(Name = "CE Specialist Expenses")]
        [DisplayFormat(DataFormatString = "{0:0,0.00}")]
        public decimal CeSpecialistExpenses { get; set; }

        [Display(Name = "Outside ARC 204 Expenses")]
        [DisplayFormat(DataFormatString = "{0:0,0.00}")]
        public decimal OutsideArcExpenses { get; set; }

        [Display(Name = "Expired 204 Expenses")]
        [DisplayFormat(DataFormatString = "{0:0,0.00}")]
        public decimal Expired204Expenses { get; set; }

        [Display(Name = "Total Project Expenses Less Than Cutoff Amount Expenses")]
        [DisplayFormat(DataFormatString = "{0:0,0.00}")]
        public decimal TotalProjectLessThanCutoffAmount204Expenses { get; set; }

        [Display(Name = "Total All Research Funds (AD-419)")]
        [DisplayFormat(DataFormatString = "{0:0,0.00}")]
        public decimal TotalAllResearchFundsAd419 { get; set; }

        [Display(Name = "Total FFY Expenses by ARC")]
        [DisplayFormat(DataFormatString = "{0:0,0.00}")]
        public decimal TotalFfyExpensesByArc { get; set; }

        [Display(Name = "Total Adjusted AD-419 Expenses")]
        [DisplayFormat(DataFormatString = "{0:0,0.00}")]
        public decimal TotalAdjustedAd419Expenses
        {
            get
            {
                var retval = TotalAllResearchFundsAd419 -
                             FieldStationExpenses -
                             CeSpecialistExpenses -
                             OutsideArcExpenses +
                             Expired204Expenses +
                             TotalProjectLessThanCutoffAmount204Expenses;

                return retval;
            }
        }

        [Display(Name = "Difference between total FFY Expenses and total Adjusted AD-419 Expenses")]
        [DisplayFormat(DataFormatString = "{0:0,0.00}")]
        public decimal DifferenceBetweenExpenseTotals
        {
            get { return TotalFfyExpensesByArc - TotalAdjustedAd419Expenses; }
        }
    }
}