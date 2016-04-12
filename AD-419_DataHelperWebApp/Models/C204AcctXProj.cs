namespace AD_419_DataHelperWebApp.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("[204AcctXProj]")]
    public partial class C204AcctXProj
    {
        [Required]
        [StringLength(7)]
        [MinLength(7)]
        [Display(Name = "Account Number")]
        public string AccountID { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double Expenses { get; set; }

        [StringLength(7)]
        [MinLength(7)]
        [Required]
        public string Accession { get; set; }

        [StringLength(1)]
        [MinLength(1)]
        [Required]
        public string Chart { get; set; }

        [Key]
        public int pk { get; set; }

        [Display(Name = "Is 219?")]
        public bool? Is219 { get; set; }

        [StringLength(20)]
        [Display(Name = "Project Contract Number")]
        public string CSREES_ContractNo { get; set; }

        [StringLength(20)]
        [Display(Name = "Account Award Number")]
        public string AwardNum { get; set; }

        [Display(Name = "Is Current AD-419 Project?")]
        public bool? IsCurrentProject { get; set; }

        [StringLength(4)]
        [MinLength(4)]
        [Required]
        public string Org { get; set; }

        [StringLength(4)]
        [MinLength(7)]
        [Required]
        public string OrgR { get; set; }

        [Display(Name = "Is Excluded Expense?")]
        public bool? IsExcludedExpense { get; set; }
    }
}
