using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AD419.DataHelper.Web.Models
{
    [Table("FFY_SFN_Entities")]
    public class FfySfnEntities
    {
        [Key]
        public int Id { get; set; }

        public string Chart { get; set; }

        public string Account { get; set; }

        public string Sfn { get; set; }

        [Column("Accounts_AwardNum")]
        public string AccountsAwardNumber { get; set; }

        [Column("OpFund_AwardNum")]
        public string OpFundAwardNumber { get; set; }

        public string AccessionNumber { get; set; }

        public string ProjectNumber { get; set; }

        public bool IsExpired { get; set; }

        public DateTime ProjectEndDate { get; set; }

        public decimal Expenses { get; set; }

        [Column("FTE")]
        public decimal FullTimeEmployees { get; set; }
    }
}