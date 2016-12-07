using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace AD419.DataHelper.Web.Models
{
    [Table("CesListImport")]
    public partial class CesListImport
    {
        public CesListImport()
        {
        }
        
        public CesListImport(DataRow row)
        {
            PI = row["PI"].ToString();
            DeptLevelOrg = row["DeptLevelOrg"].ToString();
            EmployeeId = row["EmployeeID"].ToString();
            ProjectAccessionNum = row["ProjectAccessionNum"].ToString().PadLeft(7, '0'); 
            ProjectNumber = row["ProjectNumber"].ToString();
            PercentCeEffort = decimal.Parse(row["PercentCeEffort"].ToString());
            FullAnnualPayRate = decimal.Parse(row["FullAnnualPayRate"].ToString());
            TitleCode = row["TitleCode"].ToString();
            Chart = row["Chart"].ToString();
            Account = row["Account"].ToString();
            SubAccount = row["SubAccount"].ToString();
        }

        [Required]
        [StringLength(200)]
        public string PI { get; set; }

        [Required]
        [StringLength(4)]
        [Display(Name = "Dept Level Org")]
        public string DeptLevelOrg { get; set; }

        [Required]
        [Display(Name = "Employee ID")]
        [StringLength(10)]
        public string EmployeeId { get; set; }

        [Required]
        [StringLength(7)]
        [Display(Name = "Project Accession #")]
        public string ProjectAccessionNum { get; set; }

        [StringLength(20)]
        [Display(Name = "Project Number")]
        public string ProjectNumber { get; set; }

        [Required]
        [Display(Name = "% CE Effort")]
        [DisplayFormat(DataFormatString = "{0:P}")]
        public decimal? PercentCeEffort { get; set; }

        [Required]
        [Display(Name = "Full Annual PayRate")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal? FullAnnualPayRate { get; set; }

        [Required]
        [StringLength(4)]
        [Display(Name = "Title Code")]
        public string TitleCode { get; set; }

        public decimal? FTE {
            get { return PercentCeEffort; }
            set { value = PercentCeEffort;  }
        }

        [Required]
        [StringLength(2)]
        public string Chart { get; set; }

        [Required]
        [StringLength(7)]
        public string Account { get; set; }

        [StringLength(5)]
        [Display(Name = "Sub-account")]
        public string SubAccount { get; set; }

        [Display(Name = "Estimated Chart L Expenses")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal? EstimatedCeExpenses
        {
            get { return FullAnnualPayRate * PercentCeEffort;  }
            set { value = FullAnnualPayRate * PercentCeEffort; }
        }

        public int Id { get; set; }

        [Display(Name = "Is Current AD-419 Project?")]
        [NotMapped]
        public bool IsCurrentAd419Project
        {
            get;
            set;
        }

        [Display(Name = "Notes")]
        [NotMapped]
        public string Message
        {
            get;
            set;
        }
    }
}
