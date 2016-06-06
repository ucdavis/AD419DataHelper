using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AD419.DataHelper.Web.Models
{
    [Table("ARCCodes")]
    public partial class ArcCodeSelections
    {
        [Key]
        [StringLength(6)]
        [Required]
        [Display(Name = "Annual Report Code")]
        [Column("ARCCode")]
        public string Code { get; set; }

        [StringLength(40)]
        [Required]
        [Display(Name = "Name")]
        [Column("ARCName")]
        public string Name { get; set; }
    }
}
