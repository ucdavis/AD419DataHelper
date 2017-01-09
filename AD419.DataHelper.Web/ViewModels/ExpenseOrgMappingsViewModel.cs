using System.Collections.Generic;
using AD419.DataHelper.Web.Models;

namespace AD419.DataHelper.Web.ViewModels
{
    public class ExpenseOrgMappingsViewModel
    {
        public List<UnknownDepartmentAccountDetail> UnknownDepartments { get; set; }

        public List<ExpenseOrgMapping> ExpenseOrgMappings { get; set; }
    }
}