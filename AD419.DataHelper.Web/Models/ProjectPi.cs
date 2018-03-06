using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using Newtonsoft.Json;

namespace AD419.DataHelper.Web.Models
{
    [Table("ProjectPI")]
    public class ProjectPi
    {
        [Column(Order = 0), Key]
        [DisplayName("OrgR")]
        public string OrgR { get; set; }
       
        [Column("Inv1", Order = 1), Key]
        [DisplayName("Project PI")]
        public string Inv1 { get; set; }

        [DisplayName("PI")]
        public string PI { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [DisplayName("First Initial")]
        public string FirstInitial { get; set; }
       
        [DisplayName("Employee ID")]
        [Required]
        [MinLength(9)]
        public string EmployeeId { get; set; }

        [DisplayName("Is Existing Record?")]
        public bool? IsExistingRecord { get; set; }

        [DisplayName("Last Update Date")]
        public DateTime? LastUpdateDate { get; set; }
    }
}