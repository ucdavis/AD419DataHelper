using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AD419.DataHelper.Web.Models
{
    public class ExpiringProjects
    {
        public string OrgR { get; set; }

        public string ProjectDirector { get; set; }

        public string Title { get; set; }

        public DateTime ProjectEndDate { get; set; }

        public decimal Expenses { get; set; }

        public string SFN { get; set; }

        public string Accounts_AwardNum { get; set; }

        public string OPFund_AwardNum { get; set; }

        public string ProjectNumber { get; set; }

        public string AccenssionNumber { get; set; }
    }
}
