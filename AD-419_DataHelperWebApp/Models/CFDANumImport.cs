using System.Data;

namespace AD_419_DataHelperWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CFDANumImport")]
    public partial class CFDANumImport
    {
        public CFDANumImport() { }
        public CFDANumImport(DataRow row)
        {
            CFDANum = row["CFDANum"].ToString();
            ProgramTitle = row["ProgramTitle"].ToString();
        }

        [Required]
        [Display(Name = "CFDA Number")]
        [StringLength(10)]
        public string CFDANum { get; set; }

        [Required]
        [Display(Name = "Program Title")]
        [StringLength(300)]
        public string ProgramTitle { get; set; }

        public int Id { get; set; }
    }
}
