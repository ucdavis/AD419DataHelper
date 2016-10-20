using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using AD419.DataHelper.Web.Models;

namespace AD419.DataHelper.Web.Controllers
{
    public class ArcCodeAccountExclusionsController : SuperController
    {
        // GET: ArcCodeAccountExclusions
        public ActionResult Index()
        {
            var exclusions = DbContext.ArcCodeAccountExclusions.ToList();
            return View(exclusions);
        }

        public ActionResult Review()
        {
            var year = FiscalYearService.FiscalYear;
            var exclusions = DbContext.ArcCodeAccountExclusions
                .Where(x => x.Year == year)
                .ToList();

            return View(exclusions);
        }

        [HttpPost]
        public ActionResult ImportOld()
        {
            // get this year's data
            var year = FiscalYearService.FiscalYear;
            var current = DbContext.ArcCodeAccountExclusions
                .Where(x => x.Year == year)
                .ToList();

            // get last year's data
            var last = year - 1;
            var previous = DbContext.ArcCodeAccountExclusions
                .Where(x => x.Year == last)
                .ToList();

            // match on the other 3 keys
            var matches = from p in previous 
                          join c in current on new { p.Chart, p.Account, p.AnnualReportCode } equals new { c.Chart, c.Account, c.AnnualReportCode } into g
                          from c2 in g.DefaultIfEmpty()
                          select new { Previous = p, Match = c2 };

            // get ones missing a match
            var missing = matches.Where(m => m.Match == null).Select(m => m.Previous);

            // map to new year
            var imports = missing.Select(m => new ArcCodeAccountExclusion
            {
                Year             = year,
                Chart            = m.Chart,
                Account          = m.Account,
                AnnualReportCode = m.AnnualReportCode,
                Is204            = m.Is204,
                ProjectNumber    = m.ProjectNumber,
                AwardNumber      = m.AwardNumber
            });

            // insert
            DbContext.ArcCodeAccountExclusions.AddRange(imports);
            DbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: ArcCodeAccountExclusions/Details/5
        public ActionResult Details(int? year, string chart, string account, string annualReportCode)
        {
            var exclusion = DbContext.ArcCodeAccountExclusions.Find(year, chart, account, annualReportCode);
            if (exclusion == null)
            {
                return HttpNotFound();
            }

            return View(exclusion);
        }

        public ActionResult CreateExclusionFromProjectMapping(ArcCodeAccountExclusion exclusion)
        {
            if (exclusion == null)
            {
                TempData["ErrorMessage"] =
                    "Error: Unable to redirect to ARC/Account Exclusions page.  Try selecting \"ARC/Account Exclusions\" link listed under maintenance tab.";
                return RedirectToAction("Index", "ProjectMapping");
            }
                
            return View("Create", exclusion);
        }

        // GET: ArcCodeAccountExclusions/Create
        public ActionResult Create()
        {
            var model = new ArcCodeAccountExclusion()
            {
                Year = FiscalYearService.FiscalYear
            };
            return View(model);
        }

        // POST: ArcCodeAccountExclusions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ArcCodeAccountExclusion exclusion)
        {
            if (!ModelState.IsValid) return View(exclusion);

            DbContext.ArcCodeAccountExclusions.Add(exclusion);
            DbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: ArcCodeAccountExclusions/Edit/5
        public ActionResult Edit(int? year, string chart, string account, string annualReportCode)
        {
            var exclusion = DbContext.ArcCodeAccountExclusions.Find(year, chart, account, annualReportCode);
            if (exclusion == null)
            {
                return HttpNotFound();
            }

            return View(exclusion);
        }

        // POST: ArcCodeAccountExclusions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ArcCodeAccountExclusion exclusion)
        {
            if (!ModelState.IsValid) return View(exclusion);

            DbContext.Entry(exclusion).State = EntityState.Modified;
            DbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: ArcCodeAccountExclusions/Delete/5
        public ActionResult Delete(int? year, string chart, string account, string annualReportCode)
        {
            var exclusion = DbContext.ArcCodeAccountExclusions.Find(year, chart, account, annualReportCode);
            if (exclusion == null)
            {
                return HttpNotFound();
            }

            return View(exclusion);
        }

        // POST: ArcCodeAccountExclusions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int year, string chart, string account, string annualReportCode)
        {
            var exclusion = DbContext.ArcCodeAccountExclusions.Find(year, chart, account, annualReportCode);
            if (exclusion == null)
            {
                return HttpNotFound();
            }

            DbContext.ArcCodeAccountExclusions.Remove(exclusion);
            DbContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
