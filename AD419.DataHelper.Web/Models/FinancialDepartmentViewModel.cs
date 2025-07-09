using System.Collections.Generic;

namespace AD419.DataHelper.Web.Models
{
    public class FinancialDepartmentsViewModel : LaborTransactionsForMissingCodesModel
    {
        public List<FinancialDepartment> FinancialDepartments { get; set; }
    }
}