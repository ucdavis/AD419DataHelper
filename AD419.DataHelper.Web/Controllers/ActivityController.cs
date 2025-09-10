using System.Linq;
using System.Web.Mvc;
using System.Net;
using AD419.DataHelper.Web.Models;

namespace AD419.DataHelper.Web.Controllers
{
    public class ActivitiesController : SuperController
    {
        // GET: Activities
        public ActionResult Index()
        {
            var model = new ActivitiesViewModel
            {
                Activities = DbContext.Activities
                    .OrderBy(a => a.IsExcluded.HasValue ? (a.IsExcluded.Value ? 1 : 0) : 2)  // No first, Yes second, NULL last
                    .ThenBy(a => a.Activity)
                    .ToList(),
                CodeTypeName = "Activities"
            };
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var activity = DbContext.Activities.Find(id);

            if (activity == null)
            {
                return HttpNotFound();
            }

            return View(activity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind(Include = "Id,Activity,IsExcluded")] ActivityModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var existing = DbContext.Activities.Find(id);
            if (existing == null) return HttpNotFound();

            existing.IsExcluded = model.IsExcluded;

            DbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}