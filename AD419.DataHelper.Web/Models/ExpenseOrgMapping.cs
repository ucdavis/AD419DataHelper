
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AD419.DataHelper.Web.Models
{
    [Table("ExpenseOrgR_X_AD419OrgR")]
    public class ExpenseOrgMapping
    {
        [Key]
        public int Id { get; set; }

        public string Chart { get; set; }

        public string ExpenseOrgR { get; set; }

        public string AD419OrgR { get; set; }
    }
}