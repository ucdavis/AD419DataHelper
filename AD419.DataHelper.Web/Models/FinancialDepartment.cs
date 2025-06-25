using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AD419.DataHelper.Web.Models
{
    [Table("IsAESFinancialDept")]
    public partial class FinancialDepartment
    {
        [Key]
        [Required]
        [StringLength(7)]
        [MinLength(7)]
        [Display(Name = "Financial Department Code")]
        [Column("Financial_Department_Level_G_Child")]
        public string FinancialDepartmentCode { get; set; }

        [Display(Name = "Description")]
        [Column("Financial_Department_Level_G_Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Include in AES Reporting?")]
        [Column("IsAES?")]
        public bool IsAES { get; set; }
    }
}
