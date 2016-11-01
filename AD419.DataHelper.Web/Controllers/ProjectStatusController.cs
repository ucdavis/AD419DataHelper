using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AD419.DataHelper.Web.Models;

namespace AD419.DataHelper.Web.Controllers
{
    public class ProjectStatusController : SuperController
    {
       
        // GET: ProjectStatus
        public ActionResult Index()
        {
            var statuses = DbContext.ProjectStatus.ToList();
            var statusNames = statuses.Select(s => s.Status).ToList();
            var model = new ProjectStatusViewModel
            {
                ProjectStatuses = statuses,
                CurrentAd419Projects = DbContext.CurrentAd419Projects.Where(p => !statusNames.Contains(p.ProjectStatus)).ToList()
            };

            return View(model);
        }

        // GET: ProjectStatus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectStatus projectStatus = DbContext.ProjectStatus.Find(id);
            if (projectStatus == null)
            {
                return HttpNotFound();
            }
            return View(projectStatus);
        }

        // GET: ProjectStatus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProjectStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Status,IsExcluded")] ProjectStatus projectStatus)
        {
            if (ModelState.IsValid)
            {
                DbContext.ProjectStatus.Add(projectStatus);
                DbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(projectStatus);
        }

        // GET: ProjectStatus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectStatus projectStatus = DbContext.ProjectStatus.Find(id);
            if (projectStatus == null)
            {
                return HttpNotFound();
            }
            return View(projectStatus);
        }

        // POST: ProjectStatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Status,IsExcluded")] ProjectStatus projectStatus)
        {
            if (ModelState.IsValid)
            {
                DbContext.Entry(projectStatus).State = EntityState.Modified;
                DbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(projectStatus);
        }

        // GET: ProjectStatus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectStatus projectStatus = DbContext.ProjectStatus.Find(id);
            if (projectStatus == null)
            {
                return HttpNotFound();
            }
            return View(projectStatus);
        }

        // POST: ProjectStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProjectStatus projectStatus = DbContext.ProjectStatus.Find(id);
            DbContext.ProjectStatus.Remove(projectStatus);
            DbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
