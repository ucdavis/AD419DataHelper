using AD_419_DataHelperWebApp.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace AD_419_DataHelperWebApp.Controllers
{
    public class C204AcctXProjController : SuperController
    {
        private AD419DataContext db = new AD419DataContext();

        // GET: C204AcctXProj
        public ActionResult Index()
        {
            return View(db.C204AcctXProj.ToList());
        }

        // GET: C204AcctXProj/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C204AcctXProj c204AcctXProj = db.C204AcctXProj.Find(id);
            if (c204AcctXProj == null)
            {
                return HttpNotFound();
            }
            return View(c204AcctXProj);
        }

        // GET: C204AcctXProj/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: C204AcctXProj/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "pk,AccountID,Expenses,Accession,Chart,Is219,CSREES_ContractNo,AwardNum,IsCurrentProject,Org,OrgR,IsExcludedExpense")] C204AcctXProj c204AcctXProj)
        {
            if (ModelState.IsValid)
            {
                db.C204AcctXProj.Add(c204AcctXProj);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(c204AcctXProj);
        }

        // GET: C204AcctXProj/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C204AcctXProj c204AcctXProj = db.C204AcctXProj.Find(id);
            if (c204AcctXProj == null)
            {
                return HttpNotFound();
            }
            return View(c204AcctXProj);
        }

        // POST: C204AcctXProj/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "pk,AccountID,Expenses,Accession,Chart,Is219,CSREES_ContractNo,AwardNum,IsCurrentProject,Org,OrgR,IsExcludedExpense")] C204AcctXProj c204AcctXProj)
        {
            if (ModelState.IsValid)
            {
                db.Entry(c204AcctXProj).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(c204AcctXProj);
        }

        // GET: C204AcctXProj/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C204AcctXProj c204AcctXProj = db.C204AcctXProj.Find(id);
            if (c204AcctXProj == null)
            {
                return HttpNotFound();
            }
            return View(c204AcctXProj);
        }

        // POST: C204AcctXProj/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            C204AcctXProj c204AcctXProj = db.C204AcctXProj.Find(id);
            db.C204AcctXProj.Remove(c204AcctXProj);
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
