using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AD419.DataHelper.Web.Models
{
    [Table("PI_Names")]
    public class PrincipalInvestigator
    {
        [Column("PI")]
        public string Name { get; set; }

        [Column("PI_Last_First")]
        [DisplayName("Last, First")]
        public string Name2 { get; set; }

        [Key]
        [Column("EID")]
        [DisplayName("Employee ID")]
        public string EmployeeId { get; set; }

        [DisplayName("Employee Name")]
        public string EmployeeName { get; set; }

        [Column("OrgR")]
        public string Organization { get; set; }
    }
}