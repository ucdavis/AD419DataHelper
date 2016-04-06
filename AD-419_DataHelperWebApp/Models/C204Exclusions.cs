namespace AD_419_DataHelperWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("204Exclusions")]
    public partial class C204Exclusions
    {
        [Key]
        [StringLength(50)]
        public string AwardNumber { get; set; }
    }
}
