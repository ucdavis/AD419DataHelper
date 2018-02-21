using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace AD419.DataHelper.Web.Models
{
    public partial class AccountWithDifferentTotalDetails
    {
        [Column(Order = 0), Key]
        public string Chart { get; set; }

        [Column(Order = 1), Key]
        public string Account { get; set; }

        [Display(Name = "OP Fund")]
        public string OpFund { get; set; }

        [Display(Name = "Account Award Num")]
        public string AccountAwardNum { get; set; }

        [Display(Name = "Fund Award Num")]
        public string FundAwardNum { get; set; }

        [Display(Name = "Account PI")]
        public string AccountPi { get; set; }

        [Display(Name = "Fund PI")]
        public string FundPi { get; set; }

        [Display(Name = "Account Name")]
        public string AccountName { get; set; }

        [Display(Name = "Fund Name")]
        public string FundName { get; set; }

        [Display(Name = "Account Purpose")] 
        public string AccountPurpose { get; set; }

        [Display(Name = "OP Fund Project Title")]
        public string OpFundProjectTitle { get; set; }

        [Display(Name = "FFY Expenses By ARC Total")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal FfyExpensesByArcTotal { get; set; }

        private string _expensesTotal;
        [Display(Name = "AD-419 Expenses Total")]
        public string Ad419ExpensesTotal
        {
            get
            {
                return string.IsNullOrEmpty(_expensesTotal) ? "Account did not get transferred to AD-419 expenses table" : string.Format("{0:C}",decimal.Parse(_expensesTotal));
            }
            set
            {
                _expensesTotal = value;
            }
        }

        [NotMapped]
        public string Difference
        {
            get
            {
                if (_expensesTotal != null)
                    return (FfyExpensesByArcTotal - Convert.ToDecimal(_expensesTotal)).ToString(CultureInfo.InvariantCulture);

                return "N/A";
            }
        }

        [Display(Name = "SFN")]
        public string Sfn { get; set; }

        [Display(Name = "Award End Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? AwardEndDate { get; set; }

        [Display(Name = "Expiration Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? ExpirationDate { get; set; }

        [Display(Name = "Org")]
        public string Org { get; set; }

        [Display(Name = "Annual Report Code")]
        public string AnnualReportCode { get; set; }
    }
}