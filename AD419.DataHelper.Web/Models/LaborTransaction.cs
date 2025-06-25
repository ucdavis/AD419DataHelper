using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AD419.DataHelper.Web.Models
{
    public enum LaborTransactionsOptions
    {
        All = 0,
        FinancialDepartments = 1, 
        AEFunds = 2,
        DosCodes = 3,
        AEAccounts = 4,
        Activities = 5,
        ConsolidationCodes = 6, // SHOULD THIS BE REMOVED?
        DocTypeCodes = 7 // SHOULD THIS BE REMOVED?
    }
    public class LaborTransaction
    {
        [DisplayName("AE Entity")]
        public string AE_Entity { get; set; }

        [DisplayName("AE Fund")]
        public string AE_Fund { get; set; }

        [DisplayName("AE Financial Dept")]
        public string AE_FinancialDept { get; set; }

        [DisplayName("AE Parent Dept")]
        public string AE_ParentDept { get; set; }

        [DisplayName("AE Account")]
        public string AE_Account { get; set; }

        [DisplayName("AE Purpose")]
        public string AE_Purpose { get; set; }

        [DisplayName("AE Program")]
        public string AE_Program { get; set; }

        [DisplayName("AE Project")]
        public string AE_Project { get; set; }

        [DisplayName("AE Activity")]
        public string AE_Activity { get; set; }

        [DisplayName("Trans Doc. Type")]
        public string FinanceDocTypeCd { get; set; }

        [DisplayName("ERN Code")]
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

        [DisplayName("Excluded By Financial Dept")]
        public bool? ExcludedByFinancialDept { get; set; }

        [DisplayName("Excluded By AE Account")]
        public bool? ExcludedByAeAccount { get; set; }

        [DisplayName("Excluded By AE Fund")]
        public bool? ExcludedByAeFund { get; set; }

        [DisplayName("Excluded By Activity")]
        public bool? ExcludedByActivity { get; set; }

        [DisplayName("FTE Excluded By ERN Code")]
        public bool? FteExcludedByERN_Code { get; set; }

        [Key]
        public int Id { get; set; }
    }
}