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
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal FfyExpensesByArcTotal { get; set; }

        [Display(Name = "Expenses Total")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal? ExpensesTotal { get; set; }

        [NotMapped]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public string Difference => ExpensesTotal.HasValue ? (FfyExpensesByArcTotal - Convert.ToDecimal(ExpensesTotal)).ToString(CultureInfo.InvariantCulture) : "N/A";
    }
}