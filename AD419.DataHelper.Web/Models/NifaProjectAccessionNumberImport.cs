using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AD419.DataHelper.Web.Models
{
    /// <summary>
    /// This class represents a record imported from the REEport financial system, from a list that contains 
    /// all of the projects we will be reporting on.  It is used to validate against the project list that the
    /// Data Helper derived from the All ANR projects list.  
    /// 20221019: Modified to include UCP Emp. ID provided by Shannon
    /// </summary>
    [Table("NifaProjectAccessionNumberImport")]
    public partial class NifaProjectAccessionNumberImport
    {
        [Key]
        public int Id { get; set; }

        [StringLength(255)]
        [Display(Name = "Project Number")]
        public string ProjectNumber { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Accession Number")]
        public string AccessionNumber { get; set; }

        [Required]
        [StringLength(11)]
        [Display(Name = "UCP Employee ID")]
        public string UcpEmployeeId { get; set; }

        [StringLength(255)]
        [Display(Name = "Notes")]
        public string Notes { get; set; }

        [NotMapped]
        public string ShortCode
        {
            get
            {
                if (string.IsNullOrEmpty(ProjectNumber) || ProjectNumber.Length < 9)
                    return null;

                return ProjectNumber.Substring(5, 3);
            }
        }
    }
}
