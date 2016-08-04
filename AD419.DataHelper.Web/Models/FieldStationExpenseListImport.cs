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
            //var fieldStationChargeString = row["FieldStationCharge"].ToString();
            //decimal fieldStationChargeOut = 0;
            //Decimal.TryParse(fieldStationChargeString, out fieldStationChargeOut);
            //FieldStationCharge = fieldStationChargeOut;
        }

        [Required]
        [Display(Name = "Project Accession Num")]
        [StringLength(11)]
        public string ProjectAccessionNum { get; set; }

        [Required]
        [Display(Name = "Field Station Charge (Line 22F)")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Column(TypeName = "money")]
        public decimal? FieldStationCharge { get; set; }

        [Display(Name = "Row Id")]
        public int Id { get; set; }
    }
}
