using AD_419_DataHelperWebApp.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace AD_419_DataHelperWebApp.Controllers
{
    public class AllProjectsNewController : SuperController
    {
        // GET: AllProjectsNew
        public ActionResult Index()
        {
            var projects = DbContext.AllProjectsNew.ToList();
            return View(projects);
        }

        // GET: AllProjectsNew/Details/5
        public ActionResult Details(int id)
        {
            var project = DbContext.AllProjectsNew.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }

            return View(project);
        }

        // GET: AllProjectsNew/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AllProjectsNew/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idProject,AccessionNumber,ProjectNumber,ProposalNumber,AwardNumber,Title,OrganizationName,OrgR,Department,ProjectDirector,CoProjectDirectors,FundingSource,ProjectStartDate,ProjectEndDate,ProjectStatus,isInterdepartmental,IsActive")] AllProjectsNew project)
        {
            if (!ModelState.IsValid) return View(project);

            DbContext.AllProjectsNew.Add(project);
            DbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: AllProjectsNew/Edit/5
        public ActionResult Edit(int id)
        {
            var project = DbContext.AllProjectsNew.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }

            return View(project);
        }

        // POST: AllProjectsNew/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idProject,AccessionNumber,ProjectNumber,ProposalNumber,AwardNumber,Title,OrganizationName,OrgR,Department,ProjectDirector,CoProjectDirectors,FundingSource,ProjectStartDate,ProjectEndDate,ProjectStatus,IsInterdepartmental,IsActive")] AllProjectsNew project)
        {
            if (!ModelState.IsValid) return View(project);

            DbContext.Entry(project).State = EntityState.Modified;
            DbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: AllProjectsNew/Delete/5
        public ActionResult Delete(int id)
        {
            var project = DbContext.AllProjectsNew.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }

            return View(project);
        }

        // POST: AllProjectsNew/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var project = DbContext.AllProjectsNew.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }

            DbContext.AllProjectsNew.Remove(project);
            DbContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
