using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AD419.DataHelper.Web.Models
{
    [Table("InterdepartmentalProjectsImport")]
    public partial class Interdepartmental
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [StringLength(7)]
        public string AccessionNumber { get; set; }

        [StringLength(4)]
        public string OrgR { get; set; }

        public int Year { get; set; }
    }
}
