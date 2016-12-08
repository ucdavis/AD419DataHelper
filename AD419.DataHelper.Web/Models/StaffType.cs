using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AD419.DataHelper.Web.Models
{
    [Table("staff_type")]
    public class StaffType
    {
        [Key]
        [Column("Staff_Type_Code")]
        [Required]
        [DisplayName("Staff Type Code")]
        public string StaffTypeCode { get; set; }

        [Column("Staff_Type_Name")]
        [Required]
        [DisplayName("Staff Type Name")]
        public string StaffTypeName { get; set; }

        [Column("AD419_Line_Num")]
        [Required]
        [DisplayName("AD419 Line Num")]
        public string Ad419LineNum { get; set; }

        [Column("Staff_Type_Short_Name")]
        [Required]
        [DisplayName("Staff_Type_Short_Name")]
        public string StaffTypeShortName { get; set; }
    }
}