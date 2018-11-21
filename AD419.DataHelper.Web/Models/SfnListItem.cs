using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AD419.DataHelper.Web.Models
{
    [Table("SfnDropDownListV")]
    public partial class SfnListItem
    {
        [Key]
        [Display(Name = "SFN")]
        public string Sfn { get; set; }

        public string Description { get; set; }
    }
}