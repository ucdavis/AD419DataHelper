using AD419.DataHelper.Web.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace AD419.DataHelper.Web.Controllers
{
    public class TransDocTypesController : SuperController
    {
        // GET: TransDocTypes
        public ActionResult Index()
        {
            return View(DbContext.TransDocTypes.ToList());
        }

        // GET: TransDocTypes/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransDocTypes transDocTypes = DbContext.TransDocTypes.Find(id);
            if (transDocTypes == null)
            {
                return HttpNotFound();
            }
            return View(transDocTypes);
        }

        // GET: TransDocTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TransDocTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DocumentType,Description,IncludeInFTECalc,IncludeInFISExpenses")] TransDocTypes transDocTypes)
        {
            if (ModelState.IsValid)
            {
                DbContext.TransDocTypes.Add(transDocTypes);
                DbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(transDocTypes);
        }

        // GET: TransDocTypes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransDocTypes transDocTypes = DbContext.TransDocTypes.Find(id);
            if (transDocTypes == null)
            {
                return HttpNotFound();
            }
            return View(transDocTypes);
        }

        // POST: TransDocTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DocumentType,Description,IncludeInFTECalc,IncludeInFISExpenses")] TransDocTypes transDocTypes)
        {
            if (ModelState.IsValid)
            {
                DbContext.Entry(transDocTypes).State = EntityState.Modified;
                DbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(transDocTypes);
        }

        // GET: TransDocTypes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransDocTypes transDocTypes = DbContext.TransDocTypes.Find(id);
            if (transDocTypes == null)
            {
                return HttpNotFound();
            }
            return View(transDocTypes);
        }

        // POST: TransDocTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            TransDocTypes transDocTypes = DbContext.TransDocTypes.Find(id);
            DbContext.TransDocTypes.Remove(transDocTypes);
            DbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
