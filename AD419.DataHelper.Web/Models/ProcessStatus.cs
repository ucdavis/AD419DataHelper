using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AD419.DataHelper.Web.Models
{
    [Table("ProcessStatus")]
    public class ProcessStatus
    {
        [Key]
        public int Id { get; set; }

        public int SequenceOrder { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [DisplayName("Is Completed?")]
        public bool IsCompleted { get; set; }

        public string Notes { get; set; }

        public bool? NoProcessingRequired { get; set; }

        public string Hyperlink { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public ProcessCategory Category { get; set; }
    }

    // For now we'll hardcode the process statuses here so we can look them up in the DB.
    public enum ProcessStatuses
    {
        ImportProjects = 2,
        ImportCurrentAd419Projects = 3,
        ImportCfdaNumbers = 4,
        ImportCeSpecialists = 5,
        ImportFieldStationExpenses = 6,
        ImportSelfCertifyingTitleCodes = 7
    }
}