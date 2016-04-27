namespace AD_419_DataHelperWebApp.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("AllProjectsImport")]
    public partial class AllProjectImport
    {
        [Key]
        [Column("idProject")]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Accession Number")]
        public string AccessionNumber { get; set; }

        [StringLength(255)]
        [Display(Name = "Project Number")]
        public string ProjectNumber { get; set; }

        [StringLength(255)]
        [Display(Name = "Proposal Number")]
        public string ProposalNumber { get; set; }

        [StringLength(255)]
        [Display(Name = "Award Number")]
        public string AwardNumber { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Organization Name")]
        public string OrganizationName { get; set; }

        [Required]
        [StringLength(255)]
        public string Department { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Project Director")]
        public string ProjectDirector { get; set; }

        [StringLength(255)]
        [Display(Name = "Co-Project Directors")]
        public string CoProjectDirectors { get; set; }

        [StringLength(255)]
        [Display(Name = "Funding Source")]
        public string FundingSource { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ProjectStartDate { get; set; }

        [Required]
        [Display(Name = "End Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ProjectEndDate { get; set; }

        [StringLength(255)]
        [Display(Name = "Project Status")]
        public string ProjectStatus { get; set; }
    }
}
