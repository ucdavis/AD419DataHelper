
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AD419.DataHelper.Web.Models
{
    [Table("ExpenseOrgR_X_AD419OrgR")]
    public class ExpenseOrgMapping
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Chart { get; set; }

        [Required]
        [Display(Name = "Expense OrgR")]
        public string ExpenseOrgR { get; set; }

        [Display(Name = "Expense Org")]
        public string ExpenseOrg { get; set; }

        [Required]
        [Display(Name = "AD-419 OrgR")]
        public string AD419OrgR { get; set; }
    }
}