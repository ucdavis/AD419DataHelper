namespace AD_419_DataHelperWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ARC_Codes
    {
        [Key]
        [StringLength(6)]
        public string ARC_Cd { get; set; }

        [StringLength(40)]
        public string ARC_Name { get; set; }

        [StringLength(40)]
        public string OP_Func_Name { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? DS_Last_Update_Date { get; set; }

        public bool? isAES { get; set; }
    }
}
