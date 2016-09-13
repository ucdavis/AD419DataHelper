using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AD419.DataHelper.Web.Models
{
    [Table("FFY_SFN_Entries")]
    public class FfySfnEntry
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(2)]
        public string Chart { get; set; }

        [MaxLength(7)]
        public string Account { get; set; }

        [DisplayName("SFN")]
        [MaxLength(5)]
        public string Sfn { get; set; }

        [Column("Accounts_AwardNum")]
        [DisplayName("Account Award Num")]
        [MaxLength(50)]
        public string AccountsAwardNumber { get; set; }

        [Column("OpFund_AwardNum")]
        [DisplayName("OP Fund Award Num")]
        [MaxLength(50)]
        public string OpFundAwardNumber { get; set; }

        [DisplayName("Accession Number")]
        [MaxLength(10)]
        public string AccessionNumber { get; set; }

        [DisplayName("Project Number")]
        [MaxLength(24)]
        public string ProjectNumber { get; set; }

        [DisplayName("Is Expired?")]
        public bool? IsExpired { get; set; }

        [DisplayName("Project End Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ProjectEndDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:0,0.00}")]
        public decimal? Expenses { get; set; }

        [Column("FTE")]
        [DisplayName("FTE")]
        public decimal? FullTimeEmployees { get; set; }
    }
}