using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AD419.DataHelper.Web.Models
{
    public class UnknownDepartmentAccountDetail
    {
        public string Chart { get; set; }

        public string OrgR { get; set; }

        [Display(Name = "Suggested OrgR")]
        public string SuggestedOrgR { get; set; }

        public string Org { get; set; }
       
        public string Account { get; set; }

        [Display(Name = "Account Name")]
        public string AccountName { get; set; }

        [Display(Name = "ARC Name")]
        public string ARCName { get; set; }

        public string School { get; set; }

        public string Department { get; set; }

        [Display(Name = "Manager Name")]
        public string MgrName { get; set; }

        [Display(Name = "PI Name")]
        public string PrincipalInvestigatorName { get; set; }

        [Display(Name = "Employee ID")]
        public string EmployeeId { get; set; }

        public string Purpose { get; set; }

       
    }
}
