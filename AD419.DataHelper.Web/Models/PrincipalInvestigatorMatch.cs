using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AD419.DataHelper.Web.Models
{
    [Table("PI_Match")]
    public class PrincipalInvestigatorMatch
    {
        [Key]
        public int Id { get; set; }

        [Column("OrgR")]
        public string Organization { get; set; }

        [Column("PI")]
        public string Name { get; set; }

        [Column("EID")]
        [DisplayName("Employee ID")]
        public string EmployeeId { get; set; }

        [Column("PI_Match")]
        [DisplayName("Match Name")]
        public string MatchName { get; set; }

        [DisplayName("Is Prorated?")]
        public bool? IsProrated { get; set; }
    }
}