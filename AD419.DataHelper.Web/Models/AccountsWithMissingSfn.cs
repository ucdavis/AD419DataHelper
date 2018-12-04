using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace AD419.DataHelper.Web.Models
{
    [Table("AccountsWithMissingSfnV")]
    public partial class AccountsWithMissingSfn
    {
        [Column(Order = 0), Key]
        public string Chart { get; set; }

        [Column(Order = 1), Key]
        public string Account { get; set; }

        [Display(Name = "Org")]
        public string Org { get; set; }

        [Display(Name = "SFN")]
        public string SFN { get; set; }

        [Display(Name = "Annual Report Code")]
        public string AnnualReportCode { get; set; }

        [Display(Name = "FFY Expenses By ARC Total")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal FfyExpensesByArcTotal { get; set; }

        [Display(Name = "CFDA Num")]
        public string CFDANum { get; set; }

        [Display(Name = "OP Fund Group Code")]
        public string OpFundGroupCode { get; set; }

        [Display(Name = "OP Fund Num")]
        public string OpFundNum { get; set; }

        [Display(Name = "Sub-fund Group Num")]
        public string SubFundGroupNum { get; set; }

        [Display(Name = "Sub-fund Group Type Code")]
        public string SubFundGroupTypeCode { get; set; }

        [Display(Name = "Federal Agency Code")]
        public string FederalAgencyCode { get; set; }

        [Display(Name = "NIH Doc Num")]
        public string NIHDocNum { get; set; }

        [Display(Name = "Sponsor Category Code")]
        public string SponsorCategoryCode { get; set; }

        [Display(Name = "Sponsor Code")]
        public string SponsorCode { get; set; }

        [Display(Name = "Accounts Award Num")]
        public string Accounts_AwardNum { get; set; }

        [Display(Name = "Op Fund Award Num")]
        public string OpFund_AwardNum { get; set; }

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

        [Display(Name = "Expiration Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? ExpirationDate { get; set; }

        [Display(Name = "Award End Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? AwardEndDate { get; set; }

        [Display(Name = "Is Account In Financial Data?")]
        public bool IsAccountInFinancialData { get; set; }

        [Display(Name = "Is CE?")]
        [Column("isCE")]
        public int? IsCE { get; set; }

        [Display(Name = "Is NIH?")]
        public bool IsNIH { get; set; }

        [Display(Name = "Is Federal Fund?")]
        public bool IsFederalFund { get; set; }

        [Display(Name = "Is NIFA?")]
        public bool IsNIFA { get; set; }
    }
}