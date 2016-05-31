using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AD419.DataHelper.Web.Models
{
    [Table("FinanceDocTypesForFTECalc")]
    public partial class TransDocTypesForFTECalc
    {
        [Key]
        [StringLength(4)]
        [Required]
        [Display(Name = "Document Type")]
        public string DocumentType { get; set; }

        [StringLength(200)]
        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}
