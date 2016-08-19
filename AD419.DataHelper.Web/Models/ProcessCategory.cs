using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AD419.DataHelper.Web.Models
{
    [Table("ProcessCategory")]
    public class ProcessCategory
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(200)]
        public string Name { get; set; }

        [DisplayName("Is Completed?")]
        public bool IsCompleted { get; set; }

        public string Notes { get; set; }

        [MaxLength(200)]
        public string StoredProcedureName { get; set; }
    }
}