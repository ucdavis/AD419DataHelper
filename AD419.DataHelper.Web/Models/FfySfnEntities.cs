using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AD419.DataHelper.Web.Models
{
    [Table("FFY_SFN_Entities")]
    public class FfySfnEntities
    {
        [Key]
        public int Id { get; set; }

        public string Chart { get; set; }

        public string Account { get; set; }

        [DisplayName("SFN")]
        public string Sfn { get; set; }

        [Column("Accounts_AwardNum")]
        [DisplayName("Account Award Number")]
        public string AccountsAwardNumber { get; set; }

        [Column("OpFund_AwardNum")]
        [DisplayName("OP Fund Award Number")]
        public string OpFundAwardNumber { get; set; }

        [DisplayName("Accession Number")]
        public string AccessionNumber { get; set; }

        [DisplayName("Project Number")]
        public string ProjectNumber { get; set; }

        [DisplayName("Is Expired?")]
        public bool IsExpired { get; set; }

        [DisplayName("Project End Date")]
        public DateTime ProjectEndDate { get; set; }

        public decimal Expenses { get; set; }

        [Column("FTE")]
        [DisplayName("Full Time Employees")]
        public decimal FullTimeEmployees { get; set; }
    }
}