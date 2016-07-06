using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AD419.DataHelper.Web.Models;

namespace AD419.DataHelper.Web.Controllers
{
    public class ExpiredProjectsController : SuperController
    {
        // GET: ExpiredProjectCrossReference
        public ActionResult Index()
        {
            var projects = DbContext.ExpiredProjectCrossReference.ToList();
            return View(projects);
        }

        public async Task<ViewResult> ExpiringProjects()
        {
            var year = FiscalYear;
            var projects = await DbContext.GetExpired20XProjects(year);
            return View(projects);
        }

        // GET: ExpiredProjectCrossReference/Details/5
        public ActionResult Details(int id)
        {
            var project = DbContext.ExpiredProjectCrossReference.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }

            return View(project);
        }

        // GET: ExpiredProjectCrossReference/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ExpiredProjectCrossReference/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RemapID,FromAccession,IsActive,ToAccession")] ExpiredProjectCrossReference expiredProjectCrossReference)
        {
            if (!ModelState.IsValid) return View(expiredProjectCrossReference);

            DbContext.ExpiredProjectCrossReference.Add(expiredProjectCrossReference);
            DbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: ExpiredProjectCrossReference/Edit/5
        public ActionResult Edit(int id)
        {
            var project = DbContext.ExpiredProjectCrossReference.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }

            return View(project);
        }

        // POST: ExpiredProjectCrossReference/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RemapID,FromAccession,IsActive,ToAccession")] ExpiredProjectCrossReference expiredProjectCrossReference)
        {
            if (!ModelState.IsValid) return View(expiredProjectCrossReference);

            DbContext.Entry(expiredProjectCrossReference).State = EntityState.Modified;
            DbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: ExpiredProjectCrossReference/Delete/5
        public ActionResult Delete(int id)
        {
            var project = DbContext.ExpiredProjectCrossReference.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }

            return View(project);
        }

        // POST: ExpiredProjectCrossReference/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var project = DbContext.ExpiredProjectCrossReference.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }

            DbContext.ExpiredProjectCrossReference.Remove(project);
            DbContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
