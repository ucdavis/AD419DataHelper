using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AD419.DataHelper.Web.Models
{
    [Table("NewAccountSFN")]
    public partial class NewAccountSfn
    {
        [Column(Order = 0), Key]
        public string Chart { get; set; }

        [Column(Order = 1), Key]
        public string Account { get; set; }

        [Display(Name = "Org")]
        public string Org { get; set; }

        [Display(Name = "Is CE?")]
        [Column("isCE")]
        public int? IsCE { get; set; }

        [Required]
        [Display(Name = "SFN")]
        public string SFN { get; set; }

        [Display(Name = "CFDA Num")]
        public string CFDANum { get; set; }

        [Display(Name = "OP Fund Group Code")]
        public string OpFundGroupCode { get; set; }

        [Display(Name = "OP Fund Num")]
        public string OpFundNum { get; set; }

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

        [Display(Name = "Expiration Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? ExpirationDate { get; set; }

        [Display(Name = "Award End Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime AwardEndDate { get; set; }

        [Display(Name = "Is Account In Financial Data?")]
        public bool IsAccountInFinancialData { get; set; }

        [Display(Name = "Sub-Fund Group Num")]
        public string SubFundGroupNum { get; set; }

        [Display(Name = "Sub-Fund Group Type Code")]
        public string SubFundGroupTypeCode { get; set; }

        [Display(Name = "Is NIH?")]
        public bool IsNIH { get; set; }

        [Display(Name = "Is Federal Fund?")]
        public bool IsFederalFund { get; set; }

        [Display(Name = "Is NIFA?")]
        public bool IsNIFA { get; set; }
    }
}