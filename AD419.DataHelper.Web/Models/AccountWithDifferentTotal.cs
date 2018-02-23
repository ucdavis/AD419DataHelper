using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace AD419.DataHelper.Web.Models
{
    public partial class AccountWithDifferentTotal
    {
        [Column(Order = 0), Key]
        public string Chart { get; set; }

        [Column(Order = 1), Key]
        public string Account { get; set; }

        [Display(Name = "FFY Expenses By ARC Total")]
        public decimal FfyExpensesByArcTotal { get; set; }

        private string _expensesTotal;
        [Display(Name = "Expenses Total")]
        public string ExpensesTotal
        {
            get
            {
                return string.IsNullOrEmpty(_expensesTotal) ? "Account did not get transferred to AD-419 expenses table" : _expensesTotal;
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
    }
}