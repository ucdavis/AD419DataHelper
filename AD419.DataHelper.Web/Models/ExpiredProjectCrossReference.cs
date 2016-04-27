using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AD419.DataHelper.Web.Models
{
    [Table("ExpiredProjectCrossReference")]
    public partial class ExpiredProjectCrossReference
    {
        [StringLength(7)]
        [MinLength(7)]
        [Required]
        [Display(Name = "From Accession")]
        public string FromAccession { get; set; }

        [Display(Name = "Is Active?")]
        public bool? IsActive { get; set; }

        [Key]
        public int RemapID { get; set; }

        [StringLength(7)]
        [MinLength(7)]
        [Required]
        [Display(Name = "To Accession")]
        public string ToAccession { get; set; }
    }
}
