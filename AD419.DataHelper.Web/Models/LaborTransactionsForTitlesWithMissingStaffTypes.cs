using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace AD419.DataHelper.Web.Models
{
    public class LaborTransactionsForTitlesWithMissingStaffTypes
    {
        [DisplayName("Annual Report Code (ARC)")]
        public string AnnualReportCode { get; set; }

        [Column("ARC_Name")]
        [DisplayName("ARC Name")]
        public string ArcName { get; set; }

        public string OrgR { get; set; }

        [DisplayName("Title Code")]
        public string TitleCd { get; set; }

        [DisplayName("Title Name")]
        public string TitleName { get; set; }

        [DisplayName("Employee Name")]
        public string EmployeeName { get; set; }

        [DisplayName("Employee ID")]
        public string EmployeeId { get; set; }

        public string Chart { get; set; }

        public string Org { get; set; }

        public string Account { get; set; }

        [DisplayName("Sub-Account")]
        public string SubAccount { get; set; }

        [DisplayName("Principal Investigator")]
        public string PrincipalInvestigator { get; set; }

        public decimal Amount { get; set; }

        public decimal FTE { get; set; }
    }
}