using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace AD419.DataHelper.Web.Models
{
    [Table("CFDANumImport")]
    public partial class CfdaNumberImport
    {
        const string USDA = "Department of Agriculture";
        const string AD = "AGRICULTURE, DEPARTMENT OF"; // EndsWith
        const string NIFA = "National Institute of Food and Agriculture"; // StartsWith
        const string DHHS = "Department of Health and Human Services";
        const string HHSD = "HEALTH AND HUMAN SERVICES, DEPARTMENT OF"; // EndsWith
        const string NIH = "National Institutes of Health"; // StartsWith

        public CfdaNumberImport() { }
        public CfdaNumberImport(DataRow row)
        {
            Number = row["Program Number"].ToString();
            ProgramTitle = row["Program Title"].ToString();
            AgencyOffice = row["Federal Agency (030)"].ToString();
            Code = GetCode(AgencyOffice);
        }

        public int Id { get; set; }

        [Required]
        [Display(Name = "CFDA Number")]
        [StringLength(10)]
        [Column("CFDANum")]
        public string Number { get; set; }

        [Required]
        [Display(Name = "Program Title")]
        [StringLength(300)]
        public string ProgramTitle { get; set; }

        [Required]
        [Display(Name = "Agency/Office")]
        [StringLength(2048)]
        public string AgencyOffice { get; set; }

        [Display(Name = "Code")]
        [StringLength(10)]
        public string Code { get; set; }

        /// <summary>
        /// Sets the Code based of the Agency/Office for NIFA and NIH.
        /// </summary>
        /// <param name="agencyOffice"></param>
        private string GetCode(string agencyOffice)
        {
            if (string.IsNullOrEmpty(agencyOffice))
                return null;

            if (
                (agencyOffice.StartsWith(USDA, StringComparison.OrdinalIgnoreCase) && agencyOffice.IndexOf(NIFA, StringComparison.OrdinalIgnoreCase) > -1) || // Pre-SAMS
                (agencyOffice.EndsWith(AD, StringComparison.OrdinalIgnoreCase) && agencyOffice.StartsWith(NIFA, StringComparison.OrdinalIgnoreCase))          // Post-SAMS
            )
            {
                return "NIFA";
            }
            else if (
                (agencyOffice.StartsWith(DHHS, StringComparison.OrdinalIgnoreCase) && agencyOffice.IndexOf(NIH, StringComparison.OrdinalIgnoreCase) > -1) || // Pre-SAMS
                (agencyOffice.EndsWith(HHSD, StringComparison.OrdinalIgnoreCase) && agencyOffice.StartsWith(NIH, StringComparison.OrdinalIgnoreCase))        // Post-SAMS
            )
            {
                return "NIH";
            }

            return null;
        }
    }
}
