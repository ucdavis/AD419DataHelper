using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using AD419.DataHelper.Web.Models;

namespace AD419.DataHelper.Web.Controllers
{
    public class AllProjectsImportController : SuperController
    {
        // GET: AllProjectsImport
        public ActionResult Index()
        {
            var projects = DbContext.AllProjectsImports.ToList();
            return View(projects);
        }

        // GET: AllProjectsImport/Details/5
        public ActionResult Details(int id)
        {
            var project = DbContext.AllProjectsImports.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }

            return View(project);
        }

        // GET: AllProjectsImport/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AllProjectsImport/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idProject,AccessionNumber,ProjectNumber,ProposalNumber,AwardNumber,Title,OrganizationName,Department,ProjectDirector,CoProjectDirectors,FundingSource,ProjectStartDate,ProjectEndDate,ProjectStatus")] AllProjectImport project)
        {
            if (!ModelState.IsValid) return View(project);

            DbContext.AllProjectsImports.Add(project);
            DbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: AllProjectsImport/Edit/5
        public ActionResult Edit(int id)
        {
            var project = DbContext.AllProjectsImports.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }

            return View(project);
        }

        // POST: AllProjectsImport/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idProject,AccessionNumber,ProjectNumber,ProposalNumber,AwardNumber,Title,OrganizationName,Department,ProjectDirector,CoProjectDirectors,FundingSource,ProjectStartDate,ProjectEndDate,ProjectStatus")] AllProjectImport project)
        {
            if (!ModelState.IsValid) return View(project);

            DbContext.Entry(project).State = EntityState.Modified;
            DbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: AllProjectsImport/Delete/5
        public ActionResult Delete(int id)
        {
            var project = DbContext.AllProjectsImports.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }

            return View(project);
        }

        // POST: AllProjectsImport/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var project = DbContext.AllProjectsImports.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }

            DbContext.AllProjectsImports.Remove(project);
            DbContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
