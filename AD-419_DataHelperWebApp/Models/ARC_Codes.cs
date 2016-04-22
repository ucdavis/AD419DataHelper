namespace AD_419_DataHelperWebApp.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ARC_Codes")]
    public partial class ARC_Codes
    {
        [Key]
        [StringLength(6)]
        [Required]
        [Display(Name = "ARC Code")]
        public string ARC_Cd { get; set; }

        [StringLength(40)]
        [Required]
        [Display(Name = "ARC Name")]
        public string ARC_Name { get; set; }

        [StringLength(40)]
        [Display(Name = "OP Function Name")]
        public string OP_Func_Name { get; set; }

        [Column(TypeName = "smalldatetime")]
        [Display(Name = "DaFIS Last Update Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DS_Last_Update_Date { get; set; }

        [Display(Name = "Is AES?")]
        public bool? isAES { get; set; }

        [StringLength(3)]
        [Required]
        [Display(Name = "ARC Category Code")]
        public string ARC_Category_Cd { get; set; }

        [StringLength(3)]
        [Required]
        [Display(Name = "ARC Sub-category Code")]
        public string ARC_Sub_Category_Cd { get; set; }
    }
}
