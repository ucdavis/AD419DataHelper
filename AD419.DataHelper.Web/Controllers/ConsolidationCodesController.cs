using AD419.DataHelper.Web.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace AD419.DataHelper.Web.Controllers
{
    public class ConsolidationCodesController : Controller
    {
        private AD419DataContext db = new AD419DataContext();

        // GET: ConsolidationCodes
        public ActionResult Index()
        {
            return View(db.ConsolidationCodes.ToList());
        }

        // GET: ConsolidationCodes/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConsolidationCodes consolidationCodes = db.ConsolidationCodes.Find(id);
            if (consolidationCodes == null)
            {
                return HttpNotFound();
            }
            return View(consolidationCodes);
        }

        // GET: ConsolidationCodes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ConsolidationCodes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ObjectConsolidationNumber,ObjectConsolidationName,IncludeInFTECalc,IncludeInLaborTransactions")] ConsolidationCodes consolidationCodes)
        {
            if (ModelState.IsValid)
            {
                db.ConsolidationCodes.Add(consolidationCodes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(consolidationCodes);
        }

        // GET: ConsolidationCodes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConsolidationCodes consolidationCodes = db.ConsolidationCodes.Find(id);
            if (consolidationCodes == null)
            {
                return HttpNotFound();
            }
            return View(consolidationCodes);
        }

        // POST: ConsolidationCodes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ObjectConsolidationNumber,ObjectConsolidationName,IncludeInFTECalc,IncludeInLaborTransactions")] ConsolidationCodes consolidationCodes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(consolidationCodes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(consolidationCodes);
        }

        // GET: ConsolidationCodes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConsolidationCodes consolidationCodes = db.ConsolidationCodes.Find(id);
            if (consolidationCodes == null)
            {
                return HttpNotFound();
            }
            return View(consolidationCodes);
        }

        // POST: ConsolidationCodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ConsolidationCodes consolidationCodes = db.ConsolidationCodes.Find(id);
            db.ConsolidationCodes.Remove(consolidationCodes);
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
