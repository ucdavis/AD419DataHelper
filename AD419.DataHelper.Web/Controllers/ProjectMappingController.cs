using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AD419.DataHelper.Web.Models;
using Microsoft.Ajax.Utilities;

namespace AD419.DataHelper.Web.Controllers
{
    public class ProjectMappingController : SuperController
    {
        // GET: FfySfnEntities
        public ActionResult Index()
        {
            var entries = DbContext.FfySfnEntriesWithAccounts.ToList();
            return View(entries);
        }

        // GET: FfySfnEntities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var entry = DbContext.FfySfnEntries.Find(id);
            if (entry == null)
                return HttpNotFound();

            return View(entry);
        }

        // GET: FfySfnEntities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FfySfnEntities/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FfySfnEntry ffySfnEntry)
        {
            if (!ModelState.IsValid) return View(ffySfnEntry);

            DbContext.FfySfnEntries.Add(ffySfnEntry);
            DbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: FfySfnEntities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var entry = DbContext.FfySfnEntries.Find(id);
            if (entry == null)
                return HttpNotFound();

            return View(entry);
        }

        // POST: FfySfnEntities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FfySfnEntry ffySfnEntry)
        {
            if (!ModelState.IsValid) return View(ffySfnEntry);

            DbContext.Entry(ffySfnEntry).State = EntityState.Modified;

            if (!ffySfnEntry.AccessionNumber.IsNullOrWhiteSpace() && ffySfnEntry.ProjectNumber.IsNullOrWhiteSpace())
            {
                // Find the associated project number and populate:
                var foundProject =
                    DbContext.AllProjectsNew.FirstOrDefault(p => p.AccessionNumber.Equals(ffySfnEntry.AccessionNumber));

                ffySfnEntry.ProjectNumber = foundProject.ProjectNumber.Trim();
            }
            else if (!ffySfnEntry.ProjectNumber.IsNullOrWhiteSpace() && ffySfnEntry.AccessionNumber.IsNullOrWhiteSpace())
            {
                var start = FiscalYearService.FiscalStartDate;
                var end = FiscalYearService.FiscalEndDate;
                var foundProject = DbContext.AllProjectsNew
                    .Where(p => p.ProjectStartDate <= end) //project has actually started
                    .Where(p => p.ProjectEndDate >= start).FirstOrDefault(p => p.ProjectNumber.Trim().Equals(ffySfnEntry.ProjectNumber.Trim()));
                    
                ffySfnEntry.AccessionNumber = foundProject.AccessionNumber.Trim();
            }

            DbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: FfySfnEntities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var entry = DbContext.FfySfnEntries.Find(id);
            if (entry == null)
                return HttpNotFound();

            return View(entry);
        }

        // POST: FfySfnEntities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var entry = DbContext.FfySfnEntries.Find(id);

            DbContext.FfySfnEntries.Remove(entry);
            DbContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
