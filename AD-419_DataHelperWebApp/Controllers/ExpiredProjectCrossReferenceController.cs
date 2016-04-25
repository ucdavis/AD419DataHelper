using AD_419_DataHelperWebApp.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace AD_419_DataHelperWebApp.Controllers
{
    public class ExpiredProjectCrossReferenceController : SuperController
    {
        private AD419DataContext db = new AD419DataContext();

        // GET: ExpiredProjectCrossReference
        public ActionResult Index()
        {
            return View(db.ExpiredProjectCrossReference.ToList());
        }

        // GET: ExpiredProjectCrossReference/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpiredProjectCrossReference expiredProjectCrossReference = db.ExpiredProjectCrossReference.Find(id);
            if (expiredProjectCrossReference == null)
            {
                return HttpNotFound();
            }
            return View(expiredProjectCrossReference);
        }

        // GET: ExpiredProjectCrossReference/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ExpiredProjectCrossReference/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RemapID,FromAccession,IsActive,ToAccession")] ExpiredProjectCrossReference expiredProjectCrossReference)
        {
            if (ModelState.IsValid)
            {
                db.ExpiredProjectCrossReference.Add(expiredProjectCrossReference);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(expiredProjectCrossReference);
        }

        // GET: ExpiredProjectCrossReference/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpiredProjectCrossReference expiredProjectCrossReference = db.ExpiredProjectCrossReference.Find(id);
            if (expiredProjectCrossReference == null)
            {
                return HttpNotFound();
            }
            return View(expiredProjectCrossReference);
        }

        // POST: ExpiredProjectCrossReference/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RemapID,FromAccession,IsActive,ToAccession")] ExpiredProjectCrossReference expiredProjectCrossReference)
        {
            if (ModelState.IsValid)
            {
                db.Entry(expiredProjectCrossReference).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(expiredProjectCrossReference);
        }

        // GET: ExpiredProjectCrossReference/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpiredProjectCrossReference expiredProjectCrossReference = db.ExpiredProjectCrossReference.Find(id);
            if (expiredProjectCrossReference == null)
            {
                return HttpNotFound();
            }
            return View(expiredProjectCrossReference);
        }

        // POST: ExpiredProjectCrossReference/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ExpiredProjectCrossReference expiredProjectCrossReference = db.ExpiredProjectCrossReference.Find(id);
            db.ExpiredProjectCrossReference.Remove(expiredProjectCrossReference);
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
