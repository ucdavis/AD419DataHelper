using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AD419.DataHelper.Web.Models;
using AD419.DataHelper.Web.ViewModels;

namespace AD419.DataHelper.Web.Controllers
{
    public class ExpenseOrgMappingsController : SuperController
    {
        // GET: ExpenseOrgMappings
        public ActionResult Index()
        {
            var model = new ExpenseOrgMappingsViewModel()
            {
                ExpenseOrgMappings = DbContext.ExpenseOrgMappings.ToList(),
                UnknownDepartments = DbContext.GetAccountDetailsForNullOrUnknownDepartments().ToList()
            };
            return View(model);
        }

        // GET: ExpenseOrgMappings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpenseOrgMapping expenseOrgMapping = DbContext.ExpenseOrgMappings.Find(id);
            if (expenseOrgMapping == null)
            {
                return HttpNotFound();
            }

            var unknownDepartmentAccountDetails = DbContext.GetExpenseDeptToAd419DeptRemappedAccountDetails(expenseOrgMapping.Chart, expenseOrgMapping.ExpenseOrgR, expenseOrgMapping.ExpenseOrg).ToList();
            var expenseOrgMappingViewModel = new ExpenseOrgMappingsEditViewModel(expenseOrgMapping, null, unknownDepartmentAccountDetails);
            return View(expenseOrgMappingViewModel);

            return View(expenseOrgMapping);
        }

        // GET: ExpenseOrgMappings/Create
        public ActionResult Create(string Chart, string OrgR, string Org, string SuggestedOrgR)
        {
            var expenseOrgMapping = new ExpenseOrgMapping() { Chart = Chart, ExpenseOrgR = OrgR, ExpenseOrg = Org, AD419OrgR = SuggestedOrgR };
            var unknownDepartmentAccountDetails = DbContext.GetAccountDetailsForNullOrUnknownDepartments().Where(u => u.Chart.Equals(Chart)
                && u.OrgR.Equals(OrgR) && u.Org.Equals(Org)).ToList();
            var expenseOrgMappingViewModel = new ExpenseOrgMappingsEditViewModel(
                    expenseOrgMapping, 
                    DbContext.ReportingOrganizations.ToList(), 
                    unknownDepartmentAccountDetails);
            return View(expenseOrgMappingViewModel);
        }

        // POST: ExpenseOrgMappings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Chart,ExpenseOrgR,ExpenseOrg,AD419OrgR")] ExpenseOrgMapping expenseOrgMapping)
        {
            if (ModelState.IsValid)
            {
                DbContext.ExpenseOrgMappings.Add(expenseOrgMapping);
                DbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(expenseOrgMapping);
        }

        // GET: ExpenseOrgMappings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpenseOrgMapping expenseOrgMapping = DbContext.ExpenseOrgMappings.Find(id);
            if (expenseOrgMapping == null)
            {
                return HttpNotFound();
            }

            var unknownDepartmentAccountDetails = DbContext.GetExpenseDeptToAd419DeptRemappedAccountDetails(expenseOrgMapping.Chart, expenseOrgMapping.ExpenseOrgR, expenseOrgMapping.ExpenseOrg).ToList();
            var expenseOrgMappingViewModel = new ExpenseOrgMappingsEditViewModel(expenseOrgMapping, DbContext.ReportingOrganizations.Where(s => s.IsActive).ToList(), unknownDepartmentAccountDetails);
            return View(expenseOrgMappingViewModel);
        }

        // POST: ExpenseOrgMappings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Chart,ExpenseOrgR,ExpenseOrg,AD419OrgR")] ExpenseOrgMapping expenseOrgMapping)
        {
            if (ModelState.IsValid)
            {
                DbContext.Entry(expenseOrgMapping).State = EntityState.Modified;
                DbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(expenseOrgMapping);
        }

        // GET: ExpenseOrgMappings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpenseOrgMapping expenseOrgMapping = DbContext.ExpenseOrgMappings.Find(id);
            if (expenseOrgMapping == null)
            {
                return HttpNotFound();
            }
            return View(expenseOrgMapping);
        }

        // POST: ExpenseOrgMappings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ExpenseOrgMapping expenseOrgMapping = DbContext.ExpenseOrgMappings.Find(id);
            DbContext.ExpenseOrgMappings.Remove(expenseOrgMapping);
            DbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                DbContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
