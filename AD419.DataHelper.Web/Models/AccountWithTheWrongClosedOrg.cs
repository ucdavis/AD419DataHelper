using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace AD419.DataHelper.Web.Models
{
     [Table("AccountsWithTheWrongClosedOrgOrUnknownReportingOrgV")]
    public partial class AccountWithTheWrongClosedOrg
    {
        [Column(Order = 0), Key]
        public string Chart { get; set; }

        [Column(Order = 1), Key]
        public string Account { get; set; }

        [Display(Name = "Current Org")]
        public string CurrentOrg { get; set; }

        [Display(Name = "Latest Non-Closed Org")]
        public string LatestNonClosedOrg  { get; set; }

        [Display(Name = "Current OrgR")]
        public string CurrentOrgR { get; set; }

        [Display(Name = "Latest Non-Closed OrgR")]
        public string LatestNonClosedOrgR { get; set; }

        [Display(Name = "Latest Non-Closed Year-Period")]
        public string LatestNonClosedYearPeriod { get; set; }

        [Display(Name = "Account Name")]
        public string AccountName { get; set; }

        [Display(Name = "Account Purpose")] 
        public string AccountPurpose { get; set; }

        [Display(Name = "Current Org Name")]
        public string CurrentOrgName { get; set; }

        [Display(Name = "Latest Non-Closed Org Name")]
        public string LatestNonClosedOrgName { get; set; }

        [Display(Name = "Latest Non-Closed Home Dept Name")]
        public string LatestNonClosedHomeDepartment { get; set; }

        [Display(Name = "Account Expenses")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Expenses { get; set; }
    }
}