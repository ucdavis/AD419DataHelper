using AD_419_DataHelperWebApp.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace AD_419_DataHelperWebApp.Controllers
{
    public class AllProjectsNewController : Controller
    {
        private AD419DataContext db = new AD419DataContext();

        // GET: AllProjectsNew
        public ActionResult Index()
        {
            return View(db.AllProjectsNew.ToList());
        }

        // GET: AllProjectsNew/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AllProjectsNew allProjectsNew = db.AllProjectsNew.Find(id);
            if (allProjectsNew == null)
            {
                return HttpNotFound();
            }
            return View(allProjectsNew);
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
        public ActionResult Create([Bind(Include = "idProject,AccessionNumber,ProjectNumber,ProposalNumber,AwardNumber,Title,OrganizationName,OrgR,Department,ProjectDirector,CoProjectDirectors,FundingSource,ProjectStartDate,ProjectEndDate,ProjectStatus,isInterdepartmental,IsActive")] AllProjectsNew allProjectsNew)
        {
            if (ModelState.IsValid)
            {
                db.AllProjectsNew.Add(allProjectsNew);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(allProjectsNew);
        }

        // GET: AllProjectsNew/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AllProjectsNew allProjectsNew = db.AllProjectsNew.Find(id);
            if (allProjectsNew == null)
            {
                return HttpNotFound();
            }
            return View(allProjectsNew);
        }

        // POST: AllProjectsNew/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idProject,AccessionNumber,ProjectNumber,ProposalNumber,AwardNumber,Title,OrganizationName,OrgR,Department,ProjectDirector,CoProjectDirectors,FundingSource,ProjectStartDate,ProjectEndDate,ProjectStatus,IsInterdepartmental,IsActive")] AllProjectsNew allProjectsNew)
        {
            if (ModelState.IsValid)
            {
                db.Entry(allProjectsNew).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(allProjectsNew);
        }

        // GET: AllProjectsNew/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AllProjectsNew allProjectsNew = db.AllProjectsNew.Find(id);
            if (allProjectsNew == null)
            {
                return HttpNotFound();
            }
            return View(allProjectsNew);
        }

        // POST: AllProjectsNew/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AllProjectsNew allProjectsNew = db.AllProjectsNew.Find(id);
            db.AllProjectsNew.Remove(allProjectsNew);
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
