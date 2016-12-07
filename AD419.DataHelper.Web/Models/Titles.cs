using System.ComponentModel;

namespace AD419.DataHelper.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Titles")]
    public partial class Titles
    {
        [Key]
        [StringLength(4)]
        [Required]
        [DisplayName("Title Code")]
        public string TitleCode { get; set; }

        [StringLength(150)]
        [DisplayName("Title Name")]
        public string Name { get; set; }

        [StringLength(30)]
        [DisplayName("Abbreviated Name")]
        public string AbbreviatedName { get; set; }

        [StringLength(1)]
        [DisplayName("Personnel Program Code")]
        public string PersonnelProgramCode { get; set; }

        [StringLength(2)]
        [DisplayName("Unit Code")]
        public string UnitCode { get; set; }

        [StringLength(3)]
        [DisplayName("Title Group")]
        public string TitleGroup { get; set; }

        [StringLength(1)]
        [DisplayName("Overtime Exemption Code")]
        public string OvertimeExemptionCode { get; set; }

        [StringLength(3)]
        [DisplayName("CTO Occupation Subgroup Code")]
        public string CTOOccupationSubgroupCode { get; set; }

        [StringLength(1)]
        [DisplayName("Federal Occupation Code (F.O.C.)")]
        public string FederalOccupationCode { get; set; }

        [StringLength(2)]
        [DisplayName("F.O.C. Subcategory Code")]
        public string FOCSubcategoryCode { get; set; }

        [StringLength(3)]
        [DisplayName("Link Title Group Code")]
        public string LinkTitleGroupCode { get; set; }

        [StringLength(1)]
        [DisplayName("EE06 Category Code")]
        public string EE06CategoryCode { get; set; }

        [StringLength(2)]
        [DisplayName("Staff Type")]
        public string StaffType { get; set; }

        [DisplayName("Effective Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EffectiveDate { get; set; }

        [DisplayName("Update Timestamp")]
        public DateTime? UpdateTimestamp { get; set; }
    }
}
