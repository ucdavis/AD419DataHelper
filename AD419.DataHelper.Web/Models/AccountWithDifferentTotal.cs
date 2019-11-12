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

        [Display(Name = "Expenses Total")]
        public decimal? ExpensesTotal { get; set; }

        [NotMapped]
        public string Difference => ExpensesTotal != null ? (FfyExpensesByArcTotal - Convert.ToDecimal(ExpensesTotal)).ToString(CultureInfo.InvariantCulture) : "N/A";
    }
}