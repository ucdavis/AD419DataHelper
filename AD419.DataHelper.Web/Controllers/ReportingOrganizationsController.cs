using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AD419.DataHelper.Web.Models;

namespace AD419.DataHelper.Web.Controllers
{
    public class ReportingOrganizationsController : SuperController
    {
        // GET: ReportingOrganizations
        public ActionResult Index()
        {
            return View(DbContext.ReportingOrganizations.ToList());
        }

        // GET: ReportingOrganizations/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var reportingOrganization = DbContext.ReportingOrganizations.Find(id);
            if (reportingOrganization == null)
            {
                return HttpNotFound();
            }

            return View(reportingOrganization);
        }

        // GET: ReportingOrganizations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ReportingOrganizations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReportingOrganization reportingOrganization)
        {
            if (!ModelState.IsValid) return View(reportingOrganization);

            DbContext.ReportingOrganizations.Add(reportingOrganization);
            DbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: ReportingOrganizations/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var reportingOrganization = DbContext.ReportingOrganizations.Find(id);
            if (reportingOrganization == null)
            {
                return HttpNotFound();
            }

            return View(reportingOrganization);
        }

        // POST: ReportingOrganizations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ReportingOrganization reportingOrganization)
        {
            if (!ModelState.IsValid) return View(reportingOrganization);

            DbContext.Entry(reportingOrganization).State = EntityState.Modified;
            DbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: ReportingOrganizations/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var reportingOrganization = DbContext.ReportingOrganizations.Find(id);
            if (reportingOrganization == null)
            {
                return HttpNotFound();
            }

            return View(reportingOrganization);
        }

        // POST: ReportingOrganizations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var reportingOrganization = DbContext.ReportingOrganizations.Find(id);
            DbContext.ReportingOrganizations.Remove(reportingOrganization);
            DbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
