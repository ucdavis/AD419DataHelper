using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using AD419.DataHelper.Web.Models;

namespace AD419.DataHelper.Web.Controllers
{
    public class C204AcctXProjController : SuperController
    {
        // GET: C204AcctXProj
        public ActionResult Index()
        {
            var projects = DbContext.C204AcctXProj.ToList();
            return View(projects);
        }

        // GET: C204AcctXProj/Details/5
        public ActionResult Details(int id)
        {
            var project = DbContext.C204AcctXProj.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }

            return View(project);
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
        public ActionResult Create([Bind(Include = "pk,AccountID,Expenses,Accession,Chart,Is219,CSREES_ContractNo,AwardNum,IsCurrentProject,Org,OrgR,IsExcludedExpense")] C204AcctXProj project)
        {
            if (!ModelState.IsValid) return View(project);

            DbContext.C204AcctXProj.Add(project);
            DbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: C204AcctXProj/Edit/5
        public ActionResult Edit(int id)
        {
            var project = DbContext.C204AcctXProj.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }

            return View(project);
        }

        // POST: C204AcctXProj/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "pk,AccountID,Expenses,Accession,Chart,Is219,CSREES_ContractNo,AwardNum,IsCurrentProject,Org,OrgR,IsExcludedExpense")] C204AcctXProj project)
        {
            if (!ModelState.IsValid) return View(project);

            DbContext.Entry(project).State = EntityState.Modified;
            DbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: C204AcctXProj/Delete/5
        public ActionResult Delete(int id)
        {
            var project = DbContext.C204AcctXProj.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }

            return View(project);
        }

        // POST: C204AcctXProj/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var project = DbContext.C204AcctXProj.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }

            DbContext.C204AcctXProj.Remove(project);
            DbContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
