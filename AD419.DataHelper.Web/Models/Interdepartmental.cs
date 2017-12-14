using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AD419.DataHelper.Web.Models
{
    [Table("InterdepartmentalProjectsImport")]
    public partial class Interdepartmental
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [StringLength(7)]
        public string AccessionNumber { get; set; }

        [StringLength(4)]
        public string OrgR { get; set; }

        public int Year { get; set; }

        // The following properties are set externally in order
        // to display error conditions:
        /// <summary>
        /// This is not required, just makes it easier to identify the
        /// actual project.
        /// </summary>
        [Display(Name = "Project Number")]
        [StringLength(24)]
        [NotMapped]
        public string ProjectNumber { get; set; }

        /// <summary>
        /// Allows us to indicate the following: 
        /// 1. Project is not expired and
        /// 2. Project start is not in the future.
        /// We are basically going to set this based on whether or not
        /// the project is present in the Current AD-419 Project's table, Project. 
        /// </summary>
        [Display(Name = "Is Current AD-419 Project?")]
        [NotMapped]
        public bool IsCurrentAd419Project
        {
            get;
            set;
        }

        [NotMapped]
        [DefaultValue(true)]
        [Display(Name = "Is Valid OrgR?")]
        public bool IsValidOrgR { get; set; }

        [NotMapped]
        [DefaultValue(true)]
        [Display(Name = "Is Present In File?")]
        public bool IsPresentInFile { get; set; }

        [Display(Name = "Notes")]
        [NotMapped]
        public string Message
        {
            get;
            set;
        }
    }
}
