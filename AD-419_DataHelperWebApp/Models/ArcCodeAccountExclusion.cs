namespace AD_419_DataHelperWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ArcCodeAccountExclusion
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Year { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(2)]
        public string Chart { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(7)]
        public string Account { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(6)]
        public string AnnualReportCode { get; set; }

        public string Comments { get; set; }
    }
}
