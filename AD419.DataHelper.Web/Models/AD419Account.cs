using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace AD419.DataHelper.Web.Models
{
     [Table("AD419Accounts")]
    public partial class AD419Account
    {
        [Column(Order = 0), Key]
        public string Chart { get; set; }

        [Column(Order = 1), Key]
        public string Account { get; set; }

        [Display(Name = "Org")]
        public string Org { get; set; }

        [Display(Name = "OrgR")]
        public string OrgR { get; set; }

        [Display(Name = "Expenses")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Expenses { get; set; }

        [Display(Name = "SFN")]
        public string SFN { get; set; }

        [Display(Name = "Latest Non-Closed Year-Period")]
        public string LatestNonClosedYearPeriod { get; set; }

        [Display(Name = "Is Expired?")]
        public bool IsExpired { get; set; }

        [Display(Name = "Have Orgs Been Adjusted?")]
        public bool HaveOrgBeenAdjusted { get; set; }

    }
}