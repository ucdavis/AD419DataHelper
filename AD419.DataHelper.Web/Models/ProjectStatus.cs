using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AD419.DataHelper.Web.Models
{
     [Table("ProjectStatus")]
    public class ProjectStatus
    {
         [Key]
         public int Id { get; set; }

         public string Status { get; set; }

         [DisplayName("Is Excluded?")]
         public bool IsExcluded { get; set; }
    }
}