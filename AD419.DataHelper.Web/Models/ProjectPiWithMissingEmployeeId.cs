using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AD419.DataHelper.Web.Models
{
    public class ProjectPiWithMissingEmployeeId
    {
        [Column(Order = 0), Key]
        [DisplayName("OrgR")]
        public string OrgR { get; set; }
       
        [Column("Inv1", Order = 1), Key]
        [DisplayName("Project PI")]
        public string Inv1 { get; set; }

        private string _employeeId; 
        [DisplayName("Employee ID")]
        public string EmployeeId
        {
            get { return string.IsNullOrWhiteSpace(_employeeId) ? "Employee ID not found" : _employeeId; }
            set { _employeeId = value; } 
        }

        [DisplayName("Accession Number")]
        public string Accession { get; set; }

        [DisplayName("Project Number")]
        public string Project { get; set; }

        [Column("Project Title")]
        public string Title { get; set; }
    }
}