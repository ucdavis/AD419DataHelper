using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Net;
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
                FinancialDepartments = DbContext.FinancialDepartments
                    .OrderBy(f => f.Is_AES.HasValue)  // Nulls first (false sorts before true)
                    .ThenBy(f => f.Is_AES)            // Then by actual value
                    .ThenBy(f => f.Financial_Department_Level_G_Child)
                    .ToList(),
                LaborTransactions = DbContext.GetLaborTransactions((int)LaborTransactionsOptions.FinancialDepartments, 2025).ToList(),
                CodeTypeName = "Financial Departments"
            };
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var dept = DbContext.FinancialDepartments.Find(id);

            if (dept == null)
            {
                return HttpNotFound();
            }

            return View(dept);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Financial_Department_Level_G_Child,Is_AES,IsAnimalHealth,OrgR,Notes,BCBS00CFilterByFund,BCBS00CFilterByPurpose,BCBS00CFilterByAESFacultyProjects")] FinancialDepartment model)
        {
            if (!ModelState.IsValid) return View(model);

            var existing = DbContext.FinancialDepartments.Find(model.Financial_Department_Level_G_Child);
            if (existing == null) return HttpNotFound();

            existing.Is_AES = model.Is_AES;
            existing.IsAnimalHealth = model.IsAnimalHealth;
            existing.OrgR = model.OrgR;
            existing.Notes = model.Notes;

            DbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}