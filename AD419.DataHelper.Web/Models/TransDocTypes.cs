namespace AD419.DataHelper.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    public partial class TransDocTypes
    {
        [Key]
        [StringLength(4)]
        [Required]
        [Display(Name = "Document Type")]
        public string DocumentType { get; set; }

        [StringLength(200)]
        [Display(Name = "Document Type")]
        public string Description { get; set; }

        [Display(Name = "Include In FTE Calculation?")]
        public bool? IncludeInFTECalc { get; set; }

         [Display(Name = "Include In FIS Expenses?")]
        public bool? IncludeInFISExpenses { get; set; }
    }
}
