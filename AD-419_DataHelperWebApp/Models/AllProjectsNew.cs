namespace AD_419_DataHelperWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AllProjectsNew
    {
        [Required]
        [StringLength(7)]
        [Display(Name = "Accession Number")]
        public string AccessionNumber { get; set; }

        [Required]
        [StringLength(24)]
        [Display(Name = "Project Number")]
        public string ProjectNumber { get; set; }

        [StringLength(50)]
        [Display(Name = "Proposal Number")]
        public string ProposalNumber { get; set; }

        [StringLength(50)]
        [Display(Name = "Award Number")]
        public string AwardNumber { get; set; }

        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Organization Name")]
        public string OrganizationName { get; set; }

        [Required]
        [StringLength(4)]
        [Display(Name = "DaFIS Org Code")]
        public string OrgR { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Department Name")]
        public string Department { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Project Director")]
        public string ProjectDirector { get; set; }

        [StringLength(100)]
        [Display(Name = "Co-Project Directors")]
        public string CoProjectDirectors { get; set; }

        [StringLength(100)]
        [Display(Name = "Funding Source")]
        public string FundingSource { get; set; }

        [Display(Name = "Project Start Date")]
        [Column(TypeName = "datetime2")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ProjectStartDate { get; set; }

        [Display(Name = "Project End Date")]
        [Column(TypeName = "datetime2")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ProjectEndDate { get; set; }

        [StringLength(50)]
        [Display(Name = "Project Status")]
        public string ProjectStatus { get; set; }

        [Display(Name = "Is Interdepartmental?")]
        public bool? isInterdepartmental { get; set; }

        [Display(Name = "Is Active?")]
        public bool? IsActive { get; set; }

        [Key]
        public int idProject { get; set; }
    }
}
