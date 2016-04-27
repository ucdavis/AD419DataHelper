using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AD419.DataHelper.Web.Models
{
    public partial class AllProject
    {
        [Required]
        [StringLength(7)]
        [Display(Name = "Project Accession Num")]
        public string Accession { get; set; }

        [Required]
        [StringLength(24)]
        public string Project { get; set; }

        [Display(Name = "Is Interdepartmental?")]
        public bool? isInterdepartmental { get; set; }

        [Display(Name = "Is Valid?")]
        public bool? isValid { get; set; }

        [Display(Name = "Begin Date")]
        [Column(TypeName = "datetime2")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? BeginDate { get; set; }

        [Display(Name = "End Date")]
        [Column(TypeName = "datetime2")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? TermDate { get; set; }

        [StringLength(1)]
        [Display(Name = "Project Type Code")]
        public string ProjTypeCd { get; set; }

        [StringLength(9)]
        [Display(Name = "Regional Project Num")]
        public string RegionalProjNum { get; set; }

        [StringLength(4)]
        [Display(Name = "CSREES Dept ID")]
        public string CRIS_DeptID { get; set; }

        [StringLength(50)]
        [Display(Name = "Coop-Departments")]
        public string CoopDepts { get; set; }

        [StringLength(20)]
        [Display(Name = "CSREES Contract Num")]
        public string CSREES_ContractNo { get; set; }

        [StringLength(1)]
        [Display(Name = "Status Code")]
        public string StatusCd { get; set; }

        [StringLength(200)]
        public string Title { get; set; }

        [Column(TypeName = "datetime2")]
        [Display(Name = "Update Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? UpdateDate { get; set; }

        [StringLength(30)]
        [Display(Name = "PI")]
        public string inv1 { get; set; }

        [StringLength(30)]
        [Display(Name = "Co-PI 2")]
        public string inv2 { get; set; }

        [StringLength(30)]
        [Display(Name = "Co-PI 3")]
        public string inv3 { get; set; }

        [StringLength(30)]
        [Display(Name = "Co-PI 4")]
        public string inv4 { get; set; }

        [StringLength(30)]
        [Display(Name = "Co-PI 5")]
        public string inv5 { get; set; }

        [StringLength(30)]
        [Display(Name = "Co-PI/PI 6")]
        public string inv6 { get; set; }

        [Key]
        public int idProject { get; set; }

        [Display(Name = "Is Current AD-419 Project?")]
        public bool? IsCurrentAD419Project { get; set; }
    }
}
