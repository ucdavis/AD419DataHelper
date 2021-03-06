using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AD419.DataHelper.Web.Models
{
    public partial class ArcCodeAccountExclusionSelections
    {
        [Key]
        [Column(Order = 1)]
        [Required]
        [StringLength(2)]
        public string Chart { get; set; }

        [Key]
        [Column(Order = 2)]
        [Required]
        [StringLength(7)]
        [MinLength(7)]
        public string Account { get; set; }

        [Key]
        [Column(Order = 3)]
        [Required]
        [StringLength(6)]
        [MinLength(6)]
        [Display(Name = "Annual Report Code")]
        public string AnnualReportCode { get; set; }

         [Required]
        public string Comments { get; set; }
    }
}
