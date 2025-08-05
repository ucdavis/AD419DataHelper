using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AD419.DataHelper.Web.Models
{
    [Table("AE_FundsAndHierarchy_New")]
    public class Fund
    {
        [Key]
        [Column("Fund")]
        public string FundCode { get; set; }

        public string FundName { get; set; }

        public string Parent1 { get; set; }
        public string Parent1Name { get; set; }

        public string Parent2 { get; set; }
        public string Parent2Name { get; set; }

        public string Parent3 { get; set; }
        public string Parent3Name { get; set; }

        public string Parent4 { get; set; }
        public string Parent4Name { get; set; }

        public string FundLevel { get; set; }

        // Classification field - nullable boolean for three-state (Not Set/Yes/No)
        public string SFN { get; set; }
    }
}