using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace AD419.DataHelper.Web.Models
{
    [Table("CFDANumImport")]
    public partial class CfdaNumberImport
    {
        public CfdaNumberImport() { }
        public CfdaNumberImport(DataRow row)
        {
            Number = row["CFDANum"].ToString();
            ProgramTitle = row["ProgramTitle"].ToString();
        }

        public int Id { get; set; }

        [Required]
        [Display(Name = "CFDA Number")]
        [StringLength(10)]
        [Column("CFDANum")]
        public string Number { get; set; }

        [Required]
        [Display(Name = "Program Title")]
        [StringLength(300)]
        public string ProgramTitle { get; set; }
    }
}
