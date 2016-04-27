using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AD419.DataHelper.Web.Models;

namespace AD419.DataHelper.Web.Controllers
{
    public class ExpiredProjectCrossReferenceController : SuperController
    {
        // GET: ExpiredProjectCrossReference
        public ActionResult Index()
        {
            return View(DbContext.ExpiredProjectCrossReference.ToList());
        }

        // GET: ExpiredProjectCrossReference/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpiredProjectCrossReference expiredProjectCrossReference = DbContext.ExpiredProjectCrossReference.Find(id);
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
                DbContext.ExpiredProjectCrossReference.Add(expiredProjectCrossReference);
                DbContext.SaveChanges();
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
            ExpiredProjectCrossReference expiredProjectCrossReference = DbContext.ExpiredProjectCrossReference.Find(id);
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
                DbContext.Entry(expiredProjectCrossReference).State = EntityState.Modified;
                DbContext.SaveChanges();
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
            ExpiredProjectCrossReference expiredProjectCrossReference = DbContext.ExpiredProjectCrossReference.Find(id);
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
            ExpiredProjectCrossReference expiredProjectCrossReference = DbContext.ExpiredProjectCrossReference.Find(id);
            DbContext.ExpiredProjectCrossReference.Remove(expiredProjectCrossReference);
            DbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
