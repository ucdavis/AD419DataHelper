using System;
using AD_419_DataHelperWebApp.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace AD_419_DataHelperWebApp.Controllers
{
    public class ArcCodeAccountExclusionsController : SuperController
    {
        private AD419DataContext db = new AD419DataContext();

        // GET: ArcCodeAccountExclusions
        public ActionResult Index()
        {
            return View(db.ArcCodeAccountExclusions.ToList());
        }

        // GET: ArcCodeAccountExclusions/Details/5
        public ActionResult Details(int? year, string chart, string account, string annualReportCode)
        {
            if (year == null || string.IsNullOrWhiteSpace(chart) || string.IsNullOrWhiteSpace(account) || string.IsNullOrWhiteSpace(annualReportCode))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArcCodeAccountExclusion arcCodeAccountExclusion = db.ArcCodeAccountExclusions.Find(year, chart, account, annualReportCode);
            if (arcCodeAccountExclusion == null)
            {
                return HttpNotFound();
            }
            return View(arcCodeAccountExclusion);
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
        public ActionResult Create([Bind(Include = "Year,Chart,Account,AnnualReportCode,Comments")] ArcCodeAccountExclusion arcCodeAccountExclusion)
        {
            if (ModelState.IsValid)
            {
                db.ArcCodeAccountExclusions.Add(arcCodeAccountExclusion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(arcCodeAccountExclusion);
        }

        // GET: ArcCodeAccountExclusions/Edit/5
        public ActionResult Edit(int? year, string chart, string account, string annualReportCode)
        {
            if (year == null || string.IsNullOrWhiteSpace(chart) || string.IsNullOrWhiteSpace(account) || string.IsNullOrWhiteSpace(annualReportCode))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArcCodeAccountExclusion arcCodeAccountExclusion = db.ArcCodeAccountExclusions.Find(year, chart, account, annualReportCode);
            if (arcCodeAccountExclusion == null)
            {
                return HttpNotFound();
            }
            return View(arcCodeAccountExclusion);
        }

        // POST: ArcCodeAccountExclusions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Year,Chart,Account,AnnualReportCode,Comments")] ArcCodeAccountExclusion arcCodeAccountExclusion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(arcCodeAccountExclusion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(arcCodeAccountExclusion);
        }

        // GET: ArcCodeAccountExclusions/Delete/5
        public ActionResult Delete(int? year, string chart, string account, string annualReportCode)
        {
            if (year == null || string.IsNullOrWhiteSpace(chart) || string.IsNullOrWhiteSpace(account) || string.IsNullOrWhiteSpace(annualReportCode))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArcCodeAccountExclusion arcCodeAccountExclusion = db.ArcCodeAccountExclusions.Find(year, chart, account, annualReportCode);
            if (arcCodeAccountExclusion == null)
            {
                return HttpNotFound();
            }
            return View(arcCodeAccountExclusion);
        }

        // POST: ArcCodeAccountExclusions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int year, string chart, string account, string annualReportCode)
        {
            ArcCodeAccountExclusion arcCodeAccountExclusion = db.ArcCodeAccountExclusions.Find(year, chart, account, annualReportCode);
            db.ArcCodeAccountExclusions.Remove(arcCodeAccountExclusion);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
