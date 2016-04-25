using AD_419_DataHelperWebApp.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace AD_419_DataHelperWebApp.Controllers
{
    public class DOS_CodesController : SuperController
    {
        // GET: DOS_Codes
        public ActionResult Index()
        {
            return View(DbContext.DOS_Codes.ToList());
        }

        // GET: DOS_Codes/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DOS_Codes dOS_Codes = DbContext.DOS_Codes.Find(id);
            if (dOS_Codes == null)
            {
                return HttpNotFound();
            }
            return View(dOS_Codes);
        }

        // GET: DOS_Codes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DOS_Codes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DOS_Code,Description,IncludeInAD419FTE")] DOS_Codes dOS_Codes)
        {
            if (ModelState.IsValid)
            {
                DbContext.DOS_Codes.Add(dOS_Codes);
                DbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dOS_Codes);
        }

        // GET: DOS_Codes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DOS_Codes dOS_Codes = DbContext.DOS_Codes.Find(id);
            if (dOS_Codes == null)
            {
                return HttpNotFound();
            }
            return View(dOS_Codes);
        }

        // POST: DOS_Codes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DOS_Code,Description,IncludeInAD419FTE")] DOS_Codes dOS_Codes)
        {
            if (ModelState.IsValid)
            {
                DbContext.Entry(dOS_Codes).State = EntityState.Modified;
                DbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dOS_Codes);
        }

        // GET: DOS_Codes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DOS_Codes dOS_Codes = DbContext.DOS_Codes.Find(id);
            if (dOS_Codes == null)
            {
                return HttpNotFound();
            }
            return View(dOS_Codes);
        }

        // POST: DOS_Codes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            DOS_Codes dOS_Codes = DbContext.DOS_Codes.Find(id);
            DbContext.DOS_Codes.Remove(dOS_Codes);
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
