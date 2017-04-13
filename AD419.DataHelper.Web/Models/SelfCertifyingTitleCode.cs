using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace AD419.DataHelper.Web.Models
{
    [Table("TitleCodesSelfCertify")]
    public partial class SelfCertifyingTitleCode
    {
        public SelfCertifyingTitleCode() { }
        public SelfCertifyingTitleCode(DataRow row)
        {
            TitleCode = row["TITLE CODE"].ToString();
            TitleName = row["TITLE CODE NAME"].ToString();
            ClassTitleOutline = row["CLASS TITLE OUTLINE"].ToString();
        }

        public int Id { get; set; }

        [Required]
        [Display(Name = "Title Code")]
        [StringLength(4)]
        public string TitleCode { get; set; }

        [Required]
        [Display(Name = "Title Name")]
        [StringLength(150)]
        public string TitleName { get; set; }

        [Required]
        [Display(Name = "Class Title Outline (CTO)")]
        [StringLength(3)]
        public string ClassTitleOutline { get; set; }
    }
}
