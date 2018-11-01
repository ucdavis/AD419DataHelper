using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AD419.DataHelper.Web.Models
{
    [Table("AD419CurrentProjectListV")]
    public partial class CurrentAd419Project
    {
        [Key]
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

        public string Title { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Organization Name")]
        public string OrganizationName { get; set; }

        [StringLength(4)]
        [Display(Name = "OrgR")]
        public string OrgR { get; set; }

        [StringLength(255)]
        public string Department { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Project Director")]
        public string ProjectDirector { get; set; }

        [StringLength(1024)]
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

        [Display(Name = "Interdepartmental?")]
        public bool IsInterdepartmental { get; set; }

        [NotMapped]
        [Display(Name = "Is UC Davis?")]
        public bool IsUcDavis => true;

        public bool Is204 { get; set; }

        [NotMapped]
        public bool IsExpired => false;

        [NotMapped]
        public string ShortCode
        {
            get
            {
                if (string.IsNullOrEmpty(ProjectNumber) || ProjectNumber.Length < 9)
                    return null;

                return ProjectNumber.Substring(5, 3);
            }
        }
    }
}
