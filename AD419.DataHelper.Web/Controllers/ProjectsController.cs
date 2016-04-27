using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using AD419.DataHelper.Web.Models;

namespace AD419.DataHelper.Web.Controllers
{
    public class ProjectsController : SuperController
    {
        // GET: Projects
        public ActionResult Index()
        {
            var projects = DbContext.AllProjects.ToList();
            return View(projects);
        }

        // GET: Projects/Details/5
        public ActionResult Details(int id)
        {
            var project = DbContext.AllProjects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }

            return View(project);
        }

        // GET: Projects/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idProject,Accession,Project,isInterdepartmental,isValid,BeginDate,TermDate,ProjTypeCd,RegionalProjNum,CRIS_DeptID,CoopDepts,CSREES_ContractNo,StatusCd,Title,UpdateDate,inv1,inv2,inv3,inv4,inv5,inv6,IsCurrentAD419Project")] AllProject allProject)
        {
            if (!ModelState.IsValid) return View(allProject);

            DbContext.AllProjects.Add(allProject);
            DbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Projects/Edit/5
        public ActionResult Edit(int id)
        {
            var project = DbContext.AllProjects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }

            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idProject,Accession,Project,isInterdepartmental,isValid,BeginDate,TermDate,ProjTypeCd,RegionalProjNum,CRIS_DeptID,CoopDepts,CSREES_ContractNo,StatusCd,Title,UpdateDate,inv1,inv2,inv3,inv4,inv5,inv6,IsCurrentAD419Project")] AllProject allProject)
        {
            if (!ModelState.IsValid) return View(allProject);

            DbContext.Entry(allProject).State = EntityState.Modified;
            DbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Projects/Delete/5
        public ActionResult Delete(int id)
        {
            var project = DbContext.AllProjects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }

            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var project = DbContext.AllProjects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }

            DbContext.AllProjects.Remove(project);
            DbContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
