namespace AD419.DataHelper.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    public partial class TransDocType
    {
        [Key]
        [MaxLength(4)]
        [MinLength(2)]
        [Required]
        [Display(Name = "Document Type")]
        public string DocumentType { get; set; }

        [MaxLength(200)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Include in FTE Calculation?")]
        public bool? IncludeInFTECalc { get; set; }

         [Display(Name = "Keep in Non-Labor FIS Expenses?")]
        public bool? IncludeInFISExpenses { get; set; }
    }
}
