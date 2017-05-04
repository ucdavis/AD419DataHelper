using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq.Expressions;

namespace AD419.DataHelper.Web.Models
{
    [Table("TitleCodesSelfCertify")]
    public partial class SelfCertifyingTitleCode
    {
        public SelfCertifyingTitleCode() { }
        public SelfCertifyingTitleCode(DataRow row)
        {
            foreach (DataColumn column in row.Table.Columns)
            {
                if (column.ColumnName != null)
                {
                    var columnName = column.ColumnName.Replace('_', ' ').Trim();
                    if (columnName.Equals("TITLE CODE"))
                    {
                        TitleCode = row.ItemArray[column.Ordinal].ToString().Trim();
                    }
                    else if (columnName.Equals("TITLE CODE NAME"))
                    {
                        TitleName = row.ItemArray[column.Ordinal].ToString().Trim();
                    }
                    else if (columnName.Equals("CLASS TITLE OUTLINE"))
                    {
                        ClassTitleOutline = row.ItemArray[column.Ordinal].ToString().Trim();
                    }
                }
            }
        }

        public int Id { get; set; }

        [Required]
        [Display(Name = "Title Code")]
        [StringLength(4)]
        public string TitleCode { get; set; }

        [Required]
        [Display(Name = "Title Name")]
        [Column("Name")]
        [StringLength(150)]
        public string TitleName { get; set; }

        [Required]
        [Display(Name = "Class Title Outline (CTO)")]
        [StringLength(3)]
        public string ClassTitleOutline { get; set; }
    }
}
