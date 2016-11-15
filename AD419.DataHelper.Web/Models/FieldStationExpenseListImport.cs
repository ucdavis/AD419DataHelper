using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace AD419.DataHelper.Web.Models
{
    [Table("FieldStationExpenseListImport")]
    public partial class FieldStationExpenseListImport
    {
        public FieldStationExpenseListImport() { }

        public FieldStationExpenseListImport(DataRow row)
        {
            ProjectAccessionNum = row["ProjectAccessionNum"].ToString().PadLeft(7, '0');

            FieldStationCharge = Decimal.Parse(row["FieldStationCharge"].ToString());
        }

        [Required]
        [Display(Name = "Project Accession Num")]
        [StringLength(11)]
        public string ProjectAccessionNum { get; set; }

        [Required]
        [Display(Name = "Field Station Charge (Line 22F)")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Range(0.01, int.MaxValue)]
        [Column(TypeName = "money")]

        public decimal? FieldStationCharge { get; set; }

        [Display(Name = "Row Id")]
        public int Id { get; set; }

        // The following properties are set externally in order
        // to display error conditions:
        /// <summary>
        /// This is not required, just makes it easier to identify the
        /// actual project.
        /// </summary>
        [Display(Name = "Project Num")]
        [StringLength(24)]
        public string ProjectNumber { get; set; }

        /// <summary>
        /// Allows us to indicate the following: 
        /// 1. Project is not expired and
        /// 2. Project start is not in the future.
        /// We are basically going to set this based on whether or not
        /// the project is present in the Current AD-419 Project's table, Project. 
        /// </summary>
        
        [Display(Name = "Is Current AD-419 Project?")]
        public bool? IsCurrentAd419Project
        {
            get; set; 
        }
    }
}
