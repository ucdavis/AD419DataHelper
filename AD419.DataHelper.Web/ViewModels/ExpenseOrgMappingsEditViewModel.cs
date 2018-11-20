using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AD419.DataHelper.Web.Models;

namespace AD419.DataHelper.Web.ViewModels
{
    public class ExpenseOrgMappingsEditViewModel
    {
        public ExpenseOrgMappingsEditViewModel(ExpenseOrgMapping expenseOrgMapping, List<ReportingOrganization> reportingOrgs)
        {
            ExpenseOrgMapping = expenseOrgMapping;

            // Initialize reporting orgs select list:
            var reportingOrgsSelectList = new List<SelectListItem>();

            foreach (var reportingOrg in reportingOrgs.ToList().OrderBy(r => r.Code))
            {
                reportingOrgsSelectList.Add(new SelectListItem()
                {
                    Text = string.Format("{0} ({1})", reportingOrg.ShortName, reportingOrg.Code),
                    Value = reportingOrg.Code,
                    Selected =
                    (!string.IsNullOrEmpty(ExpenseOrgMapping.AD419OrgR) &&
                     ExpenseOrgMapping.AD419OrgR.Equals(reportingOrg.Code))
                });
            }
            ReportingOrgsSelectList = reportingOrgsSelectList;
        }

        public ExpenseOrgMapping ExpenseOrgMapping { get; set; }

        public List<SelectListItem> ReportingOrgsSelectList { get; set; }
    }
}