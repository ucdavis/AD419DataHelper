using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AD419.DataHelper.Web.Models
{
    [Table("ProcessCategory")]
    public class ProcessCategory
    {
        public ProcessCategory()
        {
            Statuses = new List<ProcessStatus>();
        }

        [Key]
        public int Id { get; set; }

        public int SequenceOrder { get; set; }

        [MaxLength(200)]
        public string Name { get; set; }

        [DisplayName("Is Completed?")]
        public bool IsCompleted { get; set; }

        public string Notes { get; set; }

        public string StoredProcedureName { get; set; }

        public ICollection<ProcessStatus> Statuses { get; set; }
    }
}