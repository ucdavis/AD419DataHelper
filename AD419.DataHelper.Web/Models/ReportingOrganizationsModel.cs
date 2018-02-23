using System.Collections.Generic;

namespace AD419.DataHelper.Web.Models
{
    public class ReportingOrganizationsModel
    {
        public List<ReportingOrganization> ReportingOrganizations { get; set; }
        public List<UnknownDepartment> UnknownDepartments { get; set; }
    }
}