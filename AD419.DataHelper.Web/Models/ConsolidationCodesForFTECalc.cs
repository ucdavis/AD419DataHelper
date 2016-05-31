using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AD419.DataHelper.Web.Models
{
    [Table("ConsolCodesForFTECalc")]
    public partial class ConsolidationCodesForFTECalc
    {
        [Key]
        [StringLength(4)]
        [Column("Obj_Consolidatn_Num")]
        [Required]
        [Display(Name = "Object Consolidation Number")]
        public string ObjectConsolidationNumber { get; set; }

        [StringLength(255)]
        [Column("Obj_Consolidatn_Name")]
        [Required]
        [Display(Name = "Object Consolidation Name")]
        public string ObjectConsolidationName { get; set; }
    }
}
