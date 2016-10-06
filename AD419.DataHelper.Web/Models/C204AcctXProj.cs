using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AD419.DataHelper.Web.Models
{
    [Table("[204AcctXProj]")]
    public partial class C204AcctXProj
    {
        [Key]
        [Column("pk")]
        public int Id { get; set; }

        [Required]
        [StringLength(7)]
        [MinLength(7)]
        [Display(Name = "Account Number")]
        public string AccountID { get; set; }

        [Required]
        //[DisplayFormat(DataFormatString = "{0:C}")]
        [DisplayFormat(DataFormatString = "{0:0,0.00}")]
        public double Expenses { get; set; }

        [StringLength(7)]
        [MinLength(7)]
        public string Accession { get; set; }

        [StringLength(24)]
        [Display(Name = "Project Number")]
        public string ProjectNumber { get; set; }

        [StringLength(1)]
        [MinLength(1)]
        [Required]
        public string Chart { get; set; }

        [Display(Name = "Is 219?")]
        public bool? Is219 { get; set; }

        [StringLength(20)]
        [Display(Name = "Project Contract Number")]
        public string CSREES_ContractNo { get; set; }

        [StringLength(50)]
        [Display(Name = "Account Award Num (OP Fund Award Num)")]
        public string AwardNum { get; set; }

        [StringLength(6)]
        [MinLength(3)]
        [Display(Name = "OP Fund Number)")]
        public string OpfundNum { get; set; }

        [Display(Name = "Is Current AD-419 Project?")]
        public bool? IsCurrentProject { get; set; }

        [StringLength(4)]
        [MinLength(4)]
        [Required]
        public string Org { get; set; }

        [StringLength(4)]
        [MinLength(4)]
        [Required]
        public string OrgR { get; set; }

        [Display(Name = "Is Excluded Expense?")]
        public bool? IsExcludedExpense { get; set; }
    }
}
