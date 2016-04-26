namespace AD_419_DataHelperWebApp.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ARC_Codes")]
    public partial class ArcCode
    {
        [Key]
        [StringLength(6)]
        [Required]
        [Display(Name = "ARC Code")]
        [Column("ARC_Cd")]
        public string Code { get; set; }

        [StringLength(40)]
        [Required]
        [Display(Name = "ARC Name")]
        [Column("ARC_Name")]
        public string Name { get; set; }

        [StringLength(40)]
        [Display(Name = "OP Function Name")]
        [Column("OP_Func_Name")]
        public string FunctionName { get; set; }

        [Display(Name = "DaFIS Last Update Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Column("DS_Last_Update_Date", TypeName = "smalldatetime")]
        public DateTime? UpdatedDate { get; set; }

        [Display(Name = "Is AES?")]
        [Column("isAES")]
        public bool? IsAES { get; set; }

        [StringLength(3)]
        [Required]
        [Display(Name = "ARC Category Code")]
        [Column("ARC_Category_Cd")]
        public string CategoryCode { get; set; }

        [StringLength(3)]
        [Required]
        [Display(Name = "ARC Sub-category Code")]
        [Column("ARC_Sub_Category_Cd")]
        public string SubCategoryCode { get; set; }
    }
}
