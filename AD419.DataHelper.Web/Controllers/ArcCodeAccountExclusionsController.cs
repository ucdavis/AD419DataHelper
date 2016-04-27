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

        // GET: ArcCodeAccountExclusions/Create
        public ActionResult Create()
        {
            var model = new ArcCodeAccountExclusion() {Year = DateTime.Now.Year};
            return View(model);
        }

        // POST: ArcCodeAccountExclusions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Year,Chart,Account,AnnualReportCode,Comments")] ArcCodeAccountExclusion exclusion)
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
        public ActionResult Edit([Bind(Include = "Year,Chart,Account,AnnualReportCode,Comments")] ArcCodeAccountExclusion exclusion)
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
