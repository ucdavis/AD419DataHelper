using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using AD419.DataHelper.Web.Models;
using AD419.DataHelper.Web.ViewModels;

namespace AD419.DataHelper.Web.Controllers
{
    public class ExpiredProjectsController : SuperController
    {
        // GET: ExpiredProjectCrossReference
        public async Task<ViewResult> Index()
        {
            var year = FiscalYearService.FiscalYear;
            var expired = await DbContext.GetExpired20XProjects(year);
            var projects = DbContext.ExpiredProjectCrossReference.ToList();

            var results = 
                from x in expired
                join p in projects on x.AccessionNumber equals p.FromAccession into pMatch
                from match in pMatch.DefaultIfEmpty()
                select new ExpiringProjectMatch { ExpiringProject = x, MatchedProject = match};

            return View(results.ToList());
        }

        public ActionResult Mappings()
        {
            var projects = DbContext.ExpiredProjectCrossReference.ToList();
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MapProject(string from, string to)
        {
            if (string.IsNullOrWhiteSpace(from))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (string.IsNullOrWhiteSpace(to))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var match = new ExpiredProjectCrossReference()
            {
                FromAccession = from,
                ToAccession = to,
                IsActive = true
            };

            DbContext.ExpiredProjectCrossReference.Add(match);
            DbContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
