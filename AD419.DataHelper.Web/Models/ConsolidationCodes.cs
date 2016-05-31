using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AD419.DataHelper.Web.Models
{
    using System.Data.Entity;

    public partial class ConsolidationCodes
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

        [Display(Name = "Include In FTE Calculations?")]
        public bool? IncludeInFTECalc { get; set; }

        [Display(Name = "Include In Labor Transactions?")]
        public bool? IncludeInLaborTransactions { get; set; }
    }
}
