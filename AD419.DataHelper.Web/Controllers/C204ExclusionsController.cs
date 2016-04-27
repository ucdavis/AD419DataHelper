using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using AD419.DataHelper.Web.Models;

namespace AD419.DataHelper.Web.Controllers
{
    public class C204ExclusionsController : SuperController
    {
        // GET: C204Exclusions
        public ActionResult Index()
        {
            var exclusions = DbContext.C204Exclusions.ToList();
            return View(exclusions);
        }

        // GET: C204Exclusions/Details/5
        public ActionResult Details(string id)
        {
            var exclusion = DbContext.C204Exclusions.Find(id);
            if (exclusion == null)
            {
                return HttpNotFound();
            }

            return View(exclusion);
        }

        // GET: C204Exclusions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: C204Exclusions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AwardNumber, Comments")] C204Exclusions exclusion)
        {
            if (!ModelState.IsValid) return View(exclusion);

            DbContext.C204Exclusions.Add(exclusion);
            DbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: C204Exclusions/Edit/5
        public ActionResult Edit(string id)
        {
            var exclusion = DbContext.C204Exclusions.Find(id);
            if (exclusion == null)
            {
                return HttpNotFound();
            }

            return View(exclusion);
        }

        // POST: C204Exclusions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AwardNumber, Comments")] C204Exclusions exclusion)
        {
            if (!ModelState.IsValid) return View(exclusion);

            DbContext.Entry(exclusion).State = EntityState.Modified;
            DbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: C204Exclusions/Delete/5
        public ActionResult Delete(string id)
        {
            var exclusion = DbContext.C204Exclusions.Find(id);
            if (exclusion == null)
            {
                return HttpNotFound();
            }

            return View(exclusion);
        }

        // POST: C204Exclusions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var exclusion = DbContext.C204Exclusions.Find(id);
            if (exclusion == null)
            {
                return HttpNotFound();
            }

            DbContext.C204Exclusions.Remove(exclusion);
            DbContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
