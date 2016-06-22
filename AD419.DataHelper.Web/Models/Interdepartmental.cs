using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AD419.DataHelper.Web.Models
{
    [Table("Interdepartmental")]
    public partial class Interdepartmental
    {
        [Key]
        [StringLength(7)]
        public string AccessionNumber { get; set; }

        [Key]
        [StringLength(4)]
        public string OrgR { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Year { get; set; }

        public string ProjectNumber { get; set; }
    }
}
