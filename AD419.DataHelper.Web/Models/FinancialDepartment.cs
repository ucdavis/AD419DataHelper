using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AD419.DataHelper.Web.Models
{ 
    [Table("IsAESFinancialDept")]
    public partial class FinancialDepartment
    {
        [Display(Name = "Parent A")]
        [Column("Financial_Department_Parent_A")]
        public string Financial_Department_Parent_A { get; set; }

        [Display(Name = "Parent A Description")]
        [Column("Financial_Department_Parent_A_Description")]
        public string Financial_Department_Parent_A_Description { get; set; }

        [Display(Name = "Parent B")]
        [Column("Financial_Department_Parent_B")]
        public string Financial_Department_Parent_B { get; set; }

        [Display(Name = "Parent B Description")]
        [Column("Financial_Department_Parent_B_Description")]
        public string Financial_Department_Parent_B_Description { get; set; }
            
        [Display(Name = "Parent C")]
        [Column("Financial_Department_Parent_C")]
        public string Financial_Department_Parent_C { get; set; }

        [Display(Name = "Parent C Description")]
        [Column("Financial_Department_Parent_C_Description")]
        public string Financial_Department_Parent_C_Description { get; set; }

        [Display(Name = "Parent D")]
        [Column("Financial_Department_Parent_D")]
        public string Financial_Department_Parent_D { get; set; }

        [Display(Name = "Parent D Description")]
        [Column("Financial_Department_Parent_D_Description")]
        public string Financial_Department_Parent_D_Description { get; set; }

        [Display(Name = "Parent E")]
        [Column("Financial_Department_Parent_E")]
        public string Financial_Department_Parent_E { get; set; }

        [Display(Name = "Parent E Description")]
        [Column("Financial_Department_Parent_E_Description")]
        public string Financial_Department_Parent_E_Description { get; set; }

        [Display(Name = "Parent F")]
        [Column("Financial_Department_Parent_F")]
        public string Financial_Department_Parent_F { get; set; }

        [Display(Name = "Parent F Description")]
        [Column("Financial_Department_Parent_F_Description")]
        public string Financial_Department_Parent_F_Description { get; set; }

        [Key]
        [Required]
        [Display(Name = "Financial Department Code")]
        [Column("Financial_Department_Level_G_Child")]
        public string Financial_Department_Level_G_Child { get; set; }

        [Display(Name = "Financial Department Description")]
        [Column("Financial_Department_Level_G_Description")]
        public string Financial_Department_Level_G_Description { get; set; }

        // Editable fields
        [Required]
        [Display(Name = "Include in AES Reporting?")]
        [Column("Is_AES?")]
        public bool? Is_AES { get; set; }

        [Display(Name = "Is Animal Health?")]
        [Column("Is_Animal_Health?")]
        public bool? IsAnimalHealth { get; set; }

        [Display(Name = "Org R")]
        [Column("OrgR")]
        public string OrgR { get; set; }

        [Display(Name = "Notes")]
        [Column("Notes")]
        public string Notes { get; set; }

        [Display(Name = "BCBS00C Filter By Fund")]
        [Column("BCBS00CFilterByFund")]
        public bool? BCBS00CFilterByFund { get; set; }

        [Display(Name = "BCBS00C Filter By Purpose")]
        [Column("BCBS00CFilterByPurpose")]
        public bool? BCBS00CFilterByPurpose { get; set; }

        [Display(Name = "BCBS00C Filter By AES Faculty Projects")]
        [Column("BCBS00CFilterByAESFacultyProjects")]
        public bool? BCBS00CFilterByAESFacultyProjects { get; set; }
    }
}

