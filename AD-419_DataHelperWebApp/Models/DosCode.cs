namespace AD_419_DataHelperWebApp.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("DOS_Codes")]
    public partial class DosCode
    {
        [Key]
        [StringLength(3)]
        [MinLength(3)]
        [Required]
        [Display(Name = "DOS Code")]
        public string DOS_Code { get; set; }

        public string Description { get; set; }

        [Required]
        [Display(Name = "Include in AD-419 FTE?")]
        public bool? IncludeInAD419FTE { get; set; }
    }
}
