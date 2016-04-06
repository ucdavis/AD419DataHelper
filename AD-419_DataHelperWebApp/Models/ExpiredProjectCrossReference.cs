namespace AD_419_DataHelperWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ExpiredProjectCrossReference")]
    public partial class ExpiredProjectCrossReference
    {
        [StringLength(50)]
        public string FromAccession { get; set; }

        public bool? IsActive { get; set; }

        [Key]
        public int RemapID { get; set; }

        [StringLength(50)]
        public string ToAccession { get; set; }
    }
}
