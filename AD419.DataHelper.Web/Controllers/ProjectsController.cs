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
            var projects = DbContext.Projects.ToList();
            return View(projects);
        }

        // GET: Projects/Details/5
        public ActionResult Details(int id)
        {
            var project = DbContext.Projects.Find(id);
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
        public ActionResult Create([Bind(Include = "idProject,Accession,Project,isInterdepartmental,isValid,BeginDate,TermDate,ProjTypeCd,RegionalProjNum,CRIS_DeptID,CoopDepts,CSREES_ContractNo,StatusCd,Title,UpdateDate,inv1,inv2,inv3,inv4,inv5,inv6,IsCurrentAD419Project")] Projects project)
        {
            if (!ModelState.IsValid) return View(project);

            DbContext.Projects.Add(project);
            DbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Projects/Edit/5
        public ActionResult Edit(int id)
        {
            var project = DbContext.Projects.Find(id);
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
        public ActionResult Edit([Bind(Include = "idProject,Accession,Project,isInterdepartmental,isValid,BeginDate,TermDate,ProjTypeCd,RegionalProjNum,CRIS_DeptID,CoopDepts,CSREES_ContractNo,StatusCd,Title,UpdateDate,inv1,inv2,inv3,inv4,inv5,inv6,IsCurrentAD419Project")] Projects project)
        {
            if (!ModelState.IsValid) return View(project);

            DbContext.Entry(project).State = EntityState.Modified;
            DbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Projects/Delete/5
        public ActionResult Delete(int id)
        {
            var project = DbContext.Projects.Find(id);
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
            var project = DbContext.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }

            DbContext.Projects.Remove(project);
            DbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult AccessionNumbers()
        {
            var accessions = DbContext.Projects
                .Select(p => p.Accession)
                .Distinct()
                .ToList();

            return new JsonResult()
            {
                Data = accessions,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public ActionResult FindAccessionNumbers(string query)
        {
            var accessions = DbContext.Projects
                .Select(p => p.Accession)
                .Where(a => a.Contains(query))
                .Distinct()
                .ToList();

            return new JsonResult()
            {
                Data = accessions,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public ActionResult ProjectNumbers()
        {
            var projects = DbContext.Projects
                .Select(p => p.Project)
                .Distinct()
                .ToList();

            return new JsonResult()
            {
                Data =  projects,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public ActionResult FindProjectNumbers(string query)
        {
            var projects = DbContext.Projects
                .Select(p => p.Project)
                .Where(p => p.Contains(query))
                .Distinct()
                .ToList();

            return new JsonResult()
            {
                Data = projects,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}
