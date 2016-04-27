using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AD419.DataHelper.Web.Models
{
    [Table("[204Exclusions]")]
    public partial class C204Exclusions
    {
        [Key]
        [StringLength(50)]
        [Required]
        [Display(Name = "Award Number")]
        public string AwardNumber { get; set; }

        public string Comments { get; set; }
    }
}
