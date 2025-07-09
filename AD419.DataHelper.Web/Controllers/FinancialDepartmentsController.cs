using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using AD419.DataHelper.Web.Models;

namespace AD419.DataHelper.Web.Controllers
{
    public class FinancialDepartmentsController : SuperController
    {
        // GET: FinancialDepartments
        public ActionResult Index()
        {
            var model = new FinancialDepartmentsViewModel
            {
                 FinancialDepartments = DbContext.Database.SqlQuery<FinancialDepartment>(
                    "SELECT Financial_Department_Level_G_Child AS FinancialDepartmentCode, " +
                    "Financial_Department_Level_G_Description AS Description, " +
                    "[Is_AES?] " +
                    "FROM [caes-elzar].[AggieEnterprise].[dbo].[IsAESFinancialDept]"
                ).ToList(),
                LaborTransactions = DbContext.GetLaborTransactions((int)LaborTransactionsOptions.FinancialDepartments, 2024).ToList(),
                CodeTypeName = "Financial Departments"
            };

            return View(model);
        }

        // Additional CRUD methods will go here later...
    }
}