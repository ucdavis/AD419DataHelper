using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AD419.DataHelper.Web.Models;

namespace AD419.DataHelper.Web.Controllers
{
    public class C204AcctXProjController : SuperController
    {
        // GET: C204AcctXProj
        public ActionResult Index()
        {
            return View(DbContext.C204AcctXProj.ToList());
        }

        // GET: C204AcctXProj/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C204AcctXProj c204AcctXProj = DbContext.C204AcctXProj.Find(id);
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
                DbContext.C204AcctXProj.Add(c204AcctXProj);
                DbContext.SaveChanges();
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
            C204AcctXProj c204AcctXProj = DbContext.C204AcctXProj.Find(id);
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
                DbContext.Entry(c204AcctXProj).State = EntityState.Modified;
                DbContext.SaveChanges();
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
            C204AcctXProj c204AcctXProj = DbContext.C204AcctXProj.Find(id);
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
            C204AcctXProj c204AcctXProj = DbContext.C204AcctXProj.Find(id);
            DbContext.C204AcctXProj.Remove(c204AcctXProj);
            DbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
