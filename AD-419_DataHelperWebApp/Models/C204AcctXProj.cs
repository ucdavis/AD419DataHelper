namespace AD_419_DataHelperWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("204AcctXProj")]
    public partial class C204AcctXProj
    {
        [Required]
        [StringLength(7)]
        public string AccountID { get; set; }

        public double Expenses { get; set; }

        public double? DividedAmount { get; set; }

        [StringLength(7)]
        public string Accession { get; set; }

        [StringLength(1)]
        public string Chart { get; set; }

        [Key]
        public int pk { get; set; }

        public bool? Is219 { get; set; }

        [StringLength(20)]
        public string CSREES_ContractNo { get; set; }

        [StringLength(20)]
        public string AwardNum { get; set; }

        public bool? IsCurrentProject { get; set; }

        [StringLength(4)]
        public string Org { get; set; }

        [StringLength(4)]
        public string OrgR { get; set; }
    }
}
