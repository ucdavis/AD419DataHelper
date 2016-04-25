using AD_419_DataHelperWebApp.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace AD_419_DataHelperWebApp.Controllers
{
    public class C204ExclusionsController : SuperController
    {
        // GET: Sfn204Exclusions
        public ActionResult Index()
        {
            return View(DbContext.C204Exclusions.ToList());
        }

        // GET: Sfn204Exclusions/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C204Exclusions c204Exclusions = DbContext.C204Exclusions.Find(id);
            if (c204Exclusions == null)
            {
                return HttpNotFound();
            }
            return View(c204Exclusions);
        }

        // GET: Sfn204Exclusions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sfn204Exclusions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AwardNumber, Comments")] C204Exclusions c204Exclusions)
        {
            if (ModelState.IsValid)
            {
                DbContext.C204Exclusions.Add(c204Exclusions);
                DbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(c204Exclusions);
        }

        // GET: Sfn204Exclusions/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C204Exclusions c204Exclusions = DbContext.C204Exclusions.Find(id);
            if (c204Exclusions == null)
            {
                return HttpNotFound();
            }
            return View(c204Exclusions);
        }

        // POST: Sfn204Exclusions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AwardNumber, Comments")] C204Exclusions c204Exclusions)
        {
            if (ModelState.IsValid)
            {
                DbContext.Entry(c204Exclusions).State = EntityState.Modified;
                DbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(c204Exclusions);
        }

        // GET: Sfn204Exclusions/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C204Exclusions c204Exclusions = DbContext.C204Exclusions.Find(id);
            if (c204Exclusions == null)
            {
                return HttpNotFound();
            }
            return View(c204Exclusions);
        }

        // POST: Sfn204Exclusions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            C204Exclusions c204Exclusions = DbContext.C204Exclusions.Find(id);
            DbContext.C204Exclusions.Remove(c204Exclusions);
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
