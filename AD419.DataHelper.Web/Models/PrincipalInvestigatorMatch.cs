using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AD419.DataHelper.Web.Models
{
    public class PrincipalInvestigatorMatch
    {
        [Key]
        public int Id { get; set; }

        [Column("OrgR")]
        public string Organization { get; set; }

        [Column("PI")]
        public string Name { get; set; }

        [Column("EID")]
        public string EmployeeId { get; set; }

        [Column("PI_Match")]
        public string MatchName { get; set; }

        [DisplayName("Is Prorated?")]
        public bool? IsProrated { get; set; }
    }
}