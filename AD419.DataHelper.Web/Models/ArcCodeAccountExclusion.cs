using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AD419.DataHelper.Web.Models
{
    public partial class ArcCodeAccountExclusion
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Year { get; set; }

        [Key]
        [Column(Order = 1)]
        [Required]
        [MaxLength(2)]
        public string Chart { get; set; }

        [Key]
        [Column(Order = 2)]
        [Required]
        [MaxLength(7)]
        public string Account { get; set; }

        [Key]
        [Column(Order = 3)]
        [Required]
        [MaxLength(6)]
        [Display(Name = "Annual Report Code")]
        public string AnnualReportCode { get; set; }

        [Required]
        public string Comments { get; set; }

        [Display(Name = "Is 204?")]
        public bool Is204 { get; set; }

        [MaxLength(20)]
        [Display(Name = "Award Number")]
        public string AwardNumber { get; set; }

        [MaxLength(24)]
        [Display(Name = "Project Number")]
        public string ProjectNumber { get; set; }
    }
}
