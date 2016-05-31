using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AD419.DataHelper.Web.Models
{
    [Table("DOSCodes")]
    public partial class DosCodesForFTECalc
    {
        [Key]
        [StringLength(3)]
        [MinLength(3)]
        [Required]
        [Display(Name = "DOS Code")]
        public string DOS_Code { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

    }
}
