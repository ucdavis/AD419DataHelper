using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Routing;
using AD419.DataHelper.Web.Models;

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

            if (String.IsNullOrWhiteSpace(ffySfnEntry.AccessionNumber) || String.IsNullOrWhiteSpace(ffySfnEntry.ProjectNumber))
            {
                var foundProject = new AllProjectsNew();
                if (!String.IsNullOrWhiteSpace(ffySfnEntry.AccessionNumber) && String.IsNullOrWhiteSpace(ffySfnEntry.ProjectNumber))
                {
                    // Find the associated project number and populate:
                    foundProject =
                        DbContext.AllProjectsNew.FirstOrDefault(p => p.AccessionNumber.Equals(ffySfnEntry.AccessionNumber));

                    if (foundProject == null)
                    {
                        TempData.Add("ErrorMessage", "Unable to locate corresponding project.  Please try match using Project Number.");
                        return View(ffySfnEntry);
                    }

                    ffySfnEntry.ProjectNumber = foundProject.ProjectNumber.Trim();
                }
                else if (!String.IsNullOrWhiteSpace(ffySfnEntry.ProjectNumber) && String.IsNullOrWhiteSpace(ffySfnEntry.AccessionNumber))
                {
                    var start = FiscalYearService.FiscalStartDate;
                    var end = FiscalYearService.FiscalEndDate;

                    foundProject =
                        DbContext.AllProjectsNew.Where(p => p.ProjectNumber.Trim().StartsWith(ffySfnEntry.ProjectNumber.Trim()))
                        .Where(p => p.ProjectStartDate <= end) //project has actually started
                        .Where(p => p.ProjectEndDate >= start).OrderByDescending(p => p.Id).FirstOrDefault();

                    if (foundProject == null)
                    {
                        TempData.Add("ErrorMessage", "Unable to locate corresponding project.  Please try match using Accession Number.");
                        return View(ffySfnEntry);
                    }
                    ffySfnEntry.AccessionNumber = foundProject.AccessionNumber.Trim();
                }
                ffySfnEntry.ProjectEndDate = foundProject.ProjectEndDate;
                ffySfnEntry.IsExpired = foundProject.IsExpired;
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

        public ActionResult Exclude(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var entry = DbContext.FfySfnEntriesWithAccounts.Find(id);

            if (entry == null)
            {
                TempData["ErrorMessage"] = "Error: Unable to find matching FFY SFN Entry.  Please try again.";
                RedirectToAction("Index");
            }
 
            return RedirectToAction("CreateExclusionFromProjectMapping", "ArcCodeAccountExclusions",
                new
                {
                    Year = FiscalYearService.FiscalYear,
                    Chart = entry.Chart,
                    Account = entry.Account,
                    AnnualReportCode = entry.AnnualReportCode,
                    AwardNumber = (String.IsNullOrWhiteSpace(entry.OpFundAwardNumber) ? entry.AccountsAwardNumber : entry.OpFundAwardNumber),
                    Comments = entry.AccountName + ": " + entry.Purpose,
                    Is204 = (entry.Sfn.Equals("204") ? true : false),
                    ProjectNumber = entry.ProjectNumber
                });
        }
    }
}
