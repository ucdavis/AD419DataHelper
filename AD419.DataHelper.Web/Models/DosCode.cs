using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AD419.DataHelper.Web.Models
{
    [Table("DOS_Codes")]
    public partial class DosCode
    {
        [Key]
        [StringLength(3)]
        [MinLength(3)]
        [Required]
        [Display(Name = "DOS Code")]
        [Column("DOS_Code")]
        public string Code { get; set; }

        public string Description { get; set; }

        [Required]
        [Display(Name = "Include in AD-419 FTE?")]
        public bool? IncludeInAD419FTE { get; set; }
    }
}
