using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AD419.DataHelper.Web.Models
{
    public enum LaborTransactionsOptions
    {
        All,
        ConsolidationCodes,
        DocTypeCodes,
        DosCodes
    };
    public class LaborTransaction
    {
        public string Chart { get; set; }
        public string Account { get; set; }

        [DisplayName("Sub Account")]
        public string SubAccount { get; set; }
        public string Org { get; set; }

        [DisplayName("Consolidation Code")]
        public string ObjConsol { get; set; }

        [DisplayName("Trans Doc. Type")]
        public string FinanceDocTypeCd { get; set; }

        [DisplayName("D.O.S. Code")]
        public string DosCd { get; set; }

        [DisplayName("Employee ID")]
        public string EmployeeID { get; set; }

        [DisplayName("Employee Name")]
        public string EmployeeName { get; set; }

        [DisplayName("Title Code")]
        public string TitleCd { get; set; }

        [DisplayName("Rate Type Code")]
        public string RateTypeCd { get; set; }

        public decimal Payrate { get; set; }

        [DisplayFormat(DataFormatString = "{0:0,0.00}")]
        public decimal Amount { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [DisplayName("Pay Period End Date")]
        public DateTime PayPeriodEndDate { get; set; }

        [DisplayName("Fringe Benefit Salary Code")]
        public string FringeBenefitSalaryCd { get; set; }

        [DisplayName("Annual Report Code")]
        public string AnnualReportCode { get; set; }

        [DisplayName("Excluded By ARC")]
        public bool ExcludedByARC { get; set; }

        [DisplayName("Excluded By Org")]
        public bool ExcludedByOrg { get; set; }

        [DisplayName("Excluded By Account")]
        public bool ExcludedByAccount { get; set; }

        [DisplayName("Excluded By Consol. Code")]
        public bool ExcludedByObjConsol { get; set; }

        [Key]
        public int Id { get; set; }
    }
}